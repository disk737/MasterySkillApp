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
            // Valido las credenciales ingresadas por el usuario
            var userToken = await _userServices.UserSignIn(EntryEmail.Text, EntryPassword.Text);

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
                // Usuario y/o contraseña no corresponde
                await DisplayAlert("Mastery", userToken.message, "OK");
            }

        }

    }
}