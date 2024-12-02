namespace _3jsonlab;

public partial class FilterPage : ContentPage
{
    public event Action<string, string, string, List<string>> FilterApplied;

    public FilterPage()
    {
        InitializeComponent();
    }

    private async void OnSearchButtonClicked(object sender, EventArgs e)
    {
        
        string coachName = EntryCoachName.Text?.ToLower();
        string timeFrom = EntryTimeFrom.Text;
        string timeTo = EntryTimeTo.Text;

        var selectedDays = new List<string>();
        if (CheckBoxMonday.IsChecked) selectedDays.Add("Monday");
        if (CheckBoxTuesday.IsChecked) selectedDays.Add("Tuesday");
        if (CheckBoxWednesday.IsChecked) selectedDays.Add("Wednesday");
        if (CheckBoxThursday.IsChecked) selectedDays.Add("Thursday");
        if (CheckBoxFriday.IsChecked) selectedDays.Add("Friday");
        if (CheckBoxSaturday.IsChecked) selectedDays.Add("Saturday");
        if (CheckBoxSunday.IsChecked) selectedDays.Add("Sunday");

        
        FilterApplied?.Invoke(coachName, timeFrom, timeTo, selectedDays);

        
        await Navigation.PopModalAsync();
    }
}