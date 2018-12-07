using Acr.UserDialogs;
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
    public partial class ChangePasswordView : ContentPage
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private async void ChangeButton_Clicked(object sender, EventArgs e)
        {
            // Reviso que los campos de la interfaz tengan datos
            if (string.IsNullOrEmpty(_EntryOldPassword.Text))
            {
                _LabelOldPassword.IsVisible = true;
                return;
            }
            else
            {
                _LabelOldPassword.IsVisible = false;              
            }

            if (string.IsNullOrEmpty(_EntryNewPassword.Text))
            {
                _LabelNewPassword.IsVisible = true;
                return;
            }
            else
            {
                _LabelNewPassword.IsVisible = false;
            }


            // Instancio  los servicios de usuario
            UserServices _userServices = new UserServices();

            BaseResponse responseServer = new BaseResponse();

            // Implemento un Loading para el Login
            using (UserDialogs.Instance.Loading("Validando...", null, null, true, MaskType.Black))
            {
                // Invoco el servicio para la actualizacion de la contraseña
                responseServer = await _userServices.UpdateUserPassword(_EntryOldPassword.Text, _EntryNewPassword.Text);
            }

            Analytics.TrackEvent("User Change Password");

            if (responseServer.StatusCode == HttpStatusCode.NotFound)
            {
                await DisplayAlert("Oooooppsss", responseServer.message, "OK");
                return;
            }

            // Reviso si hubo algun fallo en el servidor
            if (!responseServer.IsSuccessStatusCode)
            {
                await DisplayAlert("Oooooppsss, fallo en el cambio de contraseña", String.Format("Error code: {0}", responseServer.StatusCode), "OK");
                return;
            }

            // Intercambio las vistas del modal
            _Layout_Step1.IsVisible = false;
            _Layout_Step2.IsVisible = true;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // Regreso a la interfaz anterior
            Navigation.PopModalAsync();
        }

        private void AcceptButton_Clicked(object sender, EventArgs e)
        {
            // Regreso a la interfaz anterior
            Navigation.PopModalAsync();
        }

        private void _EntryOldPassword_Completed(object sender, EventArgs e)
        {
            _EntryNewPassword.Focus();
        }
    }
}