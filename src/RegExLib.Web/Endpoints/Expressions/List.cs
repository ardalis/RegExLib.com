using Ardalis.ApiEndpoints;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class List : BaseAsyncEndpoint<int, PagedExpressionResult>
  {
    private readonly IRepository _repository;
    private readonly ListExpressionAssembler _assembler;

    public List(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _assembler = new ListExpressionAssembler(mapper);
    }


    [HttpGet("/api/Expressions")]
    [SwaggerOperation(
        Summary = "Gets a list of all Expressions by page every page is 25 records",
        Description = "Gets a list of all Expressions by page every page is 25 records",
        OperationId = "Expression.List",
        Tags = new[] { "ExpressionEndpoints" })
    ]
    public override async Task<ActionResult<PagedExpressionResult>> HandleAsync([FromQuery] int page)
    {
      var expressions = await _repository.ListAsync<Expression>(page);
      var result = new PagedExpressionResult(page, expressions.Count, expressions.Select(_assembler.WriteDto));

      return Ok(result);
    }
  }
}
