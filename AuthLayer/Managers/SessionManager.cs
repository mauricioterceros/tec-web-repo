using System;
using System.Collections.Generic;

namespace AuthLayer
{
    public class SessionManager : ISessionManager
    {
        private List<Session> _sessions { get; set; }
        
        // public SessionManager(IFBService fbService)
        public SessionManager() 
        {
            _sessions = new List<Session>() 
            {
                new Session() { UserName="mterceros", Password="123456", Role="Admin" }
            };
        }

        public Session ValidateCredentials(string userName, string password) 
        {
            // return _fbService.validateUser(userName, password);
            return _sessions.Find(session => session.UserName == userName && session.Password == password);
        }
    }
}
