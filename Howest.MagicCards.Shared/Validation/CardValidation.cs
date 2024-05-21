using FluentValidation;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace Howest.MagicCards.Shared.Validation
{
    public class CardValidation: AbstractValidator<CardReadDTO>
    {
        public CardValidation() { 
            RuleFor(card => card.Name).NotEmpty().NotNull().WithMessage("The name of the card can not be blank.");
            RuleFor(card => card.SetCode).NotEmpty().NotNull().WithMessage("The setcode of the card can not be blank.");
            RuleFor(card => card.RarityCode).NotEmpty().NotNull().WithMessage("The RarityCode of the card can not be blank.");
            RuleFor(card => card.Type).NotEmpty().NotNull().WithMessage("The Type of the card can not be blank.");
            RuleFor(card => card.Text).NotEmpty().NotNull().WithMessage("The Type of the card can not be blank.").Length(2,255).WithMessage("The text can not be longer then 255 characters.");
        }
    }
}
