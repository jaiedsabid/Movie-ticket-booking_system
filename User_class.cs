using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTerm_project
{
    class User
    {
        protected string Name, Address, Phone, Gender;

        public string User_Name
        {
            get => this.Name;
            set => this.Name = value;
        }

        public string User_Address
        {
            get => Address;
            set => Address = value;
        }

        public string User_Phone
        {
            get => Phone;
            set => Phone = value;
        }

        public string User_Gender
        {
            set => Gender = value;
            get => Gender;

        }
    }
}
