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
        var api = new ApiService();
        var response = await api.GetTasks("inactive", Session.CurrentUser.Id);

        var json = JsonDocument.Parse(response);

        if (json.RootElement.GetProperty("status").GetInt32() == 200)
        {
            var data = json.RootElement.GetProperty("data");

            var list = new ObservableCollection<TaskItem>();

            foreach (var item in data.EnumerateObject())
            {
                var task = item.Value;

                list.Add(new TaskItem
                {
                    TaskId = task.GetProperty("item_id").GetInt32(),
                    Title = task.GetProperty("item_name").GetString(),
                    Detail = task.GetProperty("item_description").GetString(),
                    Status = task.GetProperty("status").GetString(),
                    UserId = task.GetProperty("user_id").GetInt32(),
                    TimeModified = task.GetProperty("timemodified").GetString()
                });
            }

            taskView.ItemsSource = list;
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

            var api = new ApiService();
            await api.DeleteTask(task.TaskId);

            await LoadCompletedTasks(); // refresh list
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


}