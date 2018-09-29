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
	public partial class BadgesView : ContentPage
    {
        // Creo una instancia para los servicios
        private BadgeServices _badgeServices;
        private bool OnlyOnce = false;


        public BadgesView ()
		{
			InitializeComponent ();
            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();

            // Reviso si la lista ya fue llenada para que no se vuelva a llenar
            if (OnlyOnce)  
                return;
                
            // Cambio la bandera para que solo se ejecute una sola vez
            OnlyOnce = true;

            badgesList.BeginRefresh();

            // Llamo el metodo para refrescar la lista
            RefreshAttrList();

        }

        protected override void OnAppearing()
        {
            Analytics.TrackEvent("BadgesView");
            base.OnAppearing();
        }

        private void badgesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            badgesList.SelectedItem = null;
        }

        private void badgesList_Refreshing(object sender, EventArgs e)
        {
            RefreshAttrList();
        }

        private async void RefreshAttrList()
        {

            // Vinculo el Source a la lista
            //badgesList.ItemsSource = await _badgeServices.GetAttrPoints();
            ListAttrPoints response = await _badgeServices.GetAttrPoints();
            badgesList.EndRefresh();
            
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
                    await DisplayAlert("Oooopssss","Algo fallo en el servidor. Intentemos mas tarde ¿Vale?","OK");
                    break;
     
                default:
                    badgesList.ItemsSource = response.AttrPoints;
                    break;
            }

        }

    }
}