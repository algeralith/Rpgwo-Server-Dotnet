using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Accounts
{
    public class AccountManager
    {
        private static Dictionary<string, Account> _Accounts = new Dictionary<string, Account>();

        public static void Initialize()
        {
            ServerEvents.OnAccountLogin += Events_OnAccountLogin;
        }

        private static void Events_OnAccountLogin(LoginEventArgs e)
        {
            // Grab the account associated, if any.
            var account = GetAccount(e.Username);

            if (e.NewUser)
            {
                // Check to see if the account already exists.
                if (account != null)
                {
                    e.Result = false;
                    e.Reason = "This account name is already in use.";
                    return;
                }

                // Make sure we have all the fields needed.
                if (e.Username == "" || e.Password == "" || e.Email == "")
                {
                    // Reject. I do not believe the client will ever allow this. Best to handle anyways.
                    e.Result = false;
                    e.Reason = "Some fields have been left blank.";
                    return;
                }

                // Create the account.
                account = CreateAccount(e.Username, e.Password);

                if (account == null)
                {
                    // For some reason, the account failed to be made.
                    e.Result = false;
                    e.Reason = "Sorry, the account failed to create. Try again.";
                    return;
                }

                // Everything should be good. Allow.
                e.Result = true;

                // Bind the account to the client.
                e.Client.Account = account;
                return;
            }
            else
            {
                if (account == null)
                {
                    e.Result = false;
                    e.Reason = "Sorry, bad username. You may need to create a new account.";
                    return;
                }
                
                // Password is wrong.
                if (e.Password != account.Password)
                {
                    e.Result = false;
                    e.Reason = "Sorry, bad password.";
                    return;
                }

                // At this point, everything is correct. Allow login.
                e.Result = true;

                // Bind the account to the client.
                e.Client.Account = account;
                return;
            }
        }

        public static void LoadAccounts()
        {

        }

        public static void SaveAccounts()
        {

        }

        public static Account CreateAccount(string username, string password)
        {
            // TODO :: Checks.
            Account account = new Account(username, password);
            Add(account);

            return account;
        }

        public static void Add(Account account)
        {
            lock(_Accounts)
            {
                _Accounts.Add(account.Username.ToLower(), account);
            }
        }

        public static void Remove(Account account)
        {
            lock(_Accounts)
            {
                _Accounts.Remove(account.Username.ToLower());
            }
        }

        public static Account GetAccount(string name)
        {
            bool contains = _Accounts.TryGetValue(name.ToLower(), out Account account);

            if (contains)
                return account;
            else
                return null;
        }
    }
}
