using AutoMapper;
using BasicInventory.Application.Model;
using BasicInventory.Application.Service.IService;
using BasicInventory.DataAccess.Repository;
using BasicInventory.Entities;
using BasicInventory.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicInventory.Application.Model.StockDTO;

namespace BasicInventory.Application.Service
{
    public class StockService : IStockService
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IClaimService claimService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _claimService = claimService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ViewStockDTO>> GetAll(string keyword)
        {
            IEnumerable<Stock> stockList;

            if (string.IsNullOrEmpty(keyword))
                stockList = await _unitOfWork.Stock.GetAll(u => u.IsActive == true, includeProperties: "Item");
            else
                stockList = await _unitOfWork.Stock.GetAll(u => u.IsActive == true && u.Item.Name.Contains(keyword), includeProperties: "Item");

            foreach(var stock in stockList)
            {
                _mapper.Map<ItemDTO>(stock.Item);
            }

            return _mapper.Map<IEnumerable<ViewStockDTO>>(stockList);
        }

        public async Task<ViewStockDTO> GetById(int id)
        {
            var stock = await _unitOfWork.Stock.GetFirstOrDefault(i => i.Id == id && i.IsActive, includeProperties: "Item");
            _mapper.Map<ItemDTO>(stock.Item);
            return _mapper.Map<ViewStockDTO>(stock);
        }

        public async Task<StockDTO> Create(CreateStockDTO obj)
        {
            var stock = _mapper.Map<Stock>(obj);
            stock.CreatedBy = _claimService.GetUserId();
            stock.AvailableQuantity = stock.Quantity;
            var newStock = await _unitOfWork.Stock.Add(stock);
            await _unitOfWork.Save();

            return _mapper.Map<StockDTO>(newStock);
        }

        public async Task Delete(int id)
        {
            var stock = await _unitOfWork.Stock.GetFirstOrDefault(u => u.Id == id);

            if (stock != null)
            {
                stock.UpdatedBy = _claimService.GetUserId();
                _unitOfWork.Stock.Remove(stock);
                await _unitOfWork.Save();
            }
        }

        public async Task<StockDTO> IncrementStock(int id, decimal quantity)
        {
            var existingStock = await _unitOfWork.Stock.GetFirstOrDefault(s => s.Id == id);
            existingStock.AvailableQuantity += quantity;
            existingStock.UpdatedBy = _claimService.GetUserId();

            var stock = _mapper.Map<Stock>(existingStock);
            var updatedStock = _unitOfWork.Stock.Update(stock);
            await _unitOfWork.Save();
            return _mapper.Map<StockDTO>(updatedStock);
        }

        public async Task<StockDTO> DecrementStock(int id, decimal quantity)
        {
            var existingStock = await _unitOfWork.Stock.GetFirstOrDefault(s => s.Id == id);
            existingStock.AvailableQuantity -= quantity;
            existingStock.UpdatedBy = _claimService.GetUserId();

            var stock = _mapper.Map<Stock>(existingStock);
            var updatedStock = _unitOfWork.Stock.Update(stock);
            await _unitOfWork.Save();
            return _mapper.Map<StockDTO>(updatedStock);
        }
    }
}
