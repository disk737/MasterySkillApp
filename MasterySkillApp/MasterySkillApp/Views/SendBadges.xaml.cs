using Acr.UserDialogs;
using MasterySkillApp.Models;
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
	public partial class SendBadges : ContentPage
	{
        // Actividad que se encarga de cargar las medallas para que sean enviadas
        private UserModel userModel { get; set; }

        // Lista donde guardo los atributos basicos
        private List<BasicAttrModel> _basicAttr { get; set; }

        // Creo el objeto de los servicios
        BadgeServices _badgeServices;

        public SendBadges (UserModel argUserModel)
		{
			InitializeComponent ();

            // Guardo el contenido del argumento es una propiedad global
            userModel = argUserModel;
            BindingContext = userModel;

            // Instancio el controlador de servicios
            _badgeServices = new BadgeServices();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent("SendBadges");

            ListSendBadges.BeginRefresh();

            // Reviso si la lista fue cargada en un momento anterior
            if (MasterySingleton.Instance._listBasicAttr == null)
            {
                // Hago la llamada al servicio de la lista de atributos
                MasterySingleton.Instance._listBasicAttr = (await _badgeServices.GetBasicAttr()).BasicAttrs;
            }
                
            // Vinculo el Source de la lista al resultado del servicio
            ListSendBadges.ItemsSource = MasterySingleton.Instance._listBasicAttr;

            ListSendBadges.EndRefresh();

        }

        // Handler para enviar un punto a otro usuario
        private async void ListSendBadges_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Retorno si no tengo nada seleccionado
            if (ListSendBadges.SelectedItem == null)
                return;

            ListSendBadges.SelectedItem = null;

            var attrSelected = e.SelectedItem as BasicAttrModel;

            // Creo un objeto de configuracion
            var promptConfig = new PromptConfig();
            promptConfig.InputType = InputType.Name;
            promptConfig.IsCancellable = true;

            promptConfig.Title = string.Format("Medalla {0}", attrSelected.attrName);
            promptConfig.Message = string.Format("Enviar a {0}?", userModel.userName);
            promptConfig.Placeholder = "Mensaje Opcional";
            promptConfig.OkText = "Enviar";
            promptConfig.CancelText = "Cancelar";
            promptConfig.SetMaxLength(255);

            // Despliego el PopUp
            var result = await UserDialogs.Instance.PromptAsync(promptConfig);
            if (result.Ok)
            {
                //var Text = result.Text;

                // Invoco el servicio para el envio de una medalla a un usuario
                var resFail = await _badgeServices.SendAttrPoint(userModel,attrSelected, result.Text);

                Analytics.TrackEvent("BadgeSended");

                // Reviso si hubo algun fallo
                if (resFail != "")
                    await DisplayAlert("Oooooppsss, fallo en el envio",resFail, "OK");

            }

        }
    }
}