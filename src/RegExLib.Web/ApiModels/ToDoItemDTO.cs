using System.ComponentModel.DataAnnotations;
using RegExLib.Core.Entities;

namespace RegExLib.Web.ApiModels
{
    // Note: doesn't expose events or behavior
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        [Required]
#nullable disable
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string Title { get; set; }
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public bool IsDone { get; private set; }

        public static ToDoItemDTO FromToDoItem(ToDoItem item)
        {
            return new ToDoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsDone = item.IsDone
            };
        }
    }
}
