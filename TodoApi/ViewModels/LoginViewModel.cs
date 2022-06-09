using Flunt.Notifications;
using Flunt.Validations;

namespace TodoApi.ViewModels
{
    public class LoginViewModel : Notifiable
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;

            AddNotifications(new Contract().Requires().IsEmail(Email, "Email", "E-mail inválido").IsNotNull(Password, "Password", "Insira sua senha"));
        }
    }
}
