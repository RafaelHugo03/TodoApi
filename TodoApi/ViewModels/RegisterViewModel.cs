using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApi.ViewModels
{
    public class RegisterViewModel : Notifiable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterViewModel(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

            AddNotifications(new Contract().Requires().IsEmail(Email, "Email", "E-mail inválido")
                .IsGreaterThan(Password.Length, 8, "Password", "senha deve ser maior que 8 caracteres")
                .IsNotNull(FirstName, "FirstName", "O nome não pode ser nulo")
                .IsNotNull(LastName, "LastName", "O sobrenome não pode ser nulo"));
        }
    }
}
