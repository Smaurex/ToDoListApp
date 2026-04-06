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
        var api = new ApiService();

        await api.UpdateTask(_task.TaskId, Title.Text, Detail.Text);

        await Navigation.PopAsync();
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