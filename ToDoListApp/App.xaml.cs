using Microsoft.Extensions.DependencyInjection;

namespace ToDoListApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //opens the sign in page when the app is launched
            return new Window(new NavigationPage(new Pages.SignInPage()));
        }
    }
}