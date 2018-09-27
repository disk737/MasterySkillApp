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
                Application.Current.MainPage = new NavigationPage(new MasterTabView());
               // await Navigation.PushAsync(new MasterTabView());
            }
            
        }

        async private void LoginHandler_Clicked(object sender, EventArgs e)
        {
            // Creo el objeto para guardar el token del usuario
            UserToken userToken;

            // Implemento un Loading para el Login
            using (UserDialogs.Instance.Loading("Validando...", null, null, true, MaskType.Black))
            {
                // Consigo el ID de Instalacion
                System.Guid? installId = await AppCenter.GetInstallIdAsync();

                // Valido las credenciales ingresadas por el usuario
                userToken = await _userServices.UserSignIn(EntryEmail.Text, EntryPassword.Text, installId.ToString());
            };

            // Reviso si obtengo un Token o un mensaje de error 
            if (userToken.token != null)
            {
                // Guardo el token generado para el usuario
                Application.Current.Properties[Constans.UserTokenString] = userToken.token;

                // Si el usuario decidio guardar sus credenciales, procedo a guardarlas
                if (SaveCredencials.IsToggled == true)
                    Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveActive;
                else
                    Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveUnactive;

                // Llamo la pagina principal de Tabs
                //await Navigation.PushAsync(new MasterTabView());
                Application.Current.MainPage = new NavigationPage(new MasterTabView());
            }
            else
            {
                // Esto lo puedo cambiar con ACR
                // Usuario y/o contraseña no corresponde
                await DisplayAlert("Mastery", userToken.message, "OK");
            }

        }

    }
}