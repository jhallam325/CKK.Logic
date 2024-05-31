using static System.Net.Mime.MediaTypeNames;

namespace CKK.Logic.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private decimal price;
        public decimal Price {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                price = value;
            }
        }
        public int Quantity { get; set; }
        public Image Picture { get; set; }
    }
}