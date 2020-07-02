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
      var createdExpression = ExpressionDTO.ToExpression(request.ExpressionDto);
      createdExpression = await _repository.AddAsync(createdExpression);

      var expressionDto = ExpressionDTO.FromExpression(createdExpression);
      var result = new CreateExpressionResult
      {
        ExpressionDto = expressionDto
      };

      return Ok(result);
    }
  }
}
