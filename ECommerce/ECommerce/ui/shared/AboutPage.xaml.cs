using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
		}

        protected override bool OnBackButtonPressed()
        {
             Shell.Current.GoToAsync("//AcceuilPage");
             return true; // Prevent exiting the app
            
        }
    }
}