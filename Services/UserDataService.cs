using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PracticalCodingTest.Models;

namespace PracticalCodingTest.Services
{
    public class UserDataService
    {
        private static UserDataService _singleton = null;

        public static UserDataService GetInstance()
        {
            if (_singleton == null)
            {
                _singleton = new UserDataService();
            }
            return _singleton;
        }
        public UserDataService()
        {
            GenerateFakeUsers();
        }
        public List<User> Users { get; set; } = new List<User>();

        public bool AddUser(string username, string password)
        {
            foreach(User user in Users)
            {
                if (user.UserName == username)
                    return false;
            }
            Users.Add(new User
            {
                UserName = username,
                Password = password 
            });
            return true;
        }

        private void GenerateFakeUsers(int amount = 5)
        {
            string username = "Placeholder";
            string password = "s3cure";
            for (int i = 0;i < amount;i++)
            {
                AddUser(username + i, password);
            }

        }

    }
}