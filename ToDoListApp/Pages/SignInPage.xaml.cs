using System.Net.Http;
using System.Text;
using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;

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

        var api = new ApiService();
        var response = await api.SignIn(Email.Text, Password.Text);

        var json = JsonDocument.Parse(response);
        int status = json.RootElement.GetProperty("status").GetInt32();

        if (status == 200)
        {
            await DisplayAlert("Success", "Login successful", "OK");

            // OPTIONAL: store user data
            var data = json.RootElement.GetProperty("data");

            Session.CurrentUser = new User
            {
                Username = data.GetProperty("fname").GetString(),
                Email = data.GetProperty("email").GetString()
            };

            Application.Current.MainPage = new AppShell();
        }
        else
        {
            string message = json.RootElement.GetProperty("message").GetString();
            await DisplayAlert("Error", message, "OK");
        }
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}