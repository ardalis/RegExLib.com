using RegExLib.Core.Entities;
using AutoMapper;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class ListExpressionProfile : Profile
  {
    public ListExpressionProfile()
    {
      CreateMap<Expression, ExpressionDTO>();
    }
  }
}
