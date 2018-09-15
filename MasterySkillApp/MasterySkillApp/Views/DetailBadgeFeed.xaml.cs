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
    public partial class DetailBadgeFeed : ContentPage
    {

        BadgeServices _badgeServices;

        public DetailBadgeFeed()
        {
            InitializeComponent();

            // Instancio el BadgeServices
            _badgeServices = new BadgeServices();

            // Reviso si la lista ya fue llenada para que no se vuelva a llenar
            if (ListNewsFeed.ItemsSource != null)
                return;

            ListNewsFeed.BeginRefresh();

            RefreshNewsFeed();

        }

        private void ListNewsFeed_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListNewsFeed.SelectedItem = null;
        }

        private void ListNewsFeed_Refreshing(object sender, EventArgs e)
        {
            RefreshNewsFeed();
        }

        private async void RefreshNewsFeed()
        {
            // Vinculo el Source a la lista
            ListNewsFeed.ItemsSource = await _badgeServices.GetDetailCount();

            ListNewsFeed.EndRefresh();
        }
    }
}