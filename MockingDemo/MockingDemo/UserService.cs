using System;

namespace MockingDemo
{
    public class UserService
    {
        private readonly IUserDataService _userDataService;

        public UserService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public UserInfo Get(string email)
        {
            var user = _userDataService.GetByEmail(email);

            return MapFrom(user);
        }

        public UserInfo Create(string name, string email)
        {
            var user = _userDataService.GetByEmail(email);

            if (user != null)
                throw new Exception(string.Format("{0} already exists", email));

            user = _userDataService.Create(GuidProvider.Current.Id, name, email);

            return MapFrom(user);
        }

        public void Delete(string email)
        {
            _userDataService.Delete(email);
        }

        private UserInfo MapFrom(User user)
        {
            if (user == null)
                return null;
            
            return new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}