using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherDetailPage : ContentPage
    {

        public WeatherDetailPage()
        {
            InitializeComponent();

        }

        public WeatherDetailPage(WeatherDetailPageViewModel viewModel)
        {
            //ViewModel = viewModel;
            InitializeComponent();

        }

    }
}