using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RegExLib.Core.Constants;
using RegExLib.SharedKernel.Interfaces;

namespace RegExLib.Web.Endpoints.Expressions
{
  [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class Create : BaseAsyncEndpoint<CreateExpressionCommand, CreateExpressionResult>
  {
    private readonly IRepository _repository;
    private readonly CreateExpressionAssembler _assembler;

    public Create(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _assembler = new CreateExpressionAssembler(mapper);
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
      
      var createdExpression = _assembler.WriteEntity(request);
      createdExpression = await _repository.AddAsync(createdExpression);

      return Ok(_assembler.FromExpression(createdExpression));
    }
  }
}
