using GPSsystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private string address;
        private string token;
        private bool isLoggedIn = false;

        public User(string token)
        {
            this.token = token;
        }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public User(string username, string password, string email, string address)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.address = address;
        }

        public bool ToRegister() 
        {
            if (string.IsNullOrEmpty(this.email) || string.IsNullOrEmpty(this.password) || string.IsNullOrEmpty(this.username) || string.IsNullOrEmpty(this.address))
            {
                return false;
            }

            using (var context = new gpssystemContext())
            {
                Data.User user = new Data.User { Name = this.username, Address = this.address, Email = this.email, Password = this.password };
                context.Users.Add(user);
                context.SaveChanges();
            }

            return true;
        }

        public Data.User ToLogin()
        {
            if (string.IsNullOrEmpty(this.email) || string.IsNullOrEmpty(this.password)) {
                return null;
            }

            using (var context = new gpssystemContext()) {
                var user = context.Users.FirstOrDefault(u => u.Email.Equals(this.email) && u.Password.Equals(this.password));

                if (user != null) {
                    return user;
                }
            }

            return null;
        }

        public string GetAdminToken()
        {
            string tokenString = string.Empty;

            if (this.email.Equals("admin") && this.password.Equals("admin"))
            {
                tokenString = Guid.NewGuid().ToString();
                DateTime startTime = DateTime.UtcNow;
                using (var context = new gpssystemContext())
                {
                    Data.Token token = new Data.Token { Token1 = tokenString, StartTime = startTime };
                    context.Tokens.Add(token);
                    context.SaveChanges();
                }
            }

            return tokenString;
        }

        public bool CheckAdminToken()
        {
            bool isValid = false;

            using (var context = new gpssystemContext())
            {
                Data.Token token1 = context.Tokens.FirstOrDefault(m => m.Token1 == this.token);
                if (token1 != null) {
                    isValid = DateTime.UtcNow <= token1.StartTime.AddHours(2);
                }
            }

            return isValid;
        }
    }
}
