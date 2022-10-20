using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    internal class Contact // contact model
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";
    }
}
