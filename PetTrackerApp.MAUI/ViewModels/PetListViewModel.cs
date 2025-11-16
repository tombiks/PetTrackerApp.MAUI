using CommunityToolkit.Mvvm.ComponentModel; // ObservableObject  - property degisikliklerini UI'a bildirmek icin
using CommunityToolkit.Mvvm.Input; // RelayCommand - butona komut baglamak icin
using System.Collections.ObjectModel; // ObservableCollection - normal liste disinda degisiklikleri UI'a bildirmek icin
using PetTrackerApp.Data.Dtos; //PetDto'ya erismek icin
using PetTrackerApp.Data.Services; // PetService'e erismek icin

// onbilgi: bu sayafada cokca kullanacagimiz 2 attribute hakkinda aciklama:
// [ObservableProperty]: Bu attribute, bir alan (field) tanimladigimizda, otomatik olarak o alana karsilik gelen bir property olusturur.
// Bu property, deger degistiginde UI'a bildirimde bulunur.
// Ornegin, [ObservableProperty] attribute'u altinda "string name;" alanini tanimladigimizda,
// "public string Name { get; set; }" propertysi otomatik olarak olusur ve degeri degistigimizde UI'a bildirimde bulunur.
//
// [RelayCommand]: Bu attribute, bir metot tanimladigimizda, o metodu bir komut (command) haline getirir.
// Bu komut, UI elementlerine (butonlar gibi) baglanabilir.


namespace PetTrackerApp.MAUI.ViewModels
{
    public partial class PetListViewModel : ObservableObject // ViewModel'in ObservableObject'ten turetilmesi, property degisikliklerinin UI'a bildirilmesini saglar.
    {
        private readonly PetService _petService; //petservice'e erismek icin

        //bunu yapmamizin sebebi, maui programda bu viewmodel'i singleton olarak kaydettik, her dönüste tekrar verileri loding etmesin diye
        //sadece ilk açilisda yüklensin diye "FLAG" olarak kullanacagiz
        private bool _isDataLoaded = false; //veri yuklenip yuklenmedigini kontrol etmek icin

        // pets listesini [ObservableProperty] attribute'u ile tanimliyoruz ki UI'a baglanabilsin ve degisiklikleri bildirebilsindiye "Pets" adında property'e cevirdi.
        [ObservableProperty]
        private ObservableCollection<PetDto> pets; //UI'a baglanacak pet listesi

        [ObservableProperty] //isLoading propertysini UI'a baglamak icin "IsLoading" propertysine cevirdi
        [NotifyPropertyChangedFor(nameof(IsNotLoading))] // IsLoading degistiginde IsNotLoading propertysinin de degistiginide UI'ye bildir
        private bool isLoading; 

        public bool IsNotLoading => !IsLoading; // IsLoading'in tersini kullanmak icin !IsLoading tersineyse IsNotLoading true olur

        //kurucu metotu olusturuyoruz, petservice'i dependency injection ile aliyoruz
        public PetListViewModel(PetService petService)
        {
            _petService = petService;
            pets = new ObservableCollection<PetDto>();
        }

        [RelayCommand] //bu metodu bir komut haline getiriyor -suan sadece petlistview.cs'de kullaniyoruz ama ilerde refresh butonu icin kullanabiliriz
        public async Task LoadPetsAsync()
        {
            if (_isDataLoaded) //eger veriler zaten yuklendiyse
                return; //metottan cik

            IsLoading = true; //verinin yüklenmeye basladigini ui'ye belirt

            try
            {
                var petDtos = await _petService.GetAllPetsAsync(); //tüm petleri servisten asenkron olarak al
                
                Pets = new ObservableCollection<PetDto>(petDtos); //alınan petleri ObservableCollection'a atarak Pets propertysine ata

                _isDataLoaded = true; //veriler yüklendi olarak isaretle FLAG'i kullandik
            }
            finally 
            {
                IsLoading = false; //veri yukleme islemi bitti, ui'ye belirt
            }
            
        }
    }
}
