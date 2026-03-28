using ToDoListApp.Models;
using System.Collections.ObjectModel;
namespace ToDoListApp.Pages;

public partial class CompletedToDoPage : ContentPage
{
	public CompletedToDoPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
    {
        base.OnAppearing();
        loadTasks();

    }

    //this function will load the tasks from the TaskRepository and display them in the list view
    private void loadTasks()
    {
        var taskList = new ObservableCollection<TaskItem>(TaskRepository.GetCompletedTask());
        taskView.ItemsSource = taskList;
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

	private void Delete_Clicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        TaskItem task = button.CommandParameter as TaskItem;

        TaskRepository.DeleteTask(task.TaskId);
        loadTasks();
    }


}