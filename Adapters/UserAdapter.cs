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
namespace PracticalCodingTest.Adapters
{
    class UserAdapter : BaseAdapter<User>
    {
        List<User> users;
        Activity context;
        public UserAdapter(Activity context, List<User> users) : base()
        {
            this.context = context;
            this.users = users;

        }
        public override User this[int position]
        {
            get { return users[position]; }
        }

        public override int Count => users.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var user = users[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
            }
            view.FindViewById<TextView>(Resource.Id.textViewName).Text = user.UserName;
            view.FindViewById<TextView>(Resource.Id.textViewPassword).Text = user.Password;
            //view.FindViewById<TextView>(Resource.Id.textViewName).Text = user.UserName;
            Random rnd = new Random();
            Android.Graphics.Color color = Android.Graphics.Color.Rgb(rnd.Next(256),rnd.Next(256), rnd.Next(256));
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetBackgroundColor(color);

            return view;
        }
    }
}