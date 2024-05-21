﻿using AutoMapper;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.DTO;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Mappings
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {

            CreateMap<Artist, ArtistReadDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(c => c.FullName));
        }
    }
}
