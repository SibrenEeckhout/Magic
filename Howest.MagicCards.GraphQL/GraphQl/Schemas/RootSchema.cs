using Howest.MagicCards.GraphQL.GraphQl.Query;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Types;

namespace Howest.MagicCards.GraphQL.GraphQl.Schemas
{
    public class RootSchema : Schema
    {
        public RootSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
        }
    }
}
