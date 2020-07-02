using RegExLib.Core.Entities;
using AutoMapper;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class ExpressionProfile : Profile
  {
    public ExpressionProfile()
    {
      CreateMap<CreateExpressionCommand, Expression>();
      CreateMap<Expression, CreateExpressionResult>();
    }
  }
}
