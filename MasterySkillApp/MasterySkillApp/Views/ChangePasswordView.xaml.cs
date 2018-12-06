using Acr.UserDialogs;
using MasterySkillApp.Services;
using Microsoft.AppCenter.Analytics;
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
    public partial class ChangePasswordView : ContentPage
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private async void ChangeButton_Clicked(object sender, EventArgs e)
        {
            // Instancio  los servicios de usuario
            UserServices _userServices = new UserServices();

            string resFail;

            // Implemento un Loading para el Login
            using (UserDialogs.Instance.Loading("Validando...", null, null, true, MaskType.Black))
            {
                // Invoco el servicio para la actualizacion de la contraseña
                resFail = await _userServices.UpdateUserPassword(Entry_OldPassword.Text, Entry_NewPassword.Text);
            }
            Analytics.TrackEvent("User Change Password");

            // Reviso si hubo algun fallo
            if (resFail != "")
            {
                await DisplayAlert("Oooooppsss, fallo en el cambio de contraseña", resFail, "OK");
                return;
            }

            // Intercambio las vistas del modal
            Layout_Step1.IsVisible = false;
            Layout_Step2.IsVisible = true;

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
    }
}