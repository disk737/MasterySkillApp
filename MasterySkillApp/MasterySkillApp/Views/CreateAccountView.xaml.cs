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
            // Verifico que todos los campos tengan contenido
            if (string.IsNullOrEmpty(_EntryUserFirstName.Text) || string.IsNullOrEmpty(_EntryUserLastName.Text) || string.IsNullOrEmpty(_EntryEmail.Text) || string.IsNullOrEmpty(_EntryPassword.Text))
            {
                _LabelAllFields.IsVisible = true;
                return;
            }
            else
            {
                _LabelAllFields.IsVisible = false;
            }


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