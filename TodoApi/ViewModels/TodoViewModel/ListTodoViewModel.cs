using TodoApi.Entities;

namespace TodoApi.ViewModels.TodoViewModel
{
    public class ListTodoViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }
        public string UserName { get; set; }
        public List<Todo> Todos { get; set; }

        public ListTodoViewModel(string title, DateTime date,bool done, string userName)
        {
            Title = title;
            Date = date.Date;
            UserName = userName;
            Done = done;
        }
    }
}
