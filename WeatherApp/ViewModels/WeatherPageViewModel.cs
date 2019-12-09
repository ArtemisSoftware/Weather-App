using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        
        private IWeatherRepository _repository;
        private IPageService _pageService;
        private bool _isDataLoaded;
        private WeatherViewModel _selectedWeather;


        // Note that I've initialized Contacts to a new ObservableCollection 
        // here, even though we set it again later after loading contacts. 
        // This is required because in ContactsPage we've bound list view's ItemSource
        // to this property. Without this initialization, binding will throw a
        // NullReferenceException. 
        public ObservableCollection<WeatherViewModel> Weathers { get; private set; }
            = new ObservableCollection<WeatherViewModel>();

        public WeatherViewModel SelectedWeather
        {
            get { return _selectedWeather; }
            set { SetValue(ref _selectedWeather, value); }
        }




        public ICommand LoadDataCommand { get; private set; }
        public ICommand SearchCityCommand { get; private set; }

        public ICommand SelectWeatherCommand { get; private set; }


        public WeatherPageViewModel(IWeatherRepository repository, IPageService pageService)
        {
            _repository = repository;
            _pageService = pageService;


            // Because LoadData is an async method and returns Task, we cannot pass its name as an Action to the constructor of the Command. 
            // So, we need to define an inline function using a lambda expression and manually call it using await. 
            LoadDataCommand = new Command(async () => await LoadData());

            SearchCityCommand = new Command<string>(async city => await SearchCity(city));

            SelectWeatherCommand = new Command<WeatherViewModel>(async weather => await SelectWeather(weather));

        }






        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            // Our repository works with Weather objects. In this view model, we work with WeatherViewModel objects. 
            // So, here I've called  LINQ Select() extension method to map these Weather objects to WeatherViewModel. 
            var results = await _repository.GetSeveralCitiesWheaterAsync();

            foreach (var result in results.Weather)
                Weathers.Add(new WeatherViewModel(result));
        }


        private async Task SearchCity(string city)
        {
            if (city == null)
                return;

            var result = await _repository.GetWheater(city);
            Weathers.Add(new WeatherViewModel(result));
        }


        private async Task SelectWeather(WeatherViewModel weather)
        {
            if (weather == null)
                return;

            for (int i = 0; i < Weathers.Count; ++i){

                if (weather.Id.Equals(Weathers[i].Id)){
                    weather.Selected = Color.Green;
                    Weathers[i].Selected = Color.Green;
                    break;
                }

            }

            SelectedWeather = null;

            //var viewModel = new WeatherDetailPageViewModel(weather.CityName, _repository, _pageService);
            //await _pageService.PushAsync(new WeatherDetailPage(viewModel));

            //await _pageService.PushAsync(new WeatherDetailPage());
        }


    }
}
