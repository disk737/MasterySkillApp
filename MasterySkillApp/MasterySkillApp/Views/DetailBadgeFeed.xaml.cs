using MasterySkillApp.Models;
using MasterySkillApp.Services;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterySkillApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailBadgeFeed : ContentPage
    {

        BadgeServices _badgeServices;
        private bool OnlyOnce = false;

        public DetailBadgeFeed()
        {
            InitializeComponent();

            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();

            // Reviso si la lista ya fue llenada para que no se vuelva a llenar
            if (OnlyOnce)   
                return;

            // Cambio la bandera para que solo se ejecute una sola vez
            OnlyOnce = true;

            ListNewsFeed.BeginRefresh();

            RefreshNewsFeed();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("NewsFeed");
        }


        private void ListNewsFeed_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListNewsFeed.SelectedItem = null;
        }

        private void ListNewsFeed_Refreshing(object sender, EventArgs e)
        {
            RefreshNewsFeed();
        }

        private async void RefreshNewsFeed()
        {
            // Vinculo el Source a la lista

            ListDetailAttr response = await _badgeServices.GetDetailCount();

            ListNewsFeed.EndRefresh();
      
            // Evaluo la respuesta del servidor
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    // Invoco el servicio de Logout
                    UserServices _userServices = new UserServices();
                    _userServices.UserLogout();

                    Application.Current.MainPage = new NavigationPage(new LoginPageView());
                    break;

                case HttpStatusCode.InternalServerError:
                    await DisplayAlert("Oooopssss", "Algo fallo en el servidor. Intentemos mas tarde ¿Vale?", "OK");
                    break;

                default:
                    ListNewsFeed.ItemsSource = response.AttrDetail;
                    break;
            }


        }
    }
}