using AutoMapper;
using Application.Model;
using Application.Service.IService;
using DataAccess.Data;
using DataAccess.Repository;
using Entities;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Model.ItemDTO;

namespace Application.Service
{
    public class ItemService : IItemService
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IClaimService claimService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _claimService = claimService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemDTO> Create(CreateItemDTO obj)
        {
            var item = _mapper.Map<Item>(obj);
            item.CreatedBy = _claimService.GetUserId();
            var newItem = await _unitOfWork.Item.Add(item);
            await _unitOfWork.Save();

            return _mapper.Map<ItemDTO>(newItem);
        }
        public async Task<ItemDTO> Update(UpdateItemDTO obj)
        {
            var item = await _unitOfWork.Item.GetFirstOrDefault(i => i.Id == obj.Id);

            item.Price = obj.Price;
            item.Unit = obj.Unit;
            item.Name = obj.Name;
            item.Description = obj.Description;
            item.UpdatedBy = _claimService.GetUserId();

            //var item = _mapper.Map<Item>(obj);
            var updatedItem = _unitOfWork.Item.Update(item);
            await _unitOfWork.Save();

            return _mapper.Map<ItemDTO>(updatedItem);
        }

        public async Task Delete(int id)
        {
            var item = await _unitOfWork.Item.GetFirstOrDefault(u => u.Id == id);

            if (item != null)
            {
                item.UpdatedBy = _claimService.GetUserId(); 
                _unitOfWork.Item.Remove(item);
                await _unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<ItemDTO>> GetAll(string keyword)
        {
            IEnumerable<Item> itemList;

            if (string.IsNullOrEmpty(keyword))
                itemList = await _unitOfWork.Item.GetAll(i => i.IsActive);
            else
                itemList = await _unitOfWork.Item.GetAll(i => i.Name.Contains(keyword) && i.IsActive);

            return _mapper.Map<IEnumerable<ItemDTO>>(itemList);
        }

        public async Task<ItemDTO> GetById(int id)
        {
            var item = await _unitOfWork.Item.GetFirstOrDefault(i => i.Id == id && i.IsActive);
            return _mapper.Map<ItemDTO>(item);
        }
    }
}