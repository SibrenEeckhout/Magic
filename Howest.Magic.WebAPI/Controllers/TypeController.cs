using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraphQLParser;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.DTO;
using Type = Howest.MagicCards.DAL.Models.Type;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository typeRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public TypeController(ITypeRepository typeRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            this.typeRepository = typeRepository;
            this._mapper = mapper;
            this._cache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeReadDTO>>> GetCardTypes()
        {
            IEnumerable<TypeReadDTO> typeReadDtos = null;
            if (!_cache.TryGetValue("cardTypes", out IEnumerable<TypeReadDTO> cachedTypeReadDTOs))
            {
                IQueryable<Type> allTypes = await typeRepository.GetAllCardTypes();

                typeReadDtos = allTypes
                    .ProjectTo<TypeReadDTO>(_mapper.ConfigurationProvider)
                    .ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
                };

                _cache.Set("cardTypes", typeReadDtos, cacheOptions);
            }
            else
            {
                typeReadDtos = cachedTypeReadDTOs;
            }

            return Ok(typeReadDtos);
        }


        [HttpGet("{id:int}", Name = "Type")]
        public async Task<ActionResult<TypeReadDTO>> getType(int id)
        {
            return (await typeRepository.GetType(id) is Type foundType)
                ? Ok(_mapper.Map<TypeReadDTO>(foundType))
                : NotFound("not found");
        }
    }
}
