using RegExLib.Core.Entities;
using AutoMapper;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionProfile : Profile
  {
    public CreateExpressionProfile()
    {
      CreateMap<CreateExpressionCommand, Expression>();
      CreateMap<Expression, CreateExpressionResult>();
    }
  }
}
