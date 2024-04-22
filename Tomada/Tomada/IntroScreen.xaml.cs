using System.Diagnostics;

namespace Tomada
{
    public partial class IntroScreen : ContentPage
    {
        public IntroScreen()
        {
            InitializeComponent();
            animation();
            LabelWelcome.FadeTo(0);
            Button.FadeTo(0);
            LabelTerms.FadeTo(0);
            CheckBox.FadeTo(0);
        }
        public async void animation()
        {
            await Task.Delay(2000);
            LabelWelcome.FadeTo(1);
            LabelWelcome.TranslateTo(0, -50, 400, Easing.CubicOut);
            Button.FadeTo(1);
            Button.TranslateTo(0, 200, 400, Easing.CubicOut);
            LabelTerms.FadeTo(1);
            LabelTerms.TranslateTo(0, 170, 400, Easing.CubicOut);
            CheckBox.FadeTo(1);
            CheckBox.TranslateTo(0, 230, 400, Easing.CubicOut);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Loading.IsRunning = true;
            await Task.Delay(2000);
            Loading.IsRunning = false;
            await IntroScreenFade.FadeTo(0);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Button.IsEnabled = !Button.IsEnabled;
        }

        async void NavigateToPage(Page nextPage)
        {
            // Navegue para a nova página
            await Navigation.PushAsync(nextPage);

            // Defina a animação de fade-in na nova página
            await nextPage.FadeTo(1, 250);
        }
    }
}

