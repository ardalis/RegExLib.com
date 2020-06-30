namespace RegExLib.Web.Endpoints.ToDoItems
{
    public class ToDoItemResponse
    {
        public int Id { get; set; }
#nullable disable
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string Title { get; set; }
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public bool IsDone { get; set; }
    }
}