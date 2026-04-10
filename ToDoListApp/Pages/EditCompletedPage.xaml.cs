using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Pages;

public partial class EditCompletedPage : ContentPage
{
	 private TaskItem _task;
    public EditCompletedPage (TaskItem selectedTask)
	{
       
        InitializeComponent();

        _task = selectedTask;

       Title.Text = _task.Title;
       Detail.Text = _task.Detail;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        try
        {
            var api = new ApiService();

            var response = await api.UpdateTask(_task.TaskId, Title.Text, Detail.Text);

            // 🔥 SHOW RAW RESPONSE FIRST
            await DisplayAlert("DEBUG RESPONSE", response, "OK");

            // ❗ Check if response looks like JSON
            if (!response.Trim().StartsWith("{"))
            {
                await DisplayAlert("Error", "Invalid server response", "OK");
                return;
            }

            var json = JsonDocument.Parse(response);

            int status = json.RootElement.GetProperty("status").GetInt32();

            if (status == 200)
            {
                await DisplayAlert("Success", "Task updated!", "OK");
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
            await DisplayAlert("FULL ERROR", ex.ToString(), "OK");
        }
    }
    private async void Incomplete_Clicked(object sender, EventArgs e)
    {
        var api = new ApiService();

        await api.ChangeStatus(_task.TaskId, "active");

        await Navigation.PopAsync();
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var api = new ApiService();

        await api.DeleteTask(_task.TaskId);

        await Navigation.PopAsync();
    }
}