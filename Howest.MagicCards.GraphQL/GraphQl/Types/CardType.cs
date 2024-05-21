using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQl.Types
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType() {
            Name = "Card";

            Field(card => card.Id, type: typeof(IntGraphType))
                .Description("Id of the card.")
                .Name("Id");
            Field(card => card.Name, type: typeof(StringGraphType))
                .Description("Name of the card.")
                .Name("Name");
            Field(card => card.SetCode, type: typeof(StringGraphType))
                .Description("SetCode of the card.")
                .Name("SetCode");
            Field(card => card.ArtistId, type: typeof(IntGraphType))
                .Description("ArtistId of the card.")
                .Name("ArtistId");
            Field(card => card.RarityCode, type: typeof(StringGraphType))
                .Description("RarityCode of the card.")
                .Name("RarityCode");
            Field(card => card.Type, type: typeof(StringGraphType))
                .Description("Type of the card.")
                .Name("Type");
            Field(card => card.Text, type: typeof(StringGraphType))
                .Description("Text of the card.")
                .Name("Text");
            Field<ArtistType, Artist>()
            .Name("Artist")
            .Description("The artist who created the card.")
            .ResolveAsync(async context =>
            {
                var artistRepository = (IArtistRepository)context.RequestServices.GetService(typeof(IArtistRepository));
                var card = context.Source;
                return await artistRepository.GetArtistById((int)card.ArtistId);
            });
        }
    }
}
