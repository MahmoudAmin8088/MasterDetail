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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
                
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAll()
        {
            var Customers = _unitOfWork.Customers.GetAll();
            if(Customers.Count == 0)
                return NotFound();
            var CustomerDto= new List<CustomerDto>();

            foreach (var customer in Customers)
            {
                CustomerDto.Add(_mapper.Map<CustomerDto>(customer));

            }
            return Ok(CustomerDto);

        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Customer = _mapper.Map<Customer>(customerDto);
            if(_unitOfWork.Customers.Create(Customer) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Create Customer ");
                return StatusCode(500,ModelState);
            }
            _unitOfWork.Complete();
            customerDto.CustomerId = Customer.CustomerId;
            return Ok(customerDto);

        }

        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = _unitOfWork.Customers.GetById(customerDto.CustomerId);

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto, obj);
            if(_unitOfWork.Customers.Update(customer) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Update Customer");
                return StatusCode(500, ModelState);
            }

            _unitOfWork.Complete();
            return Ok(customerDto);
        }

        [HttpDelete("{CustomerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Delete(int CustomerId)
        {
            var customer = _unitOfWork.Customers.GetById(CustomerId);

            if (customer is null)
                return NotFound();
            if(_unitOfWork.Customers.Delete(customer) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Deleting Customer");
                return StatusCode(500,ModelState);
            }
            _unitOfWork.Complete();
            return NoContent();

        }


    }
}
