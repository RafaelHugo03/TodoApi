namespace TodoApi.Entities
{
    public class User : Entity
    {
        private readonly IList<Todo> _todos;
        private readonly IList<Role> _roles;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public IReadOnlyCollection<Todo> Todos { get { return _todos.ToArray(); } private set { } }
        public IReadOnlyCollection<Role> Roles { get { return _roles.ToArray(); } private set { } }

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

            _todos = new List<Todo>();
            _roles = new List<Role>();
        }
    }
}
