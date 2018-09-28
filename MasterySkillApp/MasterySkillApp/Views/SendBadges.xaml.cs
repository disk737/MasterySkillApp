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

            var sendRes = await DisplayAlert(string.Format("Medalla {0}", attrSelected.attrName), string.Format("Enviar a {0}?",userModel.userName),"Enviar","Cancel");

            // Evaluo la respuesta del usuario
            if (sendRes)
            {
                // Invoco el servicio para el envio de una medalla a un usuario
                var resFail = await _badgeServices.SendAttrPoint(userModel,attrSelected);

                Analytics.TrackEvent("BadgeSended");

                // Reviso si hubo algun fallo
                if (resFail != "")
                    await DisplayAlert("Oooooppsss",resFail, "OK");
            }
        }
    }
}