using MasterySkillApp.Entitys;
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
        // Creo la lista que contiene los Grupos
        List<GroupModel> ListGroups;

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

                // Hago la llamada al servicio que trae los Grupos
                ListGroups = (await _userServices.GetGroups()).GroupModel;

                // Hago la llamada al Web Service para traer la lista de usuario
                MasterySingleton.Instance._listUserModel = (await _userServices.GetUserModels()).UserModels;

                // Debo hacer una lista de los nombres de los grupos
                List<string> ListGroupNames = ListGroups.Select(group => group.groupName).OrderBy(name => name).ToList();

                // Vinculo el Picker con la lista de Grupos
                GroupPicker.ItemsSource = ListGroupNames;

                // Selecciono el Grupo al que pertenece el usuario
                GroupPicker.SelectedItem = MasterySingleton.Instance._userInfo.groupName;

            }

            // Vinculo el Source con la lista de personas
            ListUserTeam.ItemsSource = MasterySingleton.Instance._listUserModel;

            // Filtro la lista por Grupo
            FilterByGroup();

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

            SearchBar.Text = string.Empty;

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Debo revisar si la lista esta poblada
            if (true)
            {

            }

            // Devuelvo la lista de nombres filtrada
            List<UserModel> sortListModel = MasterySingleton.Instance._listUserModel;

            ListUserTeam.ItemsSource = sortListModel.Where(c => c.userFullName.ToUpper().Contains(e.NewTextValue.ToUpper()) && c.groupName == GroupPicker.SelectedItem.ToString());
        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByGroup();
        }

        private void FilterByGroup()
        {
            // Devuelvo la lista de nombres filtrada
            List<UserModel> sortListModel = MasterySingleton.Instance._listUserModel;

            ListUserTeam.ItemsSource = sortListModel.Where(c => c.groupName == GroupPicker.SelectedItem.ToString());
        }

    }
}