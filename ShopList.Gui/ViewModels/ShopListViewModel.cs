using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopList.Gui.Models;
using ShopList.Gui.Persistence;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShopList.Gui.ViewModels
{
    public partial class ShopListViewModel : ObservableObject 
    {
        [ObservableProperty] 
        private string _nombreDelArticulo = string.Empty;
        [ObservableProperty]
        private int _cantidadAComprar = 1;
        [ObservableProperty]
        private Item? _productoSeleccionado = null;

        [ObservableProperty]
        private ObservableCollection<Item>? _items = null;
        private ShopListDatabase? _dababase = null;

        public ShopListViewModel()
        {
            _dababase = new ShopListDatabase();
            Items = new ObservableCollection<Item>();
            GetItems();
            //CargarDatos();
            if (Items.Count > 0)
            {
                ProductoSeleccionado = Items.First();
            }
            else
            {
                ProductoSeleccionado = null;
            }
        }

        [RelayCommand]
        public async void AgregarShopListItem()
        {
            if (string.IsNullOrEmpty(NombreDelArticulo) 
                || CantidadAComprar <= 0)
            {
                return;
            }
            //Random generador = new Random();
            var item = new Item
            {
                //Id = generador.Next(),
                Nombre = NombreDelArticulo,
                Cantidad = CantidadAComprar,
                Comprado = false,
            };
            await _dababase.SaveItemAsync(item);
            //Items.Add(item);
            GetItems();
            ProductoSeleccionado = item;
            NombreDelArticulo = string.Empty;
            CantidadAComprar = 1;
        }

        [RelayCommand]
        public void EliminarShopListItem()
        {
            if (ProductoSeleccionado != null)
            {
                var índice = Items.IndexOf(ProductoSeleccionado);
                Item? nuevoSeleccionado;
                if (Items.Count > 1)
                {
                    if (índice < Items.Count - 1)
                    {
                        nuevoSeleccionado = Items[índice + 1];

                    }
                    else
                    {
                        nuevoSeleccionado = Items[índice - 1];
                    }
                }
                else
                {
                    nuevoSeleccionado = null;
                }
                Items.Remove(ProductoSeleccionado);
                ProductoSeleccionado = nuevoSeleccionado;
            }
        }
        private async void GetItems()
        {

            IEnumerable<Item> itemsFromDb = await _dababase.GetAllItemsAsync();
            Items = new ObservableCollection<Item>(itemsFromDb);
            //foreach (Item item in itemsFromDb)
            //{
            //    Items.Add(item);
            //}
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
    }
}