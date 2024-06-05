using System.Collections.Generic;

namespace Post.Classes
{
    public class User
    {
        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }
        public string Password { get; }
        public List<Parcel>? Parcels { get; }

        public User(long id, string firstName, string lastName, string login, string password, List<Parcel>? parcels)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Parcels = parcels;
        }

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}