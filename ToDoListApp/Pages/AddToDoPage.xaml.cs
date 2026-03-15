using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public partial class AddToDoPage : ContentPage
{
	public AddToDoPage()
	{
		InitializeComponent();
	}

    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        TaskRepository.AddTask(new TaskItem
        {
            TaskId = TaskRepository.NewId(),
            Title = Title.Text,
            Detail = Detail.Text
        });

        await Navigation.PopAsync();
    }
}