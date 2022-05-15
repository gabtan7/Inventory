
using BasicInventory.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicInventory.Application.Model.ItemDTO;

namespace BasicInventory.Application.Service.IService
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAll(string keyword);
        Task<ItemDTO> GetById(int id);
        Task<ItemDTO> Create(CreateItemDTO obj);

        Task<ItemDTO> Update(UpdateItemDTO obj);
        Task Delete (int id);
    }
}
