using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class RarityController : ControllerBase
    {
        private readonly IRarityRepository rarityRepository;
        private readonly IMapper _mapper;

        public RarityController(IRarityRepository rarityRepository, IMapper mapper)
        {
            this.rarityRepository = rarityRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RarityReadDTO>>> GetCards()
        {
            return (await rarityRepository.GetAllRarities() is IQueryable<Rarity> allRarities)
                ? Ok((
                    allRarities
                    .ProjectTo<RarityReadDTO>(_mapper.ConfigurationProvider)
                    .ToList()))
                : NotFound("not found");
        }

        [HttpGet("{id:int}", Name = "Rarity")]
        public async Task<ActionResult<RarityReadDTO>> GetCard(int id)
        {
            return (await rarityRepository.GetRarityById(id) is Rarity foundRarity)
                ? Ok(_mapper.Map<RarityReadDTO>(foundRarity))
                : NotFound("not found");
        }
    }
}
