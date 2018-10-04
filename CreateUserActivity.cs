using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Java.Util.Regex;
using PracticalCodingTest.Enums;
using PracticalCodingTest.Services;
using Pattern = Java.Util.Regex.Pattern;

namespace PracticalCodingTest
{
    [Activity(Label = "CreateUserActivity")]
    public class CreateUserActivity : Activity
    {
        private TextView tvUsername, tvPassword, tvConfirm, tvErrors;
        private EditText etUsername, etPassword, etConfirm;
        private Button btnBack, btnCreateUser;

        private const string UsernamePrompt = "Username";
        private const string PasswordPrompt = "Password";
        private const string ConfirmPrompt = "Confirm Password";
        private const string BackButtonPrompt = "Back";
        private const string CreateUserPrompt = "Create User";

        private const string UsernameRegex = "^([a-zA-Z0-9]){5,12}$";
        private const string UsernameRegexError = "Username must be 5-12 characters and alpha-numeric.";
        private const string UsernameRegexSuccess = "Username is valid.";

        private const string PasswordRegex = "^([a-zA-Z0-9]){5,12}$";
        private const string PasswordRegexError = "Password must be 5-12 characters, having at least one letter and one number without repeating any sequence of characters.";
        private const string PasswordRegexSuccess = "Password is valid.";

        private const string ContainsLetterRegex = "^([A-Za-z0-9])*([A-Za-z])+([A-Za-z0-9])*$";
        private const string ContainsNumberRegex = "^([A-Za-z0-9])*([0-9])+([A-Za-z0-9])*$";

        private const string PasswordCompareSuccess = "Passwords match.";
        private const string PasswordCompareError = "Passwords do not match.";

        private const string UserTakenError = "Username already taken.";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.create_user);
            InitializeUserInterface();
        }

        private void InitializeUserInterface()
        {
            tvUsername = FindViewById<TextView>(Resource.Id.tvUsername);
            tvUsername.Text = UsernamePrompt;
            tvPassword = FindViewById<TextView>(Resource.Id.tvPassword);
            tvPassword.Text = PasswordPrompt;
            tvConfirm = FindViewById<TextView>(Resource.Id.tvConfirm);
            tvConfirm.Text = ConfirmPrompt;
            tvErrors = FindViewById<TextView>(Resource.Id.tvErrors);
            tvErrors.Text = "";

            etUsername = FindViewById<EditText>(Resource.Id.etUsername);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            etConfirm = FindViewById<EditText>(Resource.Id.etConfirm);

            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnBack.Text = BackButtonPrompt;
            btnCreateUser = FindViewById<Button>(Resource.Id.btnCreateUser);
            btnCreateUser.Text = CreateUserPrompt;
        }

        [Java.Interop.Export("FinishActivity")]
        public void FinishActivity(View view)
        {
            HideKeyboard();
            this.Finish();
        }

        [Java.Interop.Export("CreateUser")]
        public void CreateUser(View view)
        {
            HideKeyboard();
            bool didError = false;
            List<string>errors = new List<string>();
            string username = etUsername.Text;
            errors.Add(Validate(MyValidationType.Username, username, ref didError));
            string password = etPassword.Text;
            errors.Add(Validate(MyValidationType.Password, password, ref didError));
            string passwordConfirm = etConfirm.Text;
            errors.Add(ComparePasswords(password, passwordConfirm, ref didError));
            tvErrors.Text = ErrorListToString(errors);

            if (!(didError))
            {
                if (UserDataService.GetInstance().AddUser(username, password))
                    this.Finish();
                else
                    tvErrors.Text = UserTakenError;
            }
        }

        private string ComparePasswords(string p1, string p2, ref bool didError)
        {
            if (p1.Equals(p2))
            {
                return PasswordCompareSuccess;
            }
            didError = true;
            return PasswordCompareError;
        }
        private string Validate(MyValidationType validationType, string data, ref bool didError)
        {
            if (validationType == MyValidationType.Username)
            {
                Java.Util.Regex.Pattern p = Pattern.Compile(UsernameRegex);
                Matcher m = p.Matcher(data);
                if (!(m.Matches()))
                {
                    didError = true;
                    return UsernameRegexError;
                }
                else
                {
                    return UsernameRegexSuccess;
                }
            }else if (validationType == MyValidationType.Password)
            {
                Java.Util.Regex.Pattern p = Pattern.Compile(PasswordRegex);
                Matcher m = p.Matcher(data);
                if (!(m.Matches() && ContainsLetter(data) && ContainsNumber(data) && NoRepeatingSequence(data)))
                {
                    didError = true;
                    return PasswordRegexError;
                }
                else
                {
                    return PasswordRegexSuccess;
                }
            }
            else
            {
                throw new System.Exception(string.Format("Invalid ValidationType in CreateUserActivity.Validate: {0}",validationType));
            }
        }

        private bool ContainsLetter(string data)
        {
            Java.Util.Regex.Pattern p = Pattern.Compile(ContainsLetterRegex);
            Matcher m = p.Matcher(data);
            if (m.Matches())
                return true;
            return false;
        }
        private bool ContainsNumber(string data)
        {
            Java.Util.Regex.Pattern p = Pattern.Compile(ContainsNumberRegex);
            Matcher m = p.Matcher(data);
            if (m.Matches())
                return true;
            return false;
        }

        private bool NoRepeatingSequence(string data)
        {
            for (int i = 1; i < data.Length/2; i++)
            {
                for (int i2 = 0; i2 < data.Length; i2++)
                {
                    if (!(i2 + i >= data.Length))
                    {
                        string back = data.Substring(i2, i);
                        string front = data.Substring(i2 + i);
                        int backLen = back.Length;
                        if (backLen <= front.Length)
                        {
                            string immediateFront = front.Substring(0, backLen);
                            if (back.Equals(immediateFront))
                                return false;
                        }
                    }
                }
            }
            return true;
        }
        private string ErrorListToString(List<string> errors)
        {
            string formattedErrors = "";
            foreach (var error in errors)
            {
                if (error != null)
                    formattedErrors += error.ToString() + "\n";
            }
            return formattedErrors;
        }

        private void HideKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}