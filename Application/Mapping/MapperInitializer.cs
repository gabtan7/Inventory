using AutoMapper;
using BasicInventory.Application.Model;
using BasicInventory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BasicInventory.Application.Model.StockDTO;

namespace BasicInventory.Application.Mapping
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

            CreateMap<Item, ItemDTO>().ReverseMap();

            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<Stock, ViewStockDTO>().ReverseMap();
        }
    }
}
