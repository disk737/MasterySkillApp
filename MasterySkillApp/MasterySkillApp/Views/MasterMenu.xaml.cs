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
	public partial class MasterMenu : MasterDetailPage
	{
		public MasterMenu ()
		{
			InitializeComponent ();

            MasterBehavior = MasterBehavior.Popover;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Creo la clase que contiene los servicios de usuario
            UserServices _userServices = new UserServices();

            var responseUserInfo = await _userServices.GetUserInfo();

            MasterySingleton.Instance._userInfo = responseUserInfo.UserModels[0];

            BindingContext = MasterySingleton.Instance._userInfo;
        }
    }
}