
using Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Model.StockDTO;

namespace Application.Service.IService
{
    public interface IStockService
    {
        Task<IEnumerable<ViewStockDTO>> GetAll(string keyword);
        Task<ViewStockDTO> GetById(int id);
        Task<StockDTO> Create(CreateStockDTO obj);
        Task Delete(int id);
        Task<StockDTO> IncrementStock(int id, decimal quantity);
        Task<StockDTO> DecrementStock(int id, decimal quantity);
    }
}
