using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KeypadGrid
{
    public class App : Application
    {
        
		KeyPadGridViewModel keypadGridViewModel;
        public App()
        {
          
			keypadGridViewModel = new KeyPadGridViewModel();
			keypadGridViewModel.RestoreState(Current.Properties);
			MainPage = new KeypadGridPage(keypadGridViewModel);
        }

        public string DisplayLabelText { set; get; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            //Properties[displayLabelText] = DisplayLabelText;
        }

        protected override void OnResume()
        {
			Label l = new Label();

            // Handle when your app resumes
        }
    }
}
