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
        // Creo una instancia para los servicios
        BadgeServices _badgeServices;
        private bool OnLoad = false;

        public DetailBadgeFeed()
        {
            InitializeComponent();

            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListNewsFeed.BeginRefresh();
            RefreshNewsFeed();

            // Cambio el estado de la bandera para que la View sepa que ya se hizo la carga inicial
            OnLoad = true;

            Analytics.TrackEvent("NewsFeed");
        }

        private void ListNewsFeed_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListNewsFeed.SelectedItem = null;
        }

        private void ListNewsFeed_Refreshing(object sender, EventArgs e)
        {
            if (!OnLoad)
                return;

            RefreshNewsFeed();
        }

        private async void RefreshNewsFeed()
        {
            // Hago la llamada al WS
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