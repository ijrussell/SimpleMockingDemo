using System;

namespace MockingDemo
{
    public class UserService
    {
        private readonly IUserDataService _userDataService;

        public UserService(IUserDataService userDataService)
        {
            _userDataService = userDataService ?? throw new ArgumentNullException(nameof(userDataService));
        }

        public UserInfo Get(string email)
        {
            var user = _userDataService.GetByEmail(email);

            return user != null ? new UserInfo(user) : null;
        }

        public UserInfo Create(string name, string email)
        {
            var user = _userDataService.GetByEmail(email);

            if (user != null)
                throw new Exception($"{email} already exists");

            user = _userDataService.Create(GuidProvider.Current.Id, name, email);

            return new UserInfo(user);
        }

        public void Delete(string email)
        {
            _userDataService.Delete(email);
        }
    }
}