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
	public partial class CreateAccountView : ContentPage
	{
		public CreateAccountView ()
		{
			InitializeComponent ();
		}

        private void SignUpBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void EntryUserFirstName_Completed(object sender, EventArgs e)
        {
            _EntryUserLastName.Focus();
        }

        private void EntryUserLastName_Completed(object sender, EventArgs e)
        {
            _EntryEmail.Focus();
        }

        private void EntryEmail_Completed(object sender, EventArgs e)
        {
            _EntryPassword.Focus();
        }

    }
}