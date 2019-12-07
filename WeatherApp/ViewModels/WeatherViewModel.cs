using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        
        // Default constructor for scenarios where we want to instantiate a 
        // blank ContactViewModel object. 
        public WeatherViewModel() { }

        public WeatherViewModel(Weather weather)
        {
            // When initializing this object, I'm using the private field directly
            // because I don't want this to raise a notification. 

            // You may think we have violated the DRY principle (Don't Repeat 
            // Yourself) because we've defined these properties in 2 places. 
            // While that is partially true, we've done this for good reasons. 
            // When you use the same class both as the domain and presentation model
            // that class becomes bloated with too many responsibilities. This is
            // not an issue for a small app (like this ContactBook app). But as
            // the complexity of your application increases, throwing all these
            // properties and methods into the same class will end up with a big 
            // mess. So, it's better to have two separate models, one for presentation
            // one for domain. This improves separation of concerns in your application
            // but on the downside it comes with a cost: you have to repeat some of
            // the properteis and map them together. While these few lines of 
            // property assignment are not a major issue, if this still bothers
            // you, you can use a convention-based mapping library like AutoMapper.
            Id = weather.Id;
            _cityName = weather.name;
            _icon = weather.weather[0].icon;
        }


        public string Id { get; set; }
        public string Name { get; set; }
        public WeatherTempDetails TempDetails { get; set; }
        public List<WeatherSubDetails> SubDetails { get; set; }
        public WeatherWindDetails Wind { get; set; }
        public WeatherSysDetails SysDetails { get; set; }




        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                SetValue(ref _cityName, value);
                OnPropertyChanged(nameof(CityName));
            }
        }

        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set
            {
                SetValue(ref _icon, value);
                OnPropertyChanged(nameof(Icon));
            }
        }



    }
}
