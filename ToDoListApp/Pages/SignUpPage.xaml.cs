using ToDoListApp.Models;

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

        var existingUser = UserRepository.GetUserByEmail(Email.Text);

        if (existingUser != null)
        {
            await DisplayAlert("Error", "Email already registered", "OK");
            return;
        }

        User newUser = new User
        {
            Username = Username.Text,
            Email = Email.Text,
            Password = Password.Text
        };

        UserRepository.AddUser(newUser);

        await DisplayAlert("Success", "Account created!", "OK");

        await Navigation.PopAsync(); // return to SignIn
    }

    private async void SignIpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}