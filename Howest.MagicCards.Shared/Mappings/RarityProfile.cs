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
    public class RarityProfile : Profile
    {
        public RarityProfile()
        {

            CreateMap<Rarity, RarityReadDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Code, opt => opt.MapFrom(c => c.Code))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name));
        }
    }
}
