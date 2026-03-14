namespace ToDoListApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            //responsible for registering the routes for the app, so that we can navigate to different pages using the route names
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.SignInPage), typeof(Pages.SignInPage));
            Routing.RegisterRoute(nameof(Pages.SignUpPage), typeof(Pages.SignUpPage));
            Routing.RegisterRoute(nameof(Pages.ToDoListPage), typeof(Pages.ToDoListPage));
            Routing.RegisterRoute(nameof(Pages.AddToDoPage), typeof(Pages.AddToDoPage));
            Routing.RegisterRoute(nameof(Pages.EditToDoPage), typeof(Pages.EditToDoPage));
            Routing.RegisterRoute(nameof(Pages.CompletedToDoPage), typeof(Pages.CompletedToDoPage));
            Routing.RegisterRoute(nameof(Pages.EditCompletedPage), typeof(Pages.EditCompletedPage));
            Routing.RegisterRoute(nameof(Pages.ProfilePage), typeof(Pages.ProfilePage));
        }
    }
}
