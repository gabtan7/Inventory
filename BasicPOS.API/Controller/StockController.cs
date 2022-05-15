using BasicInventory.Application.Model;
using BasicInventory.Application.Service.IService;
using BasicInventory.DataAccess.Repository;
using BasicInventory.Entities;
using BasicInventory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static BasicInventory.Application.Model.StockDTO;

namespace BasicInventory.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(string? keyword)
        {
            try
            {
                var stocks = await _stockService.GetAll(keyword ?? "");
                return Ok(stocks);
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
                var stock = await _stockService.GetById(id);
                return Ok(stock);
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
        public async Task<IActionResult> Create(CreateStockDTO obj)
        {
            try
            {
                var stock = await _stockService.Create(obj);
                return Ok(stock);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        [Route("incrementstock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementStock([FromQuery] int id, [FromQuery] decimal qty)
        {
            try
            {
                await _stockService.IncrementStock(id, qty);
                return StatusCode(204);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        [Route("decrementstock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DecrementStock([FromQuery] int id, [FromQuery] decimal qty)
        {
            try
            {
                await _stockService.DecrementStock(id, qty);
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
                await _stockService.Delete(id);
                return StatusCode(204);
            }

            catch
            {
                return StatusCode(500, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
