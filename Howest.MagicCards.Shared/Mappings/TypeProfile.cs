using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Howest.MagicCards.DAL.Models.Type;


namespace Howest.MagicCards.Shared.Mappings
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {

            CreateMap<Type, TypeReadDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Type1, opt => opt.MapFrom(c => c.Type1));
        }
    }
}
