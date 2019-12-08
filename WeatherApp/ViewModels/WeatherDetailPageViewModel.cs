using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.WeatherRestClient;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherDetailPageViewModel : BaseViewModel
    {

        private IWeatherRepository _repository;
        private IPageService _pageService;

        private Weather _weatherMainModel;  // for xaml binding

        public ICommand SearchCityCommand { get; private set; }

        public WeatherDetailPageViewModel()
        {
            _repository = new WeatherAPI();
        }

        public WeatherDetailPageViewModel(string city, IWeatherRepository repository, IPageService pageService)
        {
            if (city == null)
                throw new ArgumentNullException(city);

            _pageService = pageService;
            _repository = repository;

            _city = city;

            //SearchCityCommand = new Command<string>(async _city => await SearchCity(_city));
        }






        public Weather WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                IconImageString = "https://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png"; // fetch weather icon image
                OnPropertyChanged();
            }
        }







        private string _city;   // for entry binding and for method parameter value
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Task.Run(async () => {
                    await InitializeGetWeatherAsync();
                });
                OnPropertyChanged();
            }
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true; // set the ui property "IsRunning" to true(loading) in Xaml ActivityIndicator Control
                WeatherMainModel = await _repository.GetWheater(_city);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /*
        private async Task SearchCity(string city)
        {
            if (city == null)
                return;

            var result = await _repository.GetWheater(city);
            Weathers.Add(new WeatherViewModel(result));
        }
        */


        private string _iconImageString; // for weather icon image string binding
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;   // for showing loader when the task is initializing
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    
}
