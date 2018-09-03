using MasterySkillApp.Models;
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
	public partial class SendBadges : ContentPage
	{
        // Actividad que se encarga de cargar las medallas para que sean enviadas
        private UserModel userModel { get; set; }

        // Lista donde guardo los atributos basicos
        private List<BasicAttrModel> _basicAttr { get; set; }

        public SendBadges (UserModel argUserModel)
		{
			InitializeComponent ();

            // Guardo el contenido del argumento es una propiedad global
            userModel = argUserModel;
            BindingContext = userModel;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ListSendBadges.BeginRefresh();

            // Reviso si la lista fue cargada en un momento anterior
            if (MasterySingleton.Instance._listBasicAttr == null)
            {
                // Instancio el controlador de servicios
                BadgeServices _badgeServices = new BadgeServices();

                MasterySingleton.Instance._listBasicAttr = await _badgeServices.GetBasicAttr();
            }
                
            // Vinculo el Source de la lista al resultado del servicio
            ListSendBadges.ItemsSource = MasterySingleton.Instance._listBasicAttr;

            ListSendBadges.EndRefresh();

        }

        private void ListSendBadges_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Retorno si no tengo nada seleccionado
            if (ListSendBadges.SelectedItem == null)
                return;
            
            var attrSelected = e.SelectedItem as BasicAttrModel;

            DisplayAlert(string.Format("Token {0}", attrSelected.attrName), string.Format("Enviar a {0}?",userModel.userName),"Enviar","Cancel");

            ListSendBadges.SelectedItem = null;
        }
    }
}