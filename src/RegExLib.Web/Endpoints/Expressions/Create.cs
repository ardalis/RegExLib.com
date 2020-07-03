using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using AutoMapper;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class Create : BaseAsyncEndpoint<CreateExpressionCommand, CreateExpressionResult>
  {
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public Create(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
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
      var createdExpression = request.ToExpression(_mapper);
      createdExpression = await _repository.AddAsync(createdExpression);

      return Ok(CreateExpressionResult.FromExpression(_mapper, createdExpression));
    }
  }
}
