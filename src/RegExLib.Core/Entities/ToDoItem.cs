using RegExLib.Core.Events;
using RegExLib.SharedKernel;

namespace RegExLib.Core.Entities
{
  public class ToDoItem : BaseEntity
  {
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; private set; }

    public void MarkComplete()
    {
      IsDone = true;

      Events.Add(new ToDoItemCompletedEvent(this));
    }

    public override string ToString()
    {
      string status = IsDone ? "Done!" : "Not done.";
      return $"{Id}: Status: {status} - {Title} - {Description}";
    }
  }
}
