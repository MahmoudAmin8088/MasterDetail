using AutoMapper;
using MasterDetails.Core.Models;
using MasterDetails.Core.Models.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.Mapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Item,ItemDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

        }
    }
}
