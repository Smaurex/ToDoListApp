using ToDoListApp.Models;
namespace ToDoListApp.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Session.CurrentUser != null)
        {
            UsernameLabel.Text = Session.CurrentUser.Username;
            EmailLabel.Text = Session.CurrentUser.Email;
        }
    }

    private void Logout_Clicked(object sender, EventArgs e)
    {
        // Clear the session
        Session.Logout();

        // Return to Sign In page
        Application.Current.MainPage = new NavigationPage(new SignInPage());
    }
}