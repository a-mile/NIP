using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NIP.Models
{
    public class Business
    {
        public Business(){
            Logs = new HashSet<Log>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string KRS { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}