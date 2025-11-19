using ShopList.Gui.ViewModels;

namespace ShopList.Gui.Views;

public partial class ShopListPage : ContentPage
{
	public ShopListPage()
	{
		InitializeComponent();
		BindingContext = new ShopListViewModel();
	}
}