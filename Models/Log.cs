using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NIP.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public DateTime Date { get; set; }
        public string Headers { get; set; }
        public virtual Business Business { get; set; }
    }
}