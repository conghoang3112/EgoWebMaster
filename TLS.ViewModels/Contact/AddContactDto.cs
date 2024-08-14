using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.Contact
{
    public class AddContactDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Email { get;set; }
        public string Company { get; set; }
        public string SolutionOfInterest { get; set; }
        public string BusinessAreas { get; set; }
        public string Purpose { get; set; }

    }
}
