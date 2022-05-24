using Logic.Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public interface IUserManger
    {
        public List<User> GetUsers();
        public User PostUser(User user);
        public User PutUser(User user);
        public User DeleteUser(Guid userId);
    }
}