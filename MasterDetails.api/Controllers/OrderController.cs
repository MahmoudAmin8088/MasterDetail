using AutoMapper;
using MasterDetails.Core.IUnitOfWork;
using MasterDetails.Core.Models;
using MasterDetails.Core.Models.ModelsDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetails.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetAll()
        {
            var orders = _unitOfWork.Orders.GetOrders();
            if (orders == null)
                return NotFound();
            var orderDto = new List<OrderDto>();
            foreach (var order in orders)
            {
                orderDto.Add(_mapper.Map<OrderDto>(order));
            }
            return Ok(orderDto);

        }

        [HttpGet("[action]/{orderId:int}")]
        [ProducesResponseType(200, Type = typeof(Object))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public IActionResult GetOrder(int orderId)
        {
            var order = _unitOfWork.Orders.GetOrder(orderId);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("[action]/{customerId:int}")]
        [ProducesResponseType(200, Type = typeof(List<OrderDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]


        public IActionResult GetCustomerOrders(int customerId)
        {
            var orders = _unitOfWork.Orders.GetCustomerOrders(customerId);
            if (orders is null)
                return NotFound();
            var ordersDto = new List<OrderDto>();
            foreach (var order in orders)
            {
                ordersDto.Add(_mapper.Map<OrderDto>(order));
            }
            return Ok(ordersDto);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Create([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);

            if (_unitOfWork.Orders.CreateOrder(order) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Create Order");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            orderDto.OrderId = order.OrderId;

            return Ok(orderDto);


        }

        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = _unitOfWork.Orders.GetById(orderDto.OrderId);

            var order = _mapper.Map<OrderDto, Order>(orderDto, obj);
            if (_unitOfWork.Orders.UpdateOrder(order) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Update Order");
                return StatusCode(500, ModelState);
            }

            _unitOfWork.Complete();
            return Ok(orderDto);
        }


        [HttpDelete("{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Delete(int orderId)
        {
            var order = _unitOfWork.Orders.GetById(orderId);

            if (order is null)
                return NotFound();
            if (_unitOfWork.Orders.Delete(order) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Deleting Order");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return NoContent();

        }


    }
}
