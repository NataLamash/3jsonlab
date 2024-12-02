namespace _3jsonlab;

public partial class MorePage : ContentPage
{
    public MorePage(List<Schedule> schedule)
    {
        InitializeComponent();
        BindingContext = schedule; // Встановлюємо розклад як контекст
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(); // Закриваємо модальне вікно
    }
}