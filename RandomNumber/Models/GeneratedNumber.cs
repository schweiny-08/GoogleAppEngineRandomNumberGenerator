using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomNumber.Models
{
    public class GeneratedNumber
    {
        public int Id { get; set; }
        [Required]
        public string InstanceName { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
