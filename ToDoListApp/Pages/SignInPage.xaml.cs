using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}

    private async void SignInButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Email.Text) ||
        string.IsNullOrWhiteSpace(Password.Text))
        {
            await DisplayAlert("Error", "Please enter email and password", "OK");
            return;
        }

        var user = UserRepository.GetUserByEmail(Email.Text);

        if (user == null || user.Password != Password.Text)
        {
            await DisplayAlert("Error", "Invalid email or password", "OK");
            return;
        }

        // store session
        Session.CurrentUser = user;

        // open main app
        Application.Current.MainPage = new AppShell();
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}