using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RegExLib.Core.Entities;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionAssembler
  {
    private readonly IMapper _mapper;

    public CreateExpressionAssembler(IMapper mapper)
    {
      _mapper = mapper;
    }

    public Expression WriteEntity(CreateExpressionCommand createExpressionCommand)
    {
      var result = new Expression();
      _mapper.Map(createExpressionCommand, result);

      return result;
    }

    public CreateExpressionResult FromExpression(Expression expression)
    {
      var result = new CreateExpressionResult();
      _mapper.Map(expression, result);

      return result;
    }
  }
}
