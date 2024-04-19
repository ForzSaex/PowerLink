using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using LiveChartsCore.Defaults;
using System.Runtime.Serialization;
using Tomada;
using static Tomada.MainPage;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Text.Json;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.VisualElements;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Measure;
using System.Drawing;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Platform;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.Animations;
using Windows.ApplicationModel.VoiceCommands;
using System.Collections.Specialized;
using Windows.Networking.NetworkOperators;
using static SkiaSharp.HarfBuzz.SKShaper;


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

        private async void SwitchEnviaComando1_Clicked(object sender, EventArgs e)
        {
            if(Settings.DispAStatus == true)
            {
                ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFADFF2F"), 1, 700, Easing.CubicInOut);
            }
            if(Settings.DispAStatus == false) 
            {
                ColorAnimationExtensions.BackgroundColorTo(SwitchEnviaComando1, Microsoft.Maui.Graphics.Color.FromArgb("#FFFF4500"), 1, 700, Easing.CubicInOut);
            }
            Settings.DispAStatus = !Settings.DispAStatus;            
            Debug.WriteLine(Settings.DispAStatus + " " + Settings.DispBStatus + " " + Settings.DispCStatus + " " + Settings.DispDStatus + "" + opcao);
        }

        private void SwitchEnviaComando2_Clicked(object sender, EventArgs e)
        {
            Settings.DispBStatus = !Settings.DispBStatus;
            Debug.WriteLine(Settings.DispAStatus + " " + Settings.DispBStatus + " " + Settings.DispCStatus + " " + Settings.DispDStatus + "" + opcao);
        }

        private void SwitchEnviaComando3_Clicked(object sender, EventArgs e)
        {
            Settings.DispCStatus = !Settings.DispCStatus;
            Debug.WriteLine(Settings.DispAStatus + " " + Settings.DispBStatus + " " + Settings.DispCStatus + " " + Settings.DispDStatus + "" + opcao);
        }

        private void SwitchEnviaComando4_Clicked(object sender, EventArgs e)
        {
            Settings.DispDStatus = !Settings.DispDStatus;
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

        private void OpcaoA_Clicked(object sender, EventArgs e)
        {
            opcao = 1;
            DispName.Text = Settings.DispositivoA;
        }

        private void OpcaoB_Clicked(object sender, EventArgs e)
        {
            opcao = 2;
            DispName.Text = Settings.DispositivoB;
        }

        private void OpcaoC_Clicked(object sender, EventArgs e)
        {
            opcao = 3;
            DispName.Text = Settings.DispositivoC;
        }

        private void OpcaoD_Clicked(object sender, EventArgs e)
        {
            opcao = 4;
            DispName.Text = Settings.DispositivoD;
        }
    }





}
