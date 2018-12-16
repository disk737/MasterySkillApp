using Acr.UserDialogs;
using MasterySkillApp.Models;
using MasterySkillApp.Services;
using Microsoft.AppCenter;
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
	public partial class CreateAccountView : ContentPage
	{
		public CreateAccountView ()
		{
			InitializeComponent ();
		}

        async private void SignUpBtn_ClickedAsync(object sender, EventArgs e)
        {
            // Verifico que todos los campos tengan contenido
            if (string.IsNullOrEmpty(_EntryUserFirstName.Text) || 
                string.IsNullOrEmpty(_EntryUserLastName.Text) || 
                string.IsNullOrEmpty(_EntryEmail.Text) || 
                string.IsNullOrEmpty(_EntryPassword.Text) ||
                string.IsNullOrEmpty(_EntryGroupCode.Text))
            {
                _LabelAllFields.IsVisible = true;
                return;
            }
            else
            {
                _LabelAllFields.IsVisible = false;
            }

            // Llamo el servicio encargado de crear la cuenta
            UserServices userServices = new UserServices();
            UserToken userToken = new UserToken();


            // Implemento un Loading para el Login
            using (UserDialogs.Instance.Loading("Validando...", null, null, true, MaskType.Black))
            {
                // Consigo el ID de Instalacion
                System.Guid? installId = await AppCenter.GetInstallIdAsync();

                userToken = await userServices.UserSignUp(_EntryUserFirstName.Text,
                                                                    _EntryUserLastName.Text,
                                                                    _EntryEmail.Text,
                                                                    _EntryPassword.Text,
                                                                    _EntryGroupCode.Text,
                                                                    installId.ToString());
            }

            // Reviso si encontre mensajes de error
            if (!userToken.IsSuccessStatusCode)
            {
                // Usuario y/o contraseña no corresponde
                await DisplayAlert("Oppssss", userToken.message, "OK");
                return;
            }

            // Guardo el token en el dispositivo
            GeneralServices.SaveCredentialsMethod(userToken.token,false);

            // Ingreso al usuario en la plataforma e ingreso el menu principal
            Application.Current.MainPage = new MasterMenu();
        }

        private void EntryUserFirstName_Completed(object sender, EventArgs e)
        {
            _EntryUserLastName.Focus();
        }

        private void EntryUserLastName_Completed(object sender, EventArgs e)
        {
            _EntryEmail.Focus();
        }

        private void EntryEmail_Completed(object sender, EventArgs e)
        {
            _EntryPassword.Focus();
        }

        private void _EntryPassword_Completed(object sender, EventArgs e)
        {
            _EntryGroupCode.Focus();
        }
    }
}