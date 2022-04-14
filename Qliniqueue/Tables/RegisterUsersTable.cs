using System;
using System.Collections.Generic;
using System.Text;

namespace Qliniqueue.Tables
{
    public class RegisterUsersTable
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
