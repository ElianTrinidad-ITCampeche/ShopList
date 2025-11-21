using ShopList.Gui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ShopList.Gui.ViewModels
{
    public class ShopListViewModel : INotifyPropertyChanged
    {
        private string _nombreDelArticulo = string.Empty;
        private int _cantidadAComprar = 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Item> Items { get; }

        public string NombreDelArticulo
        {
            get=> _nombreDelArticulo;
            set
            {
                if (value != _nombreDelArticulo)
                {
                    _nombreDelArticulo = value;
                    OnPropertyChanged(nameof(NombreDelArticulo));
                }
            }
        }
        public int CantidadAComprar
        {
            get => _cantidadAComprar;
            set
            {
                if (value != _cantidadAComprar)
                {
                    _cantidadAComprar = value;
                    OnPropertyChanged(nameof(CantidadAComprar));
                }
            }
        }

        public ICommand AgregarShopListItemCommand
        {
            get;
            private set;
        }


        public ShopListViewModel()
        {
            Items = new ObservableCollection<Item>();
            CargarDatos();
            AgregarShopListItemCommand = 
                new Command(AgregarShopListItem);
        }

        
        public void AgregarShopListItem()
        {
            if (string.IsNullOrEmpty(NombreDelArticulo) 
                || CantidadAComprar <= 0)
            {
                return;
            }
            Random generador = new Random();
            var item = new Item
            {
                Id = generador.Next(),
                Nombre = NombreDelArticulo,
                Cantidad = CantidadAComprar,
                Comprado = false,
            };
            Items.Add(item);
            NombreDelArticulo = string.Empty;
            CantidadAComprar = 1;
        }

        public void EliminarShopListItem()
        {

        }

        private void CargarDatos()
        {
            Items.Add(new Item()
            {
                Id = 1,
                Nombre = "Leche",
                Cantidad = 2,
                Comprado = false,
            });

            Items.Add(new Item()
            {
                Id = 2,
                Nombre = "Pan de caja",
                Cantidad = 1,
                Comprado = true,
            });

            Items.Add(new Item()
            {
                Id = 3,
                Nombre = "Jamon",
                Cantidad = 500,
                Comprado = false,
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke
                (this, new PropertyChangedEventArgs(propertyName)
                );
        }
    }
}
