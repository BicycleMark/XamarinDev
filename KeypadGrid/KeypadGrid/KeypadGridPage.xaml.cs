using System;
using Xamarin.Forms;

namespace KeypadGrid
{
    public partial class KeypadGridPage : ContentPage
    {
       
        public KeypadGridPage()
        {
            InitializeComponent();

            
        }
		public KeypadGridPage(KeyPadGridViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;


		}

  
    }
}
