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
	public partial class GiveBadgesView : ContentPage
	{
		public GiveBadgesView ()
		{
			InitializeComponent ();

            // Instancio el servicio de usuarios
            UserServices _userServices = new UserServices();

            // Vinculo el Source con la lista
            ListUsersBadges.ItemsSource = _userServices.GetUserModels();
		}
	}
}