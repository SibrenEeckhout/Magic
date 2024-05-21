using GraphQL.Types;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;

namespace Howest.MagicCards.GraphQL.GraphQl.Types
{
    public class ArtistType : ObjectGraphType<Artist>
    {
        public ArtistType() {
            Name = "Artist";

            Field(artist => artist.Id, type: typeof(LongGraphType))
                .Description("Id of the artist.")
                .Name("Id");
            Field(artist => artist.FullName, type: typeof(StringGraphType))
                .Description("Name of the artist.")
                .Name("Name");
            Field<ListGraphType<CardType>>("cards",
                resolve: context =>
                {
                    var cardRepository = (ICardRepository)context.RequestServices.GetService(typeof(ICardRepository));
                    var card = context.Source;
                    return cardRepository.GetCardsByArtist((int)card.Id);
                });
        }
    }
}
