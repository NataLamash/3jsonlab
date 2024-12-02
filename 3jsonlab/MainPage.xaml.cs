using System.Collections.ObjectModel;
using System.Text.Json;

namespace _3jsonlab
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Section> Sections { get; set; } = new ObservableCollection<Section>();
        private Section _selectedSection;
        private ObservableCollection<Section> _allSections;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            _allSections = new ObservableCollection<Section>(Sections);
        }
        private async void OnFilterClicked(object sender, EventArgs e)
        {
            var filterPage = new FilterPage();
            filterPage.FilterApplied += OnFilterApplied;

            await Navigation.PushModalAsync(filterPage);
        }

        
        private void OnFilterApplied(string coachName, string timeFrom, string timeTo, List<string> selectedDays)
        {
            
            var filteredSections = _allSections.ToList();

            
            if (!string.IsNullOrWhiteSpace(coachName))
            {
                filteredSections = filteredSections
                    .Where(s => s.Coach.ToLower().Contains(coachName.ToLower()))
                    .ToList();
            }

            
            TimeSpan fromTime, toTime;
            bool isFromTimeValid = TimeSpan.TryParse(timeFrom, out fromTime);
            bool isToTimeValid = TimeSpan.TryParse(timeTo, out toTime);

            if (isFromTimeValid && isToTimeValid)
            {
                filteredSections = filteredSections
                    .Where(s => s.Schedule.Any(sc =>
                    {
                        
                        if (TimeSpan.TryParse(sc.Time, out var sectionTime))
                        {
                            return sectionTime >= fromTime && sectionTime <= toTime;
                        }
                        return false;
                    }))
                    .ToList();
            }

            
            if (selectedDays.Any())
            {
                filteredSections = filteredSections
                    .Where(s => s.Schedule.Any(sc => selectedDays.Contains(sc.Day)))
                    .ToList();
            }

            
            Sections.Clear();
            foreach (var section in filteredSections)
            {
                Sections.Add(section);
            }
        }


        private async void OnOpenFileClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a JSON file"
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    var json = await reader.ReadToEndAsync();

                    var rootObject = JsonSerializer.Deserialize<RootObject>(json);
                    if (rootObject?.SportsFaculty?.Sections != null)
                    {
                        Sections.Clear();
                        foreach (var section in rootObject.SportsFaculty.Sections)
                        {
                            Sections.Add(section);
                        }
                        _allSections = new ObservableCollection<Section>(Sections);
                    }
                }
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnSaveFileClicked(object sender, EventArgs e)
        {
            try
            {
                SportsFaculty sportsFaculty = new SportsFaculty { Sections = new List<Section>(Sections) };

                string jsonContent = JsonSerializer.Serialize(sportsFaculty, new JsonSerializerOptions { WriteIndented = true });

                // Збереження файлу до стандартного місця (або можна змінити на вибір користувача)
                string outputPath = Path.Combine("C:\\Users\\elpee\\source\\repos\\3jsonlab\\3jsonlab\\Resources\\Raw\\output.json");
                File.WriteAllText(outputPath, jsonContent);

                await DisplayAlert("Success", $"File saved to {outputPath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnAboutClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutPage()); // Відкриття AboutPage як модального вікна
        }

        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButton = (RadioButton)sender;
                _selectedSection = (Section)radioButton.BindingContext; // Зберігаємо вибрану секцію
            }
        }

        
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_selectedSection == null)
            {
                await DisplayAlert("Error", "Please select a section first.", "OK");
                return;
            }

            bool isConfirmed = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {_selectedSection.Name}?", "Yes", "No");
            if (isConfirmed)
            {
                Sections.Remove(_selectedSection); 
                _selectedSection = null; 
            }
        }

        private async void OnMoreClicked(object sender, EventArgs e)
        {
            if (_selectedSection == null)
            {
                await DisplayAlert("Error", "Please select a section first.", "OK");
                return;
            }

            await Navigation.PushModalAsync(new MorePage(_selectedSection.Schedule));
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var addPage = new AddPage();
            addPage.SectionAdded += OnSectionAdded; 

            await Navigation.PushModalAsync(addPage);
        }

        
        private void OnSectionAdded(Section newSection)
        {
            Sections.Add(newSection);
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (_selectedSection == null)
            {
                await DisplayAlert("Error", "Please select a section first.", "OK");
                return;
            }

            var editPage = new EditPage(_selectedSection);
            editPage.SectionEdited += OnSectionEdited; 

            await Navigation.PushModalAsync(editPage);
        }
        private void OnSectionEdited(Section updatedSection){ }

    }

}
