using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace DishesCompany
{
    public class Products : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [Key] public int Product_id { get; set; }
        [Required] public string Articul { get; set; }
        public string Product_name { get; set; }
        public string Measurement_unit { get; set; }
        public decimal Product_cost { get; set; }
        public decimal? Max_discount { get; set; }
        public string? Manufacturer { get; set; }
        public string Supplier { get; set; }
        public string Product_type { get; set; }
        public decimal? Current_discount { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        private string? image;
        public string? Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
                OnPropertyChanged(nameof(BitmapImage));
            }
        }
        public BitmapImage? BitmapImage
        {
            get
            {
                string Directory = AppDomain.CurrentDomain.BaseDirectory;
                string ImagePath = Path.Combine(Directory, "..", "..", "..", "Resources", "Images");
                BitmapImage bitmapImage = new BitmapImage();
                Stream stream = Image != null ?
                    File.OpenRead(Path.Combine(ImagePath, Image)) :
                    File.OpenRead(Path.Combine(ImagePath, "picture.png"));

                using (stream)
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
            }
        }

        public List<Product_orders> Product_order_entities { get; set; }

        public Products(string articul, string product_name, string product_type, int amount,
            string measurement_unit, string manufacturer, string supplier, decimal product_cost,
            string description, string image)
        {
            Articul = articul;
            Product_name = product_name;
            Product_type = product_type;
            Amount = amount;
            Measurement_unit = measurement_unit;
            Manufacturer = manufacturer;
            Supplier = supplier;
            Product_cost = product_cost;
            Description = description;
            Image = image;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
