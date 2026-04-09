using System.Collections.ObjectModel;
using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;
namespace ToDoListApp.Pages;

public partial class CompletedToDoPage : ContentPage
{
	public CompletedToDoPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCompletedTasks();

    }

    //this function will load the tasks from the TaskRepository and display them in the list view
    private async Task LoadCompletedTasks()
    {
        try
        {
            if (Session.CurrentUser == null)
            {
                await DisplayAlert("Error", "User not logged in", "OK");
                return;
            }

            var api = new ApiService();
            var response = await api.GetTasks("inactive", Session.CurrentUser.Id);

            var json = JsonDocument.Parse(response);

            // ✅ Check status exists
            if (!json.RootElement.TryGetProperty("status", out var statusProp))
            {
                await DisplayAlert("Error", "Invalid response from server", "OK");
                return;
            }

            int status = statusProp.GetInt32();

            if (status != 200)
            {
                await DisplayAlert("Error", "Failed to load completed tasks", "OK");
                return;
            }

            // ✅ Check if data exists
            if (!json.RootElement.TryGetProperty("data", out var data))
            {
                taskView.ItemsSource = null;
                return;
            }

            // ✅ Handle null data
            if (data.ValueKind == JsonValueKind.Null)
            {
                taskView.ItemsSource = null;
                return;
            }

            var list = new ObservableCollection<TaskItem>();

            foreach (var item in data.EnumerateObject())
            {
                var task = item.Value;

                list.Add(new TaskItem
                {
                    TaskId = task.GetProperty("item_id").GetInt32(),

                    Title = task.GetProperty("item_name").GetString() ?? "",
                    Detail = task.GetProperty("item_description").GetString() ?? "",
                    Status = task.GetProperty("status").GetString() ?? "",

                    UserId = task.GetProperty("user_id").GetInt32(),

                    // ✅ Safe timemodified (prevents crash)
                    TimeModified = task.TryGetProperty("timemodified", out var time)
                        ? time.GetString()
                        : ""
                });
            }

            taskView.ItemsSource = list;
        }
        catch (Exception ex)
        {
            // ✅ SHOW REAL ERROR (important for debugging)
            await DisplayAlert("Error", ex.ToString(), "OK");
        }
    }


    private async void taskView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            TaskItem selectedTask = e.SelectedItem as TaskItem;
            await Navigation.PushAsync(new EditCompletedPage(selectedTask));
            taskView.SelectedItem = null;
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        try
        {
            Button button = sender as Button;
            TaskItem task = button.CommandParameter as TaskItem;

            if (task == null) return;

            var api = new ApiService();
            var response = await api.DeleteTask(task.TaskId);

            var json = JsonDocument.Parse(response);

            if (json.RootElement.GetProperty("status").GetInt32() == 200)
            {
                await LoadCompletedTasks(); // refresh
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