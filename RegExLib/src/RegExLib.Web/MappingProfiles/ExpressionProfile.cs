using RegExLib.Core.Entities;
using AutoMapper;
using RegExLib.BlazorShared.Dtos;
using RegExLib.Web.Endpoints.Expressions;

namespace RegExLib.Web.MappingProfiles
{
  public class ListExpressionProfile : Profile
  {
    public ListExpressionProfile()
    {
      CreateMap<Expression, ExpressionDto>();
      CreateMap<CreateExpressionCommand, Expression>();
    }
  }
}
