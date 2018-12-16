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
        private bool OnLoad = false;

        public BadgesView ()
		{
			InitializeComponent ();

            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent("BadgesView");
            if (MasterySingleton.Instance._listBasicAttrModel == null)
            {
                // Llamo el metodo para refrescar la lista
                badgesList.BeginRefresh();
                RefreshAttrList();
            } 

            // Cambio el estado de la bandera para que la View sepa que ya se hizo la carga inicial
            OnLoad = true;         
        }

        private void badgesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            badgesList.SelectedItem = null;
        }

        private void badgesList_Refreshing(object sender, EventArgs e)
        {
            if (!OnLoad)
                return;

            RefreshAttrList();
        }

        private async void RefreshAttrList()
        {
            // Vinculo el Source a la lista
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
                    MasterySingleton.Instance._listBasicAttrModel = response.AttrPoints;
                    badgesList.ItemsSource = response.AttrPoints;
                    break;
            }

        }

    }
}