using System.Net.Http;
using System.Text;
using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Username.Text) ||
            string.IsNullOrWhiteSpace(Email.Text) ||
            string.IsNullOrWhiteSpace(Password.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPassword.Text))
        {
            await DisplayAlert("Error", "All fields are required", "OK");
            return;
        }

        if (Password.Text != ConfirmPassword.Text)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        var api = new ApiService();

        // NOTE: Your API requires first_name and last_name
        var response = await api.SignUp(
            fname.Text,      // fname
            lname.Text,             // lname (temporary)
            Email.Text,
            Password.Text,
            ConfirmPassword.Text
        );

        var json = JsonDocument.Parse(response);
        int status = json.RootElement.GetProperty("status").GetInt32();
        string message = json.RootElement.GetProperty("message").GetString();

        if (status == 200)
        {
            await DisplayAlert("Success", message, "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", message, "OK");
        }
    }

    private async void SignIpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}