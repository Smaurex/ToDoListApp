namespace ToDoListApp.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private void SignUpButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void SignIpButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}