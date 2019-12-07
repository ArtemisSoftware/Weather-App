﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel
    {
        
        private IWeatherRepository _repository;
        private bool _isDataLoaded;


        // Note that I've initialized Contacts to a new ObservableCollection 
        // here, even though we set it again later after loading contacts. 
        // This is required because in ContactsPage we've bound list view's ItemSource
        // to this property. Without this initialization, binding will throw a
        // NullReferenceException. 
        public ObservableCollection<WeatherViewModel> Weathers { get; private set; }
            = new ObservableCollection<WeatherViewModel>();


        public ICommand LoadDataCommand { get; private set; }
        public ICommand SearchCityCommand { get; private set; }


        public WeatherPageViewModel(IWeatherRepository repository)
        {
            _repository = repository;
            

            // Because LoadData is an async method and returns Task, we cannot pass its name as an Action to the constructor of the Command. 
            // So, we need to define an inline function using a lambda expression and manually call it using await. 
            LoadDataCommand = new Command(async () => await LoadData());

            SearchCityCommand = new Command<string>(async city => await SearchCity(city));

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



    }
}
