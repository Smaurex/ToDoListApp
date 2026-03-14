using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class EditToDoPage : ContentPage
{

    private TaskItem _task;
    public EditToDoPage(TaskItem task)
	{
       
        InitializeComponent();

        _task = task;

       //taskTitleEntry.Text = _task.Title;
       //taskDetailEntry.Text = _task.Detail;
    }
}