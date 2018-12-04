using System;
using System.Collections.Generic;
using System.Linq;
using CalorieCounter.Infrastructure.EF;
using CalorieCounter.Infrastructure.GraphQL.Types;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.GraphQL.Queries
{
    public class GraphQLQuery : ObjectGraphType
    {
        public GraphQLQuery(CalorieCounterContext efContext)
        {
            Field<ProductType>("product", 
            arguments: new QueryArguments(new QueryArgument<IdGraphType> {Name ="id"}, new QueryArgument<StringGraphType> {Name = "name"}),
            resolve: context => 
            {
                var id = context.GetArgument<Guid>("id");
                var name = context.GetArgument<string>("name");
                return efContext.Products.FirstOrDefault(x=>x.Id==id || x.Name.Contains(name));
            }
            );

            Field<ListGraphType<ProductType>>("products",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> {Name = "name"}, new QueryArgument<IntGraphType> {Name = "takeFirst"}),
                resolve: context => {
                    var takeFirst = context.GetArgument<int>("takeFirst");
                    var name = context.GetArgument<string>("name");

                    return efContext.Products.Where(x=>x.Name.Contains(name)).Take(takeFirst).ToList();
                }
            );

        }
    }
}