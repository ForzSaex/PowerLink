using static Tomada.MainPage;
using System.Diagnostics;
using CommunityToolkit.Maui.Extensions;

namespace Tomada
{
    
    public partial class Comandos : ContentPage
    {
        int opcao = 1;
        ViewModelsSamples.Lines.Basic.ViewModel viewModel = new ViewModelsSamples.Lines.Basic.ViewModel();
        public Comandos()
        {
            InitializeComponent();
            try
            {
                OpcaoA.Text = Settings.DispositivoA;
                OpcaoB.Text = Settings.DispositivoB;
                OpcaoC.Text = Settings.DispositivoC;
                OpcaoD.Text = Settings.DispositivoD;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            DispName.Text = Settings.DispositivoA;
            if (Settings.DispAStatus == true)
            {
                ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if (Settings.DispAStatus == false)
            {
                ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
        }

        protected override async void OnAppearing()
        {
            await Disp1.FadeTo(1);
            base.OnAppearing();
            DadosCompartilhados.commandStatus = true;

        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await Disp1.FadeTo(0);
            DadosCompartilhados.commandStatus = false;
        }

        private void SwitchEnviaComando1_Clicked(object sender, EventArgs e)
        {

            switch (opcao)
            {
                case 1:
                    if (Settings.DispAStatus == true)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
                    }
                    if (Settings.DispAStatus == false)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
                        
                    }
                    Settings.DispAStatus = !Settings.DispAStatus;
                    break;
                case 2:
                    if (Settings.DispBStatus == true)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
                    }
                    if (Settings.DispBStatus == false)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
                    }
                    Settings.DispBStatus = !Settings.DispBStatus;
                    break;
                case 3:
                    if (Settings.DispCStatus == true)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
                    }
                    if (Settings.DispCStatus == false)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
                    }
                    Settings.DispCStatus = !Settings.DispCStatus;
                    break;
                case 4:
                    if (Settings.DispDStatus == true)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
                    }
                    if (Settings.DispDStatus == false)
                    {
                        ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
                    }
                    Settings.DispDStatus = !Settings.DispDStatus;
                    break;
            }
            Debug.WriteLine(Settings.DispAStatus + " " + Settings.DispBStatus + " " + Settings.DispCStatus + " " + Settings.DispDStatus + "" + opcao);
        }


        private void updateName()
        {
            MainThread.BeginInvokeOnMainThread(() => {
                OpcaoA.Text = Settings.DispositivoA;
                OpcaoB.Text = Settings.DispositivoB;
                OpcaoC.Text = Settings.DispositivoC;
                OpcaoD.Text = Settings.DispositivoD;
            });
            switch (opcao)
            {
                case 1:
                    DispName.Text = Settings.DispositivoA;
                    break;
                case 2:
                    DispName.Text = Settings.DispositivoB;
                    break;
                case 3:
                    DispName.Text = Settings.DispositivoC;
                    break;
                case 4:
                    DispName.Text = Settings.DispositivoD;
                    break;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            switch(opcao)
            {
                case 1:
                    Settings.DispositivoA = await DisplayPromptAsync("Trocar nome", "Digite o novo nome");
                    break;
                case 2:
                    Settings.DispositivoB = await DisplayPromptAsync("Trocar nome", "Digite o novo nome");
                    break;
                case 3:
                    Settings.DispositivoC = await DisplayPromptAsync("Trocar nome", "Digite o novo nome");
                    break;
                case 4:
                    Settings.DispositivoD = await DisplayPromptAsync("Trocar nome", "Digite o novo nome");
                    break;
            }
            updateName();
        }

        private async void OpcaoA_Clicked(object sender, EventArgs e)
        {
            opcao = 1;
            DispName.Text = Settings.DispositivoA;
            if (Settings.DispAStatus == true)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if (Settings.DispAStatus == false)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
        }

        private async void OpcaoB_Clicked(object sender, EventArgs e)
        {
            opcao = 2;
            DispName.Text = Settings.DispositivoB;
            if (Settings.DispBStatus == true)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if (Settings.DispBStatus == false)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
        }

        private async void OpcaoC_Clicked(object sender, EventArgs e)
        {
            opcao = 3;
            DispName.Text = Settings.DispositivoC;
            if (Settings.DispCStatus == true)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if (Settings.DispCStatus == false)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
        }

        private async void OpcaoD_Clicked(object sender, EventArgs e)
        {
            opcao = 4;
            DispName.Text = Settings.DispositivoD;
            if (Settings.DispDStatus == true)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if (Settings.DispDStatus == false)
            {
                await ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
        }
    }





}
