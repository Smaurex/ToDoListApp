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
            var api = new ApiService();

            var response = await api.AddTask(
                Title.Text,
                Detail.Text,
                Session.CurrentUser.Id
            );

            var json = JsonDocument.Parse(response);
            int status = json.RootElement.GetProperty("status").GetInt32();

            if (status == 200)
            {
                await DisplayAlert("Success", "Task added!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to add task", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}