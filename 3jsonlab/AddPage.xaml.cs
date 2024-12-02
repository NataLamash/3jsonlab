namespace _3jsonlab;

public partial class AddPage : ContentPage
{
    public event Action<Section> SectionAdded;

    public AddPage()
    {
        InitializeComponent();
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        // Перевірка на заповнення всіх полів
        if (string.IsNullOrWhiteSpace(EntryId.Text) ||
            string.IsNullOrWhiteSpace(EntryName.Text) ||
            string.IsNullOrWhiteSpace(EntryCoach.Text) ||
            string.IsNullOrWhiteSpace(EntryLocation.Text) ||
            string.IsNullOrWhiteSpace(EntryDay.Text) ||
            string.IsNullOrWhiteSpace(EntryTime.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        // Створення нового об'єкта секції
        var newSection = new Section
        {
            Id = EntryId.Text,
            Name = EntryName.Text,
            Coach = EntryCoach.Text,
            Location = EntryLocation.Text,
            Schedule = new List<Schedule>
                {
                    new Schedule { Day = EntryDay.Text, Time = EntryTime.Text }
                }
        };

        // Виклик події SectionAdded для додавання секції
        SectionAdded?.Invoke(newSection);

        // Закриття вікна
        await Navigation.PopModalAsync();
    }
}