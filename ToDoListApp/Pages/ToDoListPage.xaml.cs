using System.Collections.ObjectModel;
using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class ToDoListPage : ContentPage
{
	public ToDoListPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        loadTasks();
    }

    private void goToAddToDoPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddToDoPage());
    }



    /*private async void taskView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //logic here
     if (taskView.SelectedItem != null)
        {
            //await Shell.Current.GoToAsync($"{nameof(EditTaskPage)}?id={((TaskItem)taskView.SelectedItem).TaskId}");
            Navigation.PushAsync(new EditToDoPage());
        }
    }

    private void taskView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        taskView.SelectedItem = null; // Deselect the item after selection
    }*/

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var taskToDelete = menuItem.CommandParameter as TaskItem;

        TaskRepository.DeleteTask(taskToDelete.TaskId);
        loadTasks();
    }

    private void loadTasks()
    {
        var taskList = new ObservableCollection<TaskItem>(TaskRepository.GetTask());
        taskView.ItemsSource = taskList;
    }

    private void taskView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //logic here
        if (taskView.SelectedItem != null)
        {
            //await Shell.Current.GoToAsync($"{nameof(EditTaskPage)}?id={((TaskItem)taskView.SelectedItem).TaskId}");
            Navigation.PushAsync(new EditToDoPage());
            taskView.SelectedItem = null; // Deselect the item after selection
        }
    }
}