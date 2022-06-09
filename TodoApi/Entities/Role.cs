namespace TodoApi.Entities
{
    public class Role : Entity
    {
        private readonly IList<User> _users;
        public string Name { get; private set; }
        public string Slug { get; set; }

        public IReadOnlyCollection<User> Users { get { return _users.ToArray(); } private set { } }

        public Role(string name)
        {
            Name = name;
            Slug = name.Replace(" ","-").ToLower();

            _users = new List<User>();
        }
    }
}
