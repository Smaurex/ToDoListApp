namespace ToDoListApp.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}

    private void SignInButton_Clicked(object sender, EventArgs e)
    {
        //opens appshell and sets it as the main page of the app
        Application.Current.MainPage = new AppShell();
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}