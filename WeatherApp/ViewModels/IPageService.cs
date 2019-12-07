using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();

    }
}
