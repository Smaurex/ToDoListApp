using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class EditToDoPage : ContentPage
{

    private TaskItem _task;
    public EditToDoPage(TaskItem selectedTask)
	{
       
        InitializeComponent();

        _task = selectedTask;

       Title.Text = _task.Title;
       Detail.Text = _task.Detail;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        _task.Title = Title.Text;
        _task.Detail = Detail.Text;

        TaskRepository.UpdateTask(_task.TaskId, _task);
        await Navigation.PopAsync();
    }
    private void Complete_Clicked(object sender, EventArgs e)
    {
        _task.isComplete = true;
        TaskRepository.UpdateTask(_task.TaskId, _task);
        Navigation.PopAsync();
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        TaskRepository.DeleteTask(_task.TaskId);

        await Navigation.PopAsync();
    }

}