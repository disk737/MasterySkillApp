using MasterySkillApp.Models;
using MasterySkillApp.Services;
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

            ListUserTeam.BeginRefresh();

            if (MasterySingleton.Instance._listUserModel == null)
            {
                // Instancio el servicio de usuarios
                UserServices _userServices = new UserServices();

                // Hago la llamada al Web Service para traer la lista de usuario
                MasterySingleton.Instance._listUserModel = await _userServices.GetUserModels();
            }

            // Vinculo el Source con la lista
            ListUserTeam.ItemsSource = MasterySingleton.Instance._listUserModel;

            ListUserTeam.EndRefresh();

            //List<UserModel> UserList = new List<UserModel>
            //{
            //    new UserModel{userName = "Angelica Avila", userStatus = "I bring the help"},
            //    new UserModel{userName = "Ivan Hidalgo", userStatus = "I am your backup!!"},
            //    new UserModel{userName = "Mariana Alzate", userStatus = "Automation  master"},
            //    new UserModel{userName = "Jaime Trujillo", userStatus = "Architect OS"},
            //    new UserModel{userName = "Giovanny Cifuentes", userStatus = "Hippies will inherit the earth"},
            //    new UserModel{userName = "Arturo Suarez", userStatus = "Golden Newbie"},
            //    new UserModel{userName = "Juan Camilo Moreno", userStatus = "Code Master"},
            //};
            //ListUserTeam.ItemsSource = UserList;

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