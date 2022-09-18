using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RegExLib.BlazorShared;
using RegExLib.BlazorShared.Dtos;
using RegExLib.Core.Constants;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Web.Endpoints.Expressions
{
  [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]

  public class Create : EndpointBaseAsync
    .WithRequest<CreateExpressionCommand>
    .WithActionResult<Result>
  {
    private readonly IRepository<Expression> _repository;
    private readonly IMapper _mapper;

    public Create(IRepository<Expression> repository, IMapper mapper)
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
    public override async Task<ActionResult<Result>> HandleAsync([FromBody] CreateExpressionCommand request, CancellationToken cancellationToken = default)
    {
      var createdExpression = _mapper.Map<Expression>(request);
      createdExpression = await _repository.AddAsync(createdExpression, cancellationToken);
      var responseExpression = _mapper.Map<ExpressionDto>(createdExpression);

      return Ok(Result.SuccessWithDataResult(responseExpression));
    }
  }
}
