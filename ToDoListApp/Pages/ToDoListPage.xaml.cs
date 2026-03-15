using System.Collections.ObjectModel;
using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class ToDoListPage : ContentPage
{
	public ToDoListPage()
	{
		InitializeComponent();
    }

    //this runs every time the user looks at the page, so it will update the list every time
    protected override void OnAppearing()
    {
        base.OnAppearing();
        loadTasks();

        if (Session.CurrentUser != null)
        {
            username.Text = Session.CurrentUser.Username;
        }
    }

    //this function will load the tasks from the TaskRepository and display them in the list view
    private void loadTasks()
    {
        var taskList = new ObservableCollection<TaskItem>(TaskRepository.GetTask());
        taskView.ItemsSource = taskList;
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
    private void Delete_Clicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        TaskItem task = button.CommandParameter as TaskItem;

        TaskRepository.DeleteTask(task.TaskId);
        loadTasks();
    }
    //Mark the task as complete when the user clicks the "Complete" button
    private void Complete_Clicked(object sender, EventArgs e)
    {
        
    }
}