using LiveChartsCore.Defaults;
using System.Diagnostics;
using ViewModelsSamples.Lines.Basic;
using static Tomada.MainPage;

namespace Tomada
{
    public partial class Relatório : ContentPage
    {
        public object Sync { get; } = new object();
        ViewModel viewModel = new ViewModel();
        DateTime setDayTime;
        DateTime setWeekTime;
        DateTime setMonthTime;
        public Relatório()
        {
            base.OnAppearing();
            InitializeComponent();
            LoadData();

        }

        public void LoadData()
        {
            try
            {
                using (var md = File.Open("C:\\Users\\creep\\AppData\\Local\\PowerLink\\Date.dat", FileMode.Open))
                {
                    using (BinaryReader bw = new BinaryReader(md, System.Text.Encoding.UTF8, false))
                    {
                        setDayTime = DateTime.Parse(bw.ReadString());
                        setWeekTime = DateTime.Parse(bw.ReadString());
                        setMonthTime = DateTime.Parse(bw.ReadString());
                    }
                }
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        protected override void OnAppearing()
        {
                base.OnAppearing();
            Debug.WriteLine(viewModel._Semanal.Count);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Media.Text = "Tempo Real";
            Grafico.Series = viewModel.Real;
            Grafico.XAxes = viewModel.XAxes;
            Grafico.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.None;
            Grafico.IsVisible = true;
            Erro.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await viewModel.ReadDayData();
            if(viewModel.Diario.Count == 1)
            {
                Erro.IsVisible=true;
                Grafico.IsVisible = false;
                Erro.Text = "Média Diária não disponivel";
            }
            else 
            {
                Grafico.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.ZoomX;
                Media.Text = "Media Diaria";
                Grafico.Series = viewModel.Diario;
                Grafico.IsVisible = true;

            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await viewModel.ReadWeekData();
            if (viewModel._Semanal.Count == 0)
            {
                Erro.IsVisible = true;
                Grafico.IsVisible = false;
                Erro.Text = "Média Semanal não disponivel";
            }
            else
            {
                Grafico.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.ZoomX;
                Media.Text = "Media Semanal";
                Grafico.Series = viewModel.Semanal;
                Grafico.IsVisible = true;

            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await viewModel.ReadMonthData();
            if (viewModel.Diario.Count == 1)
            {
                Erro.IsVisible = true;
                Grafico.IsVisible = false;
                Erro.Text = "Média Mensal não disponivel";
            }
            else
            {
                Grafico.XAxes = viewModel.XMAxes;
                Grafico.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.ZoomX;
                Media.Text = "Media Mensal";
                Grafico.Series = viewModel.Mensal;
                Grafico.IsVisible = true;
            }
        }

        }




}
