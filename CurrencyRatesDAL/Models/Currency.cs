using System.ComponentModel.DataAnnotations;

namespace CurrencyRatesDAL.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ccy { get; set; }

        [Required]
        public string Base_ccy { get; set; }

        [Required]
        public string Buy { get; set; }
        
        [Required]
        public string Sale { get; set; }
    }
}
