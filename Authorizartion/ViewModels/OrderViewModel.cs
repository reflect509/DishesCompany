using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DishesCompany.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Users user;
        private ObservableCollection<Orders> activeOrders;
        public ObservableCollection<Orders> ActiveOrders
        {
            get { return activeOrders; }
            set
            {
                activeOrders = value;
                OnProperyChanged(nameof(ActiveOrders));
            }
        }
        private ObservableCollection<Orders> completedOrders;
        public ObservableCollection<Orders> CompletedOrders
        {
            get { return completedOrders; }
            set
            {
                completedOrders = value;
                OnProperyChanged(nameof(CompletedOrders));
            }
        }
        private ObservableCollection<Products> products;
        public ObservableCollection<Products> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnProperyChanged(nameof(Products));
            }
        }
        private ObservableCollection<Delivery_places> deliveryPlaces;
        public ObservableCollection<Delivery_places> Delivery_places
        {
            get { return deliveryPlaces; }
            set
            {
                deliveryPlaces = value;
                OnProperyChanged(nameof(Delivery_places));
            }
        }
        private ObservableCollection<Product_orders> product_orders;
        public ObservableCollection<Product_orders> Product_orders
        {
            get { return product_orders; }
            set
            {
                product_orders = value;
                OnProperyChanged(nameof(Product_orders));
            }
        }

        public OrderViewModel(Users user)
        {
            this.user = user;
            LoadOrders();
        }

        private void LoadOrders()
        {
            ActiveOrders = new ObservableCollection<Orders>(DatabaseControl.GetActiveOrders(user));
            CompletedOrders = new ObservableCollection<Orders>(DatabaseControl.GetCompletedOrders(user));
        }

        protected void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
