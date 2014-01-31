using System;

namespace MockingDemo
{
    public interface IUserDataService
    {
        User GetByEmail(string email);
        User Create(Guid id, string name, string email);
        void Delete(string email);
    }
}