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
	public partial class MasterMenu : MasterDetailPage
	{
		public MasterMenu ()
		{
			InitializeComponent ();

            MasterBehavior = MasterBehavior.Popover;
        }

        // Creo la clase que contiene los servicios de usuario
        UserServices _userServices;

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Instancio los servicios de usuario
            _userServices = new UserServices();

            var responseUserInfo = await _userServices.GetUserInfo();

            MasterySingleton.Instance._userInfo = responseUserInfo.UserModels[0];

            BindingContext = MasterySingleton.Instance._userInfo;
        }

        private async void EditStatus_Clicked(object sender, EventArgs e)
        {
            // Creo un objeto de configuracion
            var promptConfig = new PromptConfig();
            promptConfig.InputType = InputType.Name;
            promptConfig.IsCancellable = true;

            promptConfig.Title = "Tu estado";
            promptConfig.Message = String.Format("\"{0}\"", MasterySingleton.Instance._userInfo.userStatus);
            promptConfig.Placeholder = "Nuevo estado";
            promptConfig.OkText = "Cambiar";
            promptConfig.CancelText = "Cancelar";
            promptConfig.SetMaxLength(255);

            // Despliego el PopUp
            var result = await UserDialogs.Instance.PromptAsync(promptConfig);
            if (result.Ok)
            {
                var Text = result.Text;

                // Actualizo el estado que el usuario ve en el menu Master
                MasterySingleton.Instance._userInfo.userStatus = Text;
                LabelStatus.Text = MasterySingleton.Instance._userInfo.userStatus;

                // Invoco el servicio para la actualizacion del estado
                var resFail = await _userServices.UpdateUserStatus(Text);

                Analytics.TrackEvent("User Change Status");

                // Reviso si hubo algun fallo
                if (resFail != "")
                    await DisplayAlert("Oooooppsss, fallo en el cambio de estado", resFail, "OK");

            }
        }

        private void ChangePassword_Clicked(object sender, EventArgs e)
        {
            // Llamo la vista encargada de cambiar la contraseña
            Navigation.PushModalAsync(new ChangePasswordView());
        }
    }
}