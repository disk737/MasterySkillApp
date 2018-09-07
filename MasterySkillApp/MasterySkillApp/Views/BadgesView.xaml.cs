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
	public partial class BadgesView : ContentPage
	{
		public BadgesView ()
		{
			InitializeComponent ();         
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Reviso si la lista ya fue llenada para que no se vuelva a llenar
            if (badgesList.ItemsSource != null)
                return;

            badgesList.BeginRefresh();

            // Instacion el BadgeServices
            BadgeServices _badgeServices = new BadgeServices();

            // Vinculo el Source a la lista
            badgesList.ItemsSource = await _badgeServices.GetAttrPoints();

            badgesList.EndRefresh();

        }
    }
}