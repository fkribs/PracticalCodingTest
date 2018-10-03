using Android.App;
using Android.Widget;
using Android.Views;
using Android.OS;
using System.Collections.Generic;
using PracticalCodingTest.Models;
using PracticalCodingTest.Adapters;

namespace PracticalCodingTest
{
    [Activity(Label = "PracticalCodingTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<User> users = new List<User>();
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            listView = FindViewById<ListView>(Resource.Id.lvContacts);

            for (int i = 1; i < 20; i++)
            {

                users.Add(new User
                {
                    UserName = "Forrest",
                    PasswordHash = "90"
                });
            }

            listView.Adapter = new UserAdapter(this, users);
        }
    }
}

