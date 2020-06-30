using RegExLib.Core.Entities;
using RegExLib.SharedKernel.Interfaces;
using RegExLib.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace RegExLib.Web.Api
{
  public class ExpressionsController : BaseApiController
  {
    private readonly IRepository _repository;

    public ExpressionsController(IRepository repository)
    {
      _repository = repository;
    }

    // GET: /api/expressions
    [HttpGet]
    public async Task<IActionResult> List()
    {
      var expressions = (await _repository.ListAsync<Expression>())
        .Select(ExpressionDTO.FromExpression);

      return Ok(expressions);
    }

    // GET: /api/expressions?page=0
    [HttpGet("{page:int}")]
    public async Task<IActionResult> List(int page)
    {
      var expressions = (await _repository.ListAsync<Expression>(page))
        .Select(ExpressionDTO.FromExpression);

      return Ok(expressions);
    }

  }
}
