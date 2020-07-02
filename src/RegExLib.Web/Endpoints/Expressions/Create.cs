using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using RegExLib.SharedKernel.Interfaces;
using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class Create : BaseAsyncEndpoint<CreateExpressionCommand, CreateExpressionResult>
  {
    private readonly IRepository _repository;

    public Create(IRepository repository)
    {
      _repository = repository;
    }

    [HttpPost("/api/expressions")]
    [SwaggerOperation(
      Summary = "Creates a new Expression",
      Description = "Creates a new Expression",
      OperationId = "Expression.Create",
      Tags = new[] { "ExpressionEndpoint" })
    ]
    public override async Task<ActionResult<CreateExpressionResult>> HandleAsync([FromBody] CreateExpressionCommand request)
    {
      var expression = ExpressionDTO.ToExpression(request.ExpressionDto);
      expression = await _repository.AddAsync(expression);

      var result = new CreateExpressionResult(ExpressionDTO.FromExpression(expression));
      return Ok(result);
    }
  }
}
