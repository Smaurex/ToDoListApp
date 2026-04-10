using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Pages;

public partial class AddToDoPage : ContentPage
{
	public AddToDoPage()
	{
		InitializeComponent();
	}

    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Title.Text) ||
                string.IsNullOrWhiteSpace(Detail.Text))
            {
                await DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            if (Session.CurrentUser == null)
            {
                await DisplayAlert("Error", "User not logged in", "OK");
                return;
            }

            var api = new ApiService();

            var response = await api.AddTask(
                Title.Text,
                Detail.Text,
                Session.CurrentUser.Id
            );

            // 🔥 DEBUG: SEE ACTUAL RESPONSE
            await DisplayAlert("DEBUG", response, "OK");

            var json = JsonDocument.Parse(response);

            int status = json.RootElement.GetProperty("status").GetInt32();

            if (status == 200)
            {
                await DisplayAlert("Success", "Task added!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                string message = json.RootElement.GetProperty("message").GetString();
                await DisplayAlert("Error", message, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "OK");
        }
    }
}