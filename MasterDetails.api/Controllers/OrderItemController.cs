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
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OrderItemDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAll()
        {
            var OrderItems = _unitOfWork.OrderItems.GetAll();
            if (OrderItems.Count == 0)
                return NotFound();
            var orderitemDto = new List<OrderItemDto>();
            foreach (var orderitem in OrderItems)
            {
                orderitemDto.Add(_mapper.Map<OrderItemDto>(orderitem));
            }
            return Ok(orderitemDto);
        }

        [HttpGet("[action]/{orderItemId:int}")]
        [ProducesResponseType(200, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public IActionResult GetOrderItem(int orderItemId)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(orderItemId);
            if (orderItem is null)
                return NotFound();
            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            return Ok(orderItemDto);
        }


        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody] OrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var orderitem = _mapper.Map<OrderItems>(orderItemDto);
            if (_unitOfWork.OrderItems.Create(orderitem) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Create OrderItem ");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            orderItemDto.OrderItemId = orderitem.OrderItemId;
            return Ok(orderItemDto);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] OrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = _unitOfWork.OrderItems.GetById(orderItemDto.OrderItemId);

            var orderitem = _mapper.Map<OrderItemDto, OrderItems>(orderItemDto, obj);
            if (_unitOfWork.OrderItems.Update(orderitem) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Update OrderItem");
                return StatusCode(500, ModelState);
            }

            _unitOfWork.Complete();
            return Ok(orderItemDto);
        }


        [HttpDelete("{orderItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Delete(int orderItemId)
        {
            var orderitem = _unitOfWork.OrderItems.GetById(orderItemId);

            if (orderitem is null)
                return NotFound();
            if (_unitOfWork.OrderItems.Delete(orderitem) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Deleting OrderItem");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
