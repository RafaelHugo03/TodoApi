using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApi.ViewModels.TodoViewModel
{
    public class CreateTodoViewModel : Notifiable
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public CreateTodoViewModel(string title, DateTime date)
        {
            Title = title;
            Date = date;

            AddNotifications(new Contract().Requires().IsGreaterThan(Title.Length, 3, "Title", "O título deve ser maior que 3 caracteres")
                .IsNotNull(Date, "Date", "Informe a data de término da tarefa"));
        }
    }
}
