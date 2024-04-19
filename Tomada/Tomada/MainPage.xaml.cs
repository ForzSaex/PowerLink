using System.Diagnostics;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;

namespace Tomada
{
    public partial class MainPage : ContentPage
    {
        private HttpClient client;
        Random randnum = new Random();
        private System.Timers.Timer _timer;
        private int _intervalInSeconds = 1; // Intervalo em segundos
        public float Valor;
        private readonly List<DateTimePoint> _values = new();
        private DateTimeAxis _customAxis;
        public Int64 valorAt;
        ViewModelsSamples.Lines.Basic.ViewModel viewModel = new ViewModelsSamples.Lines.Basic.ViewModel();




        public MainPage()
        {
            _timer = new System.Timers.Timer(TimeSpan.FromSeconds(_intervalInSeconds).TotalMilliseconds);
            _timer.Elapsed += async (sender, e) => await OnTimerElapsed();
            _timer.AutoReset = true;
            _timer.Start();
            InitializeComponent();
            client = new HttpClient();
            DadosCompartilhados.status = true;
            FirstTimeOpen();
            LoadData();
            ConnectionTest();
            RoomName.Text = Settings.Comodo;
            
        }

        public async Task ConnectionTest()
        {
            try
            {
                var Esp32Url = "https://www.youtube.com/asdcxccaw";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(Esp32Url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {

                }
                else
                {

                    MainThread.BeginInvokeOnMainThread(() => {
                        ConsumoAtual.Text = "Consumo atual:" + 0 + "W";
                    });

                }
            }
            catch (Exception ex)
            {
                
                DadosCompartilhados.valorCompartilhado = 5;
                RoomName.Text = ex.ToString();
            }
        }

        public void FirstTimeOpen()
        {
            if (Settings.IsFirstTimeOpen == true)
            {
                Settings.IsFirstTimeOpen = false;
                CreateAppStockFiles();
            }
            
        }
        public void LoadData()
        {
            try
            {

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            
        }

        public async void CreateAppStockFiles() 
        {
            GetTime();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {

                try
                {
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PowerLink");
                    var pathfileData = Path.Combine(path, "DispData.dat");
                    var pathfileMedia = Path.Combine(path, "MediaData.dat");
                    var pathfileDate = Path.Combine(path, "Date.dat");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else if (!File.Exists(pathfileData))
                    {
                        using (FileStream fs = new FileStream(pathfileData, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false))
                            {

                            }
                        }
                    }
                    else if (!File.Exists(pathfileMedia))
                    {
                        using (FileStream fs = new FileStream(pathfileMedia, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false))
                            {
                                bw.Write(0M); // Diario
                                bw.Write(0M); // Semanal
                                bw.Write(0M); // Mensal
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Preferences.Set("FirstOpen", true);
                }
            }
            if(DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                try
                {
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PowerLink");
                    var pathfileData = Path.Combine(path, "DispData.dat");
                    var pathfileMedia = Path.Combine(path, "MediaData.dat");
                    var pathfileDate = Path.Combine(path, "Date.dat");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!File.Exists(pathfileData))
                    {
                        using (FileStream fs = new FileStream(pathfileData, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false))
                            {
                                bw.Write("Dispositivo1"); //Nome do dispositivo posição 1
                                bw.Write(false); // Status do dispositivo posição 1
                            }
                        }
                    } 
                    if (!File.Exists(pathfileMedia))
                    {
                        using (FileStream fs = new FileStream(pathfileMedia, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.UTF8, false))
                            {
                                bw.Write(0M); // Diario
                                bw.Write(0M); // Semanal
                                bw.Write(0M); // Mensal
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Settings.IsFirstTimeOpen = true;
                }

            }

            
        }

        public void GetTime()
        {
            Settings.GetDay = System.DateTime.Now.AddDays(1).ToString();
            Settings.GetWeek = System.DateTime.Now.AddDays(7).ToString();
            Settings.GetMonth = System.DateTime.Now.AddDays(31).ToString();
        }


        public static class DadosCompartilhados
        {
            public static Int64 valorCompartilhado;
            public static Int64 ValorCompartilhado
            {
                get { return valorCompartilhado; }
                set { valorCompartilhado = value; }
            }

            public static bool status;
            public static bool Status
            {
                get { return status; }
                set { status = value; }
            }

            public static bool commandStatus;
            public static bool CommandStatus
            {
                get { return commandStatus; }
                set { commandStatus = value; }
            }
        }

        public async Task OnTimerElapsed()
        {
            try
            {
                var Esp32Url = "https://www.youtube.com/asdcxccaw";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(Esp32Url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    DadosCompartilhados.valorCompartilhado = long.Parse(await httpResponseMessage.Content.ReadAsStringAsync());

                }
                else
                {
                    await ConnectionTest();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await Chart1.FadeTo(0);
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Chart1.FadeTo(1);
            CreateAppStockFiles();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Relatório)}", true);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            Detalhes.BackgroundColor = Colors.DarkCyan;
        }

        private void Detalhes_Released(object sender, EventArgs e)
        {
            Detalhes.BackgroundColor = Colors.White;

        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            Settings.Comodo = await DisplayPromptAsync("Trocar nome", "Digite o novo nome");
            MainThread.BeginInvokeOnMainThread(() => {
                RoomName.Text = Settings.Comodo;
            });
        }
    }

    public class Settings
    {
        private const string FirstTimeOpen = "FirstTimeOpen";
        private const string Comodo1 = "Comodo";
        private const string Disp1Status = "DispAStatus";
        private const string Disp2Status = "DispBStatus";
        private const string Disp3Status = "DispCStatus";
        private const string Disp4Status = "DispDStatus";
        private const string Dispositivo1 = "Dispositivo1";
        private const string Dispositivo2 = "Dispositivo2";
        private const string Dispositivo3 = "Dispositivo3";
        private const string Dispositivo4 = "Dispositivo4";
        private const string Day = "01/01/2024 00:00:00";
        private const string Week = "01/01/2024 00:00:00";
        private const string Month = "01/01/2024 00:00:00";

        public static bool IsFirstTimeOpen
        {
            get => Preferences.Get(FirstTimeOpen, true);
            set => Preferences.Set(FirstTimeOpen, value);
        }

        public static bool DispAStatus
        {
            get => Preferences.Get(Disp1Status, true);
            set => Preferences.Set(Disp1Status, value);
        }

        public static bool DispBStatus
        {
            get => Preferences.Get(Disp2Status, true);
            set => Preferences.Set(Disp2Status, value);
        }

        public static bool DispCStatus
        {
            get => Preferences.Get(Disp3Status, true);
            set => Preferences.Set(Disp3Status, value);
        }

        public static bool DispDStatus
        {
            get => Preferences.Get(Disp4Status, true);
            set => Preferences.Set(Disp4Status, value);
        }

        public static string Comodo
        {
            get => Preferences.Get(Comodo1, "Comodo");
            set => Preferences.Set(Comodo1, value);
        }

        public static string DispositivoA
        {
            get => Preferences.Get(Dispositivo1, "Dispositivo1");
            set => Preferences.Set(Dispositivo1, value);
        }

        public static string DispositivoB
        {
            get => Preferences.Get(Dispositivo2, "Dispositivo2");
            set => Preferences.Set(Dispositivo2, value);
        }

        public static string DispositivoC
        {
            get => Preferences.Get(Dispositivo3, "Dispositivo3");
            set => Preferences.Set(Dispositivo3, value);
        }

        public static string DispositivoD
        {
            get => Preferences.Get(Dispositivo4, "Dispositivo4");
            set => Preferences.Set(Dispositivo4, value);
        }

        public static string GetDay
        {
            get => Preferences.Get(Day, "01/01/2024 00:00:00");
            set => Preferences.Set(Day, value);
        }

        public static string GetWeek
        {
            get => Preferences.Get(Week, "01/01/2024 00:00:00");
            set => Preferences.Set(Week, value);
        }

        public static string GetMonth
        {
            get => Preferences.Get(Month, "01/01/2024 00:00:00");
            set => Preferences.Set(Month, value);
        }

    }





}