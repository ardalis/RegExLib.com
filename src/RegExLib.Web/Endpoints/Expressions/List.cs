using Ardalis.ApiEndpoints;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class List : BaseAsyncEndpoint<int, PagedExpressionResult>
  {
    private readonly IRepository _repository;

    public List(IRepository repository)
    {
      _repository = repository;
    }


    [HttpGet("/Expressions")]
    [SwaggerOperation(
        Summary = "Gets a list of all Expressions by page every page is 25 records",
        Description = "Gets a list of all Expressions by page every page is 25 records",
        OperationId = "Expression.List",
        Tags = new[] { "ExpressionEndpoints" })
    ]
    public override async Task<ActionResult<PagedExpressionResult>> HandleAsync([FromQuery] int page)
    {
      var expressions = await _repository.ListAsync<Expression>(page);
      var result = new PagedExpressionResult(page, expressions.Count, expressions.Select(ExpressionDTO.FromExpression));

      return Ok(result);
    }
  }
}
