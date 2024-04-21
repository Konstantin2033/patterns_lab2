using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Post.Classes
{
    public class User
    {
        private long id;
        private string firstName;
        private string lastName;
        private string login;
        private string password;
        private List<Parcel>? parcels;

        public User(long id, string firstName, string lastName, string login, string password, List<Parcel>? parcels)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.login = login;
            this.password = password;
            this.parcels = parcels;
        }

        public long Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        internal List<Parcel>? Parcels { get => parcels; set => parcels = value; }

        public string GetFullName() { return $"{FirstName} {LastName}"; }
    }
}
