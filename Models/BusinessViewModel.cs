using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace NIP.Models
{
    public class BusinessViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

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
        public string KRS { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
    }
}