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

namespace Howest.MagicCards.WebAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository artistRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ArtistController(IArtistRepository artistRepository, IMapper mapper, IMemoryCache cache)
        {
            this.artistRepository = artistRepository;
            this._mapper = mapper;
            this._cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistReadDTO>>> GetCards()
        {
            IEnumerable<ArtistReadDTO> artistReadDtos = null;

            if (!_cache.TryGetValue("artists", out IEnumerable<ArtistReadDTO> cachedArtists))
            {
                IQueryable<Artist> allArtists = await artistRepository.GetAllArtists();

                artistReadDtos = allArtists
                    .ProjectTo<ArtistReadDTO>(_mapper.ConfigurationProvider)
                    .ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
                };

                _cache.Set("artists", artistReadDtos, cacheOptions);
            }
            else
            {
                artistReadDtos = cachedArtists; 
            }

            return Ok(artistReadDtos);
        }


        [HttpGet("{id:int}", Name = "Artist")]
        public async Task<ActionResult<ArtistReadDTO>> GetCard(int id)
        {
            return (await artistRepository.GetArtistById(id) is Artist foundArtist)
                ? Ok(_mapper.Map<ArtistReadDTO>(foundArtist))
                : NotFound("not found");
        }
    }
}
