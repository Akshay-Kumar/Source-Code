using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceCodeBL
{
    public class Users
    {
        public Users()
        {
            role = new Roles();
        }
        string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        short isLocked;

        public short IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        short isDeleted;

        public short IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        Roles role;

        public Roles Role
        {
            get { return role; }
            set { role = value; }
        }
        string emailId;

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }
    }
}
