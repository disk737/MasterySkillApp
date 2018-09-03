using MasterySkillApp.Models;
using MasterySkillApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterySkillApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GiveBadgesView : ContentPage
	{
		public GiveBadgesView ()
		{
			InitializeComponent ();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (MasterySingleton.Instance._listUserModel == null)
            {
                // Instancio el servicio de usuarios
                UserServices _userServices = new UserServices();

                // Hago la llamada al Web Service para traer la lista de usuario
                MasterySingleton.Instance._listUserModel = await _userServices.GetUserModels();
            }

            // Vinculo el Source con la lista
            ListUsersBadges.ItemsSource = MasterySingleton.Instance._listUserModel;
        }

        private void ListUsersBadges_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            // Retorno si no tengo un item seleccionado
            if (ListUsersBadges.SelectedItem == null)
                return;

            var userSelected = e.SelectedItem as UserModel;

            Navigation.PushAsync(new SendBadges(userSelected));

            ListUsersBadges.SelectedItem = null;

        }
    }
}