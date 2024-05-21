using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class CardsProfile : Profile
    {

        public CardsProfile() {

            CreateMap<Card, CardReadDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name));
        }

    }
}
