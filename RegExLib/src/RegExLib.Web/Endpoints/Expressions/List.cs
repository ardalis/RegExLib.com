using Ardalis.ApiEndpoints;
using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using RegExLib.BlazorShared;
using RegExLib.BlazorShared.Dtos;
using RegExLib.Core.Specifications;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class List : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<Result>
  {
    private readonly IRepository<Expression> _repository;
    private readonly IMapper _mapper;

    public List(IRepository<Expression> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }


    [HttpGet("/api/Expressions")]
    [SwaggerOperation(
        Summary = "Gets a list of all Expressions by page every page is 25 records",
        Description = "Gets a list of all Expressions by page every page is 25 records",
        OperationId = "Expression.List",
        Tags = new[] { "ExpressionEndpoints" })
    ]
    public override async Task<ActionResult<Result>> HandleAsync([FromQuery] int page, CancellationToken cancellationToken = default)
    {
      var expressionFilter = new ExpressionFilter();
      var spec = new ExpressionsByFilterSpec(expressionFilter);
      var expressionsCount = await _repository.CountAsync(spec, cancellationToken);

      expressionFilter.Page = page;
      spec = new ExpressionsByFilterSpec(expressionFilter);
      var expressions = await _repository.ListAsync(spec, cancellationToken);

      var expressionResponse = _mapper.Map<List<ExpressionDto>>(expressions);

      var response = Result.SuccessWithDataResult(expressionsCount, expressionResponse);

      return Ok(response);
    }
  }
}
