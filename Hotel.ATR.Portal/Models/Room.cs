using System.ComponentModel.DataAnnotations;

namespace Hotel.ATR.Portal.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string pathIMG { get; set; }

        public string DateTime { get; set; }
    }
}
