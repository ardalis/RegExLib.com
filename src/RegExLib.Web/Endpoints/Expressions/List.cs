using Ardalis.ApiEndpoints;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class List : BaseAsyncEndpoint<List<ExpressionResponse>>
  {
    private readonly IRepository _repository;

    public List(IRepository repository)
    {
      _repository = repository;
    }

    [HttpGet("/Expressions")]
    [SwaggerOperation(
        Summary = "Gets a list of all Expressions",
        Description = "Gets a list of all Expressions",
        OperationId = "Expression.List",
        Tags = new[] { "ExpressionEndpoints" })
    ]
    public override async Task<ActionResult<List<ExpressionResponse>>> HandleAsync()
    {
      var expressions = (await _repository.ListAsync<Expression>())
          .Select(expression => new ExpressionResponse(expression.Id, expression.Title, expression.Pattern, expression.Description, expression.AuthorId));

      return Ok(expressions);
    }
  }
}
