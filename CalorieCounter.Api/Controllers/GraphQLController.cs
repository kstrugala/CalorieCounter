using System.Threading.Tasks;
using CalorieCounter.Infrastructure.GraphQL;
using CalorieCounter.Infrastructure.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Api.Controllers
{
    public class GraphQLController : Controller
    {
        private readonly GraphQLQuery _graphQLQuery;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(GraphQLQuery graphQLQuery, IDocumentExecuter documentExecuter, ISchema schema)
        {
            _graphQLQuery = graphQLQuery;
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        [HttpPost("/graphql")]
        public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        {
            var executionOptions = new ExecutionOptions { Schema = _schema, Query = query.Query, UserContext = User };
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }
    }
}