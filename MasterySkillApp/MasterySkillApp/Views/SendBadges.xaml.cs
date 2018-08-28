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

        public SendBadges (UserModel argUserModel)
		{
			InitializeComponent ();

            // Guardo el contenido del argumento es una propiedad global
            userModel = argUserModel;
            BindingContext = userModel;

            // Instancio el controlador de servicios
            BadgeServices _badgeServices = new BadgeServices();

            // Vinculo el Source de la lista al resultado del servicio
            ListSendBadges.ItemsSource = _badgeServices.GetBadgeModels();
		}

        private void ListSendBadges_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Retorno si no tengo nada seleccionado
            if (ListSendBadges.SelectedItem == null)
                return;
            
            var badgeSelected = e.SelectedItem as BadgeModel;

            DisplayAlert(String.Format("Medalla {0}",badgeSelected.BadgeName),String.Format("Enviar a {0}?",userModel.UserName),"Enviar","Cancel");

            ListSendBadges.SelectedItem = null;
        }
    }
}