using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace NIP.Models
{
    public class BusinessViewModel
    {
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Ulica")]
        public string Street { get; set; }

        [DisplayName("Numer")]
        public int Number { get; set; }

        [DisplayName("Kod pocztowy")]
        public string ZIPCode { get; set; }

        [DisplayName("Miasto")]
        public string City { get; set; }
    }
}