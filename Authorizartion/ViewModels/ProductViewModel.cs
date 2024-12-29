using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DishesCompany
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Products> products;
        public ObservableCollection<Products> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        private ObservableCollection<string> manufacturers;
        public ObservableCollection<string> Manufacturers
        {
            get { return manufacturers; }
            set
            {
                manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        private int totalProductCount;
        public int TotalProductCount
        {
            get { return totalProductCount; }
            set
            {
                totalProductCount = value;
                OnPropertyChanged(nameof(TotalProductCount));
                OnPropertyChanged(nameof(Products));
            }
        }

        private int currentProductCount;
        public int CurrentProductCount
        {
            get { return currentProductCount; }
            set
            {
                currentProductCount = value;
                OnPropertyChanged(nameof(CurrentProductCount));
            }
        }

        public ProductViewModel()
        {
            LoadProducts(); 
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadProducts()
        {
            Products = new ObservableCollection<Products>(DatabaseControl.GetProducts());

            List<string> manufacturerList = new List<string>();
            manufacturerList.Add("Все производители");
            manufacturerList.AddRange(DatabaseControl.GetManufacturers());
            Manufacturers = new ObservableCollection<string>(manufacturerList);
            TotalProductCount = DatabaseControl.GetCount();
            CurrentProductCount = DatabaseControl.GetCount(Products.ToList());
        }
    }
}
