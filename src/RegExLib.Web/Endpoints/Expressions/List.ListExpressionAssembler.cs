using AutoMapper;
using RegExLib.Core.Entities;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class ListExpressionAssembler
  {
    private readonly IMapper _mapper;

    public ListExpressionAssembler(IMapper mapper)
    {
      _mapper = mapper;
    }

    public ExpressionDTO WriteDto(Expression expression)
    {
      var result = new ExpressionDTO();
      _mapper.Map(expression, result);

      return result;
    }
  }
}
