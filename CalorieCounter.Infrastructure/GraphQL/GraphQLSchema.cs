using System;
using CalorieCounter.Infrastructure.GraphQL.Queries;
using GraphQL.Types;

namespace CalorieCounter.Infrastructure.GraphQL
{
    public class GraphQLSchema : Schema
{
    public GraphQLSchema(Func<Type, GraphType> resolveType)
        : base(resolveType)
    {
        Query = (GraphQLQuery)resolveType(typeof(GraphQLQuery));
    }
}
}