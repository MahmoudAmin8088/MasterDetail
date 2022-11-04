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
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ItemDto>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAll()
        {
            var Items = _unitOfWork.Items.GetAll();
            if (Items.Count == 0)
                return NotFound();
            var itemDto=new List<ItemDto>();
            foreach (var item in Items)
            {
                itemDto.Add(_mapper.Map<ItemDto>(item));
            }
            return Ok(itemDto);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromBody] ItemDto itemDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var item = _mapper.Map<Item>(itemDto);
            if (_unitOfWork.Items.Create(item) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Create Item ");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            itemDto.ItemId = item.ItemId;
            return Ok(itemDto);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult Update([FromBody] ItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = _unitOfWork.Items.GetById(itemDto.ItemId);

            var item = _mapper.Map<ItemDto, Item>(itemDto,obj );
            if (_unitOfWork.Items.Update(item) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Update Item");
                return StatusCode(500, ModelState);
            }

            _unitOfWork.Complete();
            return Ok(itemDto);
        }

        [HttpDelete("{ItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public IActionResult Delete(int ItemId)
        {
            var item = _unitOfWork.Items.GetById(ItemId);

            if (item is null)
                return NotFound();
            if (_unitOfWork.Items.Delete(item) is null)
            {
                ModelState.AddModelError("", "SomeThing Went Wrong While Deleting Item");
                return StatusCode(500, ModelState);
            }
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
