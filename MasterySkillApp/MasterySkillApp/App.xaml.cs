using MasterySkillApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MasterySkillApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new LoginPageView());
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            AppCenter.Start("android=9bc0c492-c4de-4ea7-814f-0fc37c72b27a;" + "ios=f1be2f87-74e1-4e72-ae2b-5c1d5c53ed16;", typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
