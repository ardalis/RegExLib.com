using AutoMapper;
using RegExLib.Core.Entities;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionResult : CreateExpressionCommand
  {
    public int Id { get; set; }

  }
}
