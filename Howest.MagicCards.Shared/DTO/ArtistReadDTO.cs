using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record ArtistReadDTO
    {
        [Required]
        public long Id { get; init; }

        [Required]
        [MaxLength(100)]
        public String FullName { get; init; }
    }
}
