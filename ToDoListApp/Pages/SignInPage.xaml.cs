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
        try
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
                var data = json.RootElement.GetProperty("data");

                var user = new User();
                user.Id = data.GetProperty("id").GetInt32();
                user.FirstName = data.GetProperty("fname").GetString();
                user.LastName = data.GetProperty("lname").GetString();
                user.Email = data.GetProperty("email").GetString();

                Session.CurrentUser = user;

                await DisplayAlert("Success", "Login successful", "OK");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                string message = json.RootElement.GetProperty("message").GetString();
                await DisplayAlert("Error", message, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}