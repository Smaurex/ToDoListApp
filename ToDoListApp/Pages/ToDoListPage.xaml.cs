using System.Collections.ObjectModel;
using System.Text.Json;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Pages;

public partial class ToDoListPage : ContentPage
{
	public ToDoListPage()
	{
		InitializeComponent();
    }

    //this runs every time the user looks at the page, so it will update the list every time
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (Session.CurrentUser != null)
        {
            username.Text = Session.CurrentUser.Username;
            await LoadTasksFromApi(); // API call
        }
    }

    //this function will load the tasks from the TaskRepository and display them in the list view
    private async Task LoadTasksFromApi()
    {
        try
        {
            if (Session.CurrentUser == null) return;

            var api = new ApiService();
            var response = await api.GetTasks("active", Session.CurrentUser.Id);

            var json = JsonDocument.Parse(response);
            int status = json.RootElement.GetProperty("status").GetInt32();

            if (status == 200)
            {
                if (!json.RootElement.TryGetProperty("data", out var data))
                {
                    taskView.ItemsSource = null;
                    return;
                }

                var taskList = new ObservableCollection<TaskItem>();

                // IMPORTANT: loop through dynamic keys (0,1,2,...)
                foreach (var item in data.EnumerateObject())
                {
                    var task = item.Value;

                    taskList.Add(new TaskItem
                    {
                        TaskId = task.GetProperty("item_id").GetInt32(),
                        Title = task.GetProperty("item_name").GetString(),
                        Detail = task.GetProperty("item_description").GetString(),
                        Status = task.GetProperty("status").GetString(),
                        UserId = task.GetProperty("user_id").GetInt32(),
                        TimeModified = task.TryGetProperty("timemodified", out var time) ? time.GetString() : ""
                    });
                }

                taskView.ItemsSource = taskList;
            }
            else
            {
                await DisplayAlert("Error", "Failed to load tasks", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "OK");
        }
    }
    //Navigate to the AddToDoPage when the user clicks the "Add Task" button
    private async void goToAddToDoPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToDoPage());
    }

    //Navigate to the EditToDoPage when the user clicks on a task in the list view
    private async void taskView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            TaskItem selectedTask = e.SelectedItem as TaskItem;

            await Navigation.PushAsync(new EditToDoPage(selectedTask));

            taskView.SelectedItem = null;
        }
    }
    //Delete the task when the user clicks the "Delete" button
    private async void Delete_Clicked(object sender, EventArgs e)
    {
        try
        {
            Button button = sender as Button;
            TaskItem task = button.CommandParameter as TaskItem;

            var api = new ApiService();
            await api.DeleteTask(task.TaskId);

            await LoadTasksFromApi(); // refresh list
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "OK");
        }
    }


    //Mark the task as complete when the user clicks the "Complete" button
    private async void Complete_Clicked(object sender, EventArgs e)
    {
        // gets response as complete 
        try
        {
            Button button = sender as Button;
            TaskItem task = button.CommandParameter as TaskItem;

            var api = new ApiService();
            await api.ChangeStatus(task.TaskId, "inactive");

            await LoadTasksFromApi(); // refresh list
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "OK");
        }
    }
}