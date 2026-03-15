namespace ToDoListApp.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
       await Navigation.PopAsync();
    }

    private async void SignIpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}