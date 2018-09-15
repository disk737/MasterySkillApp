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
        // Defino la calse
        BadgeServices _badgeServices;


        public BadgesView ()
		{
			InitializeComponent ();
            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();

            // Reviso si la lista ya fue llenada para que no se vuelva a llenar
            if (badgesList.ItemsSource != null)
                return;

            badgesList.BeginRefresh();

            // Llamo el metodo para refrescar la lista
            RefreshAttrList();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //// Reviso si la lista ya fue llenada para que no se vuelva a llenar
            //if (badgesList.ItemsSource != null)
            //    return;

            //badgesList.BeginRefresh();

            //// Llamo el metodo para refrescar la lista
            //RefreshAttrList();

        }

        private void badgesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            badgesList.SelectedItem = null;
        }

        private void badgesList_Refreshing(object sender, EventArgs e)
        {
            RefreshAttrList();
        }

        private async void RefreshAttrList()
        {
            // Vinculo el Source a la lista
            badgesList.ItemsSource = await _badgeServices.GetAttrPoints();

            badgesList.EndRefresh();
        }
    }
}