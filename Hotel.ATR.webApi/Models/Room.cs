using System;
using System.Collections.Generic;

namespace Hotel.ATR.webApi.Models
{
    public partial class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PathImg { get; set; } = null!;
        public string DateTime { get; set; } = null!;
    }
}
