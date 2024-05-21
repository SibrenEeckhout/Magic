using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record CardReadDetailDTO
    {
        [Required]
        public long Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Name { get; init; }

        [Required]
        public string SetCode { get; init; }

        [Required]
        public long? ArtistId { get; init; }
        [Required]
        public string RarityCode { get; init; }

        [Required]
        [MaxLength(100)]
        public string Type { get; init; }

        [Required]
        [MaxLength(255)]
        public string Text { get; init; }
        [Required]
        public string Flavor { get; init; }
        [Required]
        public string Number { get; init; }
        [Required]
        public string Power { get; init; }
        [Required]
        public string Toughness { get; init; }
        [Required]
        public string OriginalText { get; init; }




    }
}
