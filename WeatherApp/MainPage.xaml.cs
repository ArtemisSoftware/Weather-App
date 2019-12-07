using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using WeatherApp.WeatherRestClient;
using Xamarin.Forms;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {

            var api = new WeatherAPI();
            ViewModel = new WeatherPageViewModel(api);

            InitializeComponent();
        }


        public WeatherPageViewModel ViewModel
        {
            get { return BindingContext as WeatherPageViewModel; }
            set { BindingContext = value; }
        }


        protected override void OnAppearing()
        {
            ViewModel.SearchCityCommand.Execute("Lisbon");

            base.OnAppearing();
        }


    }
}
