using System.ComponentModel.DataAnnotations;
using AutoMapper;
using RegExLib.Core.Entities;
using RegExLib.Web.ApiModels;

namespace RegExLib.Web.Endpoints.Expressions
{
  public class CreateExpressionCommand
  {
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Pattern { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;

    public Expression ToExpression(IMapper mapper)
    {
      var createdExpression = new Expression();
      mapper.Map(this, createdExpression);

      return createdExpression;
    }
  }
}
