using System;
using System.Collections.Generic;
using Logic.Models;
using Services;
using DBLayer.Models;
using DBLayer;

namespace Logic
{
    public class UserManager : IUserManger
    {
        public List<Logic.Models.User> Users { get; set; }
        private IdNumberService _idNumberService;
        private IUnitOfWork _uow;

        // public UserManager(IDbLayer dbLayer)
        public UserManager(IdNumberService idNumberService, IUnitOfWork uow)
        {
            _uow = uow;
            _idNumberService = idNumberService;
            Users = new List<Logic.Models.User>()
            {
                new Logic.Models.User() { Name = "Mauricio" },
                new Logic.Models.User() { Name = "Antonio" }
            };
        }
        public List<Logic.Models.User> GetUsers()
        {
            // List<DBLayer.Models.User> users = _dbLayer.GetUser();
            // return Users;
            List<DBLayer.Models.User> users = _uow.UserRepository.GetAll().Result;
            
            List<Logic.Models.User> usersConverted = new List<Models.User>();
            foreach (DBLayer.Models.User item in users)
            {
                usersConverted.Add(new Logic.Models.User() { Name = item.Name, LastName = item.LastName, Id = item.Id });
            }

            return usersConverted;
        }

        public Logic.Models.User PostUser(Logic.Models.User user)
        {
            /* 
            int ciParsed = 0;
            if (Int32.TryParse(user.CI, out ciParsed))
            {
                throw new InvalidCIException();
            }
            */

            /* string idNumber = _idNumberService.GetIdNumberServiceAsync().Result;
            user.CI = idNumber;
            Users.Add(user);
            return user; */

            DBLayer.Models.User userConverted = new DBLayer.Models.User()
            {
                Name = user.Name,
                LastName = user.LastName,
                Id = new Guid()
            };
            userConverted = _uow.UserRepository.CreateUser(userConverted);
            _uow.Save();

            return user;
        }

        public Logic.Models.User PutUser(Logic.Models.User user)
        {
            DBLayer.Models.User userFound = _uow.UserRepository.GetById(user.Id);

            userFound.Name = user.Name;
            userFound.LastName = user.LastName;

            _uow.UserRepository.UpdateUser(userFound);
            _uow.Save();

            return user;
        }

        public Logic.Models.User DeleteUser(Guid userId)
        {
            DBLayer.Models.User userFound = _uow.UserRepository.GetById(userId);

            _uow.UserRepository.DeleteUser(userFound);
            _uow.Save();

            return new Logic.Models.User() { Name = userFound.Name, LastName = userFound.LastName, Id = userFound.Id };
        }
    }
}
