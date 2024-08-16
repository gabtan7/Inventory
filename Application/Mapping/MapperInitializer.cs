using AutoMapper;
using Application.Model;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Model.StockDTO;

namespace Application.Mapping
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
