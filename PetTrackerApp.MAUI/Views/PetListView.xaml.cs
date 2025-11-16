using PetTrackerApp.MAUI.ViewModels;

namespace PetTrackerApp.MAUI.Views;

public partial class PetListView : ContentPage
{
	public PetListView(PetListViewModel viewModel)
	{
		InitializeComponent();

        // viewmodel'i binding context olarak ayarlýyoruz yani PetListViewModel'i bu view'in veri kaynagi olarak belirliyoruz
        BindingContext = viewModel;
    }

    //biz petlistview'e petlerin sadece ilk acildiginda yuklenmesini istiyoruz 
    //bu yüzden onappearing metotunu override ediyoruz (onappearing sayfa her göründügünde cagrilan metot)
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //binding context'in PetListViewModel oldugunu kontrol ediyoruz /true ise viewmodel'de tanimlanan LoadPetsAsync komutunu cagiriyoruz
        if (BindingContext is PetListViewModel viewModel)
        {            
            await viewModel.LoadPetsAsync();
        }
    }
}