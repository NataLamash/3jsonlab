
    namespace _3jsonlab
    {
        public partial class EditPage : ContentPage
        {
            private Section _sectionToEdit;

            public event Action<Section> SectionEdited;

            public EditPage(Section section)
            {
                InitializeComponent();

                _sectionToEdit = section;

                // Ініціалізація полів введення значеннями секції
                EntryId.Text = _sectionToEdit.Id;
                EntryName.Text = _sectionToEdit.Name;
                EntryCoach.Text = _sectionToEdit.Coach;
                EntryLocation.Text = _sectionToEdit.Location;

                if (_sectionToEdit.Schedule.Count > 0)
                {
                    EntryDay.Text = _sectionToEdit.Schedule[0].Day;
                    EntryTime.Text = _sectionToEdit.Schedule[0].Time;
                }
            }

            private async void OnEditButtonClicked(object sender, EventArgs e)
            {
                // Оновлення даних секції
                _sectionToEdit.Name = EntryName.Text;
                _sectionToEdit.Coach = EntryCoach.Text;
                _sectionToEdit.Location = EntryLocation.Text;

                if (_sectionToEdit.Schedule.Count > 0)
                {
                    _sectionToEdit.Schedule[0].Day = EntryDay.Text;
                    _sectionToEdit.Schedule[0].Time = EntryTime.Text;
                }
                else
                {
                    _sectionToEdit.Schedule.Add(new Schedule
                    {
                        Day = EntryDay.Text,
                        Time = EntryTime.Text
                    });
                }

                // Виклик події для інформування про редагування
                SectionEdited?.Invoke(_sectionToEdit);

                // Закриття вікна
                await Navigation.PopModalAsync();
            }
        }
    }

