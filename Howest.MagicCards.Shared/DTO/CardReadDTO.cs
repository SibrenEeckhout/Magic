using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO;

public record CardReadDTO()
{
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
    public string Type { get; init; }
    [Required]
    [MaxLength(100)]
    public string Text { get; init; }



}
