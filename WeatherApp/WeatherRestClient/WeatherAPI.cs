using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.WeatherRestClient
{
    class WeatherAPI : IWeatherRepository
    {

        private const string OpenWeatherApi = "http://api.openweathermap.org/data/2.5/weather?q=";
        private const string OpenMultipleWeatherApi = "http://api.openweathermap.org/data/2.5/box/city?bbox=";
        private const string Key = "653b1f0bf8a08686ac505ef6f05b94c2";
        HttpClient _httpClient = new HttpClient();

        public async Task<CitiesWeather> GetSeveralCitiesWheaterAsync()
        {

            var json = await _httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/box/city?bbox=12,32,15,37,10&APPID=653b1f0bf8a08686ac505ef6f05b94c2");
            var getWeatherModels = JsonConvert.DeserializeObject<CitiesWeather>(json);
            return getWeatherModels;
        }

        public async Task<Weather> GetWheater(string city)
        {
            var json = await _httpClient.GetStringAsync(OpenWeatherApi + city + "&APPID=" + Key);
            var getWeatherModels = JsonConvert.DeserializeObject<Weather>(json);
            return getWeatherModels;
        }
    }
}
