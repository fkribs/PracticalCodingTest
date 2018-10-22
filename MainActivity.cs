﻿using System;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using PracticalCodingTest.Models;
using PracticalCodingTest.Adapters;
using PracticalCodingTest.Services;

namespace PracticalCodingTest
{
    [Activity(Label = "PracticalCodingTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<User> users = new List<User>();
        ListView listView;
        private Button btnNewUser;
        private const string NewUserPrompt = "New User";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            listView = FindViewById<ListView>(Resource.Id.lvContacts);
            users = UserDataService.GetInstance().Users;
            listView.Adapter = new UserAdapter(this, users);
            btnNewUser = FindViewById<Button>(Resource.Id.button1);
            btnNewUser.Text = NewUserPrompt;

        }

        [Java.Interop.Export("NavigateCreateUser")]
        public void NavigateCreateUser(View view)
        {
            Intent intent = new Intent(this, typeof(CreateUserActivity));

            StartActivity(intent);

        }

        protected override void OnResume()
        {
            base.OnResume();
            users = UserDataService.GetInstance().Users;
            listView.Adapter = new UserAdapter(this, users);
        }

    }
}

