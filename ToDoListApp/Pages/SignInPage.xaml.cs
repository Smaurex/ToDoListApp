namespace ToDoListApp.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}

    private void SignInButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }

    private void SignUpButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());
    }
}