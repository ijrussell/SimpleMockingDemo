using System;
using System.Collections.Generic;

namespace MockingDemo
{
    public class User : IEquatable<User>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Name == other.Name &&
                   Email == other.Email;
        }

        public override int GetHashCode()
        {
            var hashCode = 1231508573;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

        public static bool operator ==(User user1, User user2)
        {
            return EqualityComparer<User>.Default.Equals(user1, user2);
        }

        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }
    }
}