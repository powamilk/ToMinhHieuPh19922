using AppData.Entities;
using AppData.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Mapper
{
    public class ThucAnProfile : Profile
    {
        public ThucAnProfile()
        {
            CreateMap<ThucAnViewModel, ThucAn>()
                .ForMember(dest => dest.ID, opt => opt.Ignore());

            CreateMap<ThucAn, ThucAnViewModel>();
        }
    }
}
