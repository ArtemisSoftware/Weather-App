using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class PageService : IPageService
    {
        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }


        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
