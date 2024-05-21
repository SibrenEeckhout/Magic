using GraphQL;
using GraphQL.Types;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.GraphQL.GraphQl.Types;

namespace Howest.MagicCards.GraphQL.GraphQl.Query
{
    public class RootQuery : ObjectGraphType
    {

        public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
        {
            Name = "Query";

            Field<CardType>(
                 "cardById",
                  Description = "Get card by id.",
                 arguments: new QueryArguments
                 {
                     new  QueryArgument<NonNullGraphType<IntGraphType>> { Name = "Id" }
                 },
                 resolve: context =>
                {
                   int Id = context.GetArgument<int>("Id");
                   return cardRepository.GetCardById(Id);
                }
                );

            Field<ListGraphType<CardType>>(
                "AllCards",
                Description = "Get all cards.",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> {Name = "amount"}
                },
                resolve: context =>
                {
                    int amount = context.GetArgument<int>("amount");
                    var allCardsTask = cardRepository.GetAllCards();
                    allCardsTask.Wait();
                    var allCards = allCardsTask.Result;
                    if (amount == 0)
                    {
                        return allCards;
                    }
                    return allCards.Take(amount);
                });


            Field<ArtistType>(
                "ArtistById",
                Description = "Get Artist by id.",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "Id"}
                },
                resolve: context =>
                {
                    int Id = context.GetArgument<int>("Id");
                    return artistRepository.GetArtistById(Id);
                });

            Field<ListGraphType<ArtistType>>(
                "AllArtists",
                Description = "Get all artists.",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType> {Name = "amount"}
                },
                resolve: context =>
                {
                    int amount = context.GetArgument<int>("amount");
                    var allArtistsTask = artistRepository.GetAllArtists();
                    allArtistsTask.Wait(); // Wait for the task to complete
                    var allArtists = allArtistsTask.Result;
                    if (amount == 0)
                    {
                        return allArtists;
                    }
                    return allArtists.Take(amount);
                });
        }
    }
}
