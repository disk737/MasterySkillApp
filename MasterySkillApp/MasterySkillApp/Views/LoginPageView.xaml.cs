using Acr.UserDialogs;
using MasterySkillApp.Models;
using MasterySkillApp.Services;
using Microsoft.AppCenter;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterySkillApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPageView : ContentPage
	{
        private UserServices _userServices;

        // Guardo el Token de autentificacion
        private string oldToken;

        //private bool activityActivated = false;

        public LoginPageView ()
		{
			InitializeComponent ();

            _userServices = new UserServices();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // ESTO SE DEBE VERIFICAR YA QUE SI EL TOKEN ESTA VIEJO LA APP LO DEJARA PASAR Y TENDRA PROBLEMAS MAS ADELANTE
            // Reviso si tengo un Token viejo que pueda usar. Esto es un seguro
            if (Application.Current.Properties.ContainsKey(Constans.UserTokenString))
            {
                // Extraigo el viejo Token
                oldToken = Application.Current.Properties[Constans.UserTokenString].ToString();
            }

            if (oldToken != null &&
                Application.Current.Properties.ContainsKey(Constans.SaveCredentials) &&
                Application.Current.Properties[Constans.SaveCredentials] as string == Constans.SaveActive)
            {
                // Llamo la pagina principal de Tabs              
                //Application.Current.MainPage = new NavigationPage(new MasterTabView());
                Application.Current.MainPage = new MasterMenu();
                // await Navigation.PushAsync(new MasterTabView());
            }
            
        }

        async private void LoginHandler_Clicked(object sender, EventArgs e)
        {
            // Verifico que se ingrese la informacion de Correo
            if (string.IsNullOrEmpty(_EntryEmail.Text))
            {
                _LabelEmail.IsVisible = true;
                return;
            }
            else
            {
                _LabelEmail.IsVisible = false;
            }

            // Verifico que se ingrese la informacion de contraseña
            if (string.IsNullOrEmpty(_EntryPassword.Text))
            {
                _LabelPassword.IsVisible = true;
                return;
            }
            else
            {
                _LabelPassword.IsVisible = false;
            }


            // Creo el objeto para guardar el token del usuario
            UserToken userToken;

            // Implemento un Loading para el Login
            using (UserDialogs.Instance.Loading("Validando...", null, null, true, MaskType.Black))
            {
                // Consigo el ID de Instalacion
                System.Guid? installId = await AppCenter.GetInstallIdAsync();

                // Valido las credenciales ingresadas por el usuario
                userToken = await _userServices.UserSignIn(_EntryEmail.Text, _EntryPassword.Text, installId.ToString());
            };

            // Reviso si encontre mensajes de error
            if (!userToken.IsSuccessStatusCode)
            {
                // Usuario y/o contraseña no corresponde
                await DisplayAlert("Oppssss", userToken.message, "OK");
                return;
            }

            // Reviso si tengo un token que pueda usar
            if (userToken.token == null)
            {
                // Usuario y/o contraseña no corresponde
                await DisplayAlert("Oppssss", "Tenemos un problema con el Token. Contacta al Admin", "OK");
                return;
            }

            // Guardo el token en el dispositivo
            GeneralServices.SaveCredentialsMethod(userToken.token, SaveCredencials.IsToggled);
                   
            // Llamo la pagina principal de Tabs
            Application.Current.MainPage = new MasterMenu();

        }




        private void _EntryEmail_Completed(object sender, EventArgs e)
        {
            _EntryPassword.Focus();
        }

        private void CrearCuenta_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccountView());
        }
    }
}