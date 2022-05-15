using BasicInventory.Application.Model;
using BasicInventory.Application.Service.IService;
using BasicInventory.DataAccess.Repository;
using BasicInventory.Entities;
using BasicInventory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BasicInventory.Application.Model.ItemDTO;

namespace BasicInventory.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IClaimService _claimService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string? keyword)
        {
            try
            {
                var items = await _itemService.GetAll(keyword ?? "");
                return Ok(items);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("getbyid/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _itemService.GetById(id);
                return Ok(item);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateItemDTO obj)
        {
            try
            {
                var item = await _itemService.Create(obj);
                return Ok(item);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateItemDTO obj)
        {
            try
            {
                await _itemService.Update(obj);
                return StatusCode(204);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _itemService.Delete(id);
                return StatusCode(204);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
