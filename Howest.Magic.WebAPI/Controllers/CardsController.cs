using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Howest.MagicCards.Shared.DTO;
using Microsoft.Extensions.Caching.Memory;
using GraphQLParser;

namespace Howest.MagicCards.WebAPI.Controllers.V1
{
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository cardRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            this.cardRepository = cardRepository;
            this._mapper = mapper;
            this._cache = memoryCache;
        }

        
        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCardsWithoutSorting([FromQuery] PaginationFilter paginationFilter, [FromQuery] CardsFilter cardsFilter = null)
        {
            IEnumerable<CardReadDTO> cardReadDtos = null;
            string cacheKey = $"cards_{cardsFilter.Id}_{cardsFilter.Name}_{cardsFilter.SetCode}_{cardsFilter.ArtistId}_{cardsFilter.RarityCode}_{cardsFilter.Type}_{cardsFilter.Text}";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<CardReadDTO> cardReadDTOs))
            {
                IQueryable<Card> allCards = await cardRepository.GetAllCards();
                allCards = allCards.FilterCards(cardsFilter);
                cardReadDtos = allCards
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                    .ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
                };
                _cache.Set(cacheKey, cardReadDtos, cacheOptions);
            }
            else{cardReadDtos = _cache.Get<IEnumerable<CardReadDTO>>(cacheKey);}

            return Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                cardReadDtos,
                paginationFilter.PageNumber,
                paginationFilter.PageSize));
        }


        [HttpGet("{id:int}",Name = "Card")]
        public async Task<ActionResult<CardReadDTO>> GetCard(int id) {
            return ( await cardRepository.GetCardById(id) is Card foundCard)
                ? Ok(_mapper.Map<CardReadDTO>(foundCard))
                : NotFound("not found");
        }
    }
}

namespace Howest.MagicCards.WebAPI.Controllers.V2
{
    [ApiVersion("1.5")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
         private readonly ICardRepository cardRepository;
         private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepository, IMapper mapper, IMemoryCache memoryCache)
          {
             this.cardRepository = cardRepository;
             this._mapper = mapper;
             this._cache = memoryCache;
          }

        [HttpGet("{id:int}/detailed", Name = "CardDetailed")]
        public async Task<ActionResult<CardReadDetailDTO>> GetCardDetailed(int id)
        {
            return (await cardRepository.GetCardById(id) is Card foundCard)
                ? Ok(_mapper.Map<CardReadDetailDTO>(foundCard))
                : NotFound("not found");
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadDTO>>>> GetCards([FromQuery] PaginationFilter paginationFilter, [FromQuery] CardsFilter cardsFilter = null, [FromQuery] string sortMethod = null)
        {
            IEnumerable<CardReadDTO> cardReadDtos = null;
            string cacheKey = $"cards_{cardsFilter.Id}_{cardsFilter.Name}_{cardsFilter.SetCode}_{cardsFilter.ArtistId}_{cardsFilter.RarityCode}_{cardsFilter.Type}_{cardsFilter.Text}_{sortMethod}";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<CardReadDTO> cardReadDTOs))
            {
                IQueryable<Card> allCards = await cardRepository.GetAllCards();

                allCards = allCards.FilterCards(cardsFilter);

                if (!string.IsNullOrEmpty(sortMethod))
                {
                    if (sortMethod.ToUpper() == "DESC")
                    {
                        allCards = allCards.OrderByDescending(c => c.Name);
                    }
                    else if (sortMethod.ToUpper() == "ASC")
                    {
                        allCards = allCards.OrderBy(c => c.Name);
                    }
                }

                cardReadDtos = allCards
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                    .ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
                };

                _cache.Set(cacheKey, cardReadDtos, cacheOptions);
            }
            else
            {
                cardReadDtos = _cache.Get<IEnumerable<CardReadDTO>>(cacheKey);

            }

            return Ok(new PagedResponse<IEnumerable<CardReadDTO>>(
                cardReadDtos,
                paginationFilter.PageNumber,
                paginationFilter.PageSize));
        }

    }

}
