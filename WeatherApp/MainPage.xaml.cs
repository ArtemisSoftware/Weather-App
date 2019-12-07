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
            var pageService = new PageService();
            ViewModel = new WeatherPageViewModel(api, pageService);

            InitializeComponent();
        }


        public WeatherPageViewModel ViewModel
        {
            get { return BindingContext as WeatherPageViewModel; }
            set { BindingContext = value; }
        }


        void OnCitySelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectWeatherCommand.Execute(e.SelectedItem);
        }


        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);

            base.OnAppearing();
        }


    }
}
