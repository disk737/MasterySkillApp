using MasterySkillApp.Models;
using MasterySkillApp.Services;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterySkillApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamListView : ContentPage
	{

        public TeamListView ()
		{
			InitializeComponent ();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent("TeamList");

            ListUserTeam.BeginRefresh();

            if (MasterySingleton.Instance._listUserModel == null)
            {
                // Instancio el servicio de usuarios
                UserServices _userServices = new UserServices();

                // Hago la llamada al Web Service para traer la lista de usuario
                MasterySingleton.Instance._listUserModel = (await _userServices.GetUserModels()).UserModels;
            }

            // Vinculo el Source con la lista
            ListUserTeam.ItemsSource = MasterySingleton.Instance._listUserModel;

            ListUserTeam.EndRefresh();
        }

        private void ListUsersBadges_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            // Retorno si no tengo un item seleccionado
            if (ListUserTeam.SelectedItem == null)
                return;

            var userSelected = e.SelectedItem as UserModel;

            Navigation.PushAsync(new SendBadges(userSelected));

            ListUserTeam.SelectedItem = null;

        }
    }
}