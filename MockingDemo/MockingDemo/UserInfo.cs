using System;
using System.Collections.Generic;

namespace MockingDemo
{
    public class UserInfo : IEquatable<UserInfo>
    {
        public UserInfo()
        {
            
        }
        public UserInfo(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserInfo);
        }

        public bool Equals(UserInfo other)
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

        public static bool operator ==(UserInfo info1, UserInfo info2)
        {
            return EqualityComparer<UserInfo>.Default.Equals(info1, info2);
        }

        public static bool operator !=(UserInfo info1, UserInfo info2)
        {
            return !(info1 == info2);
        }
    }
}