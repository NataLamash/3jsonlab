﻿namespace _3jsonlab;

    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(); // Закриваємо модальне вікно
        }
    }
