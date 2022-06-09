namespace TodoApi.Entities
{
    public class Todo : Entity
    {
        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime Date { get; private set; }
        public int UserId { get; private set; }

        public Todo(string title, DateTime date, int userId)
        {
            Title = title;
            Date = date.Date;
            UserId = userId;
            Done = false;
        }

        public void MarkAsDone() => Done = true;
        public void MarkAsUndone() => Done = false;
        public void UpdateTitle(string title) => Title = title;
    }
}
