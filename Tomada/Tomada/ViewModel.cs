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


namespace ViewModelsSamples.Lines.Basic;

public class ViewModel
{
    DateTime setDayTime;
    DateTime setWeekTime;
    DateTime setMonthTime;
    private System.Timers.Timer _timer;
    private int _intervalInSeconds = 1;
    private readonly Random _random = new();
    public readonly List<DateTimePoint> _Real = new();
    public readonly List<DateTimePoint> _Diario = new();
    public readonly List<DateTimePoint> _Semanal = new();
    private static readonly List<ObservableValue> _GaugeVal = new();
    private readonly List<Double> _Mensal = new();
    public readonly DateTimeAxis _XcustomAxis;
    private readonly DateTimeAxis _XMcustomAxis;
    public Int64 valorAt;
    private static readonly SKColor s_dark3 = new(60, 60, 60);
    private static readonly SKColor s_gray = new(195, 195, 195);
    private HttpClient client;
    string? url;
    public bool IsReading { get; set; } = true;
    public bool status1;
    public double widthScreen2 = DeviceDisplay.Current.MainDisplayInfo.Width;



    HttpResponseMessage message = new HttpResponseMessage();

    public ViewModel()
    {
        client = new HttpClient();
        _timer = new System.Timers.Timer(TimeSpan.FromSeconds(_intervalInSeconds).TotalMilliseconds);
        _timer.Elapsed += async (sender, e) => await OnTimerElapsed();
        _timer.AutoReset = true;
        _timer.Start();

        if(File.Exists("C:\\Users\\creep\\AppData\\Local\\PowerLink\\Date.dat"))
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



        ObservableValue1 = new ObservableValue { Value = 0 };
        ObservableValue2 = new ObservableValue { Value = 0 };
        ObservableValue3 = new ObservableValue { Value = 0 };
        ObservableValue4 = new ObservableValue { Value = 0 };

        GaugeSeries1 = GaugeGenerator.BuildSolidGauge(
            new GaugeItem(ObservableValue1, series =>
            {
                series.MaxRadialColumnWidth = 10;
                series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                series.Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new[] { new SKColor(114, 212, 210), new SKColor(245, 185, 66) });
                series.DataLabelsSize = 50;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255));
                series.IsHoverable = false;
            }));

        GaugeSeries2 = GaugeGenerator.BuildSolidGauge(
            new GaugeItem(ObservableValue2, series =>
            {
                series.Name = "Potencia";
                series.MaxRadialColumnWidth = 10;
                series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                series.Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new[] { new SKColor(114, 212, 210), new SKColor(245, 185, 66) });
                series.DataLabelsSize = 50;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255));
            }));

        GaugeSeries3 = GaugeGenerator.BuildSolidGauge(
            new GaugeItem(ObservableValue3, series =>
            {
                series.Name = "Potencia";
                series.MaxRadialColumnWidth = 10;
                series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                series.Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new[] { new SKColor(114, 212, 210), new SKColor(245, 185, 66) });
                series.DataLabelsSize = 50;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255));
            }));

        GaugeSeries4 = GaugeGenerator.BuildSolidGauge(
            new GaugeItem(ObservableValue4, series =>
            {
                series.Name = "Potencia";
                series.MaxRadialColumnWidth = 10;
                series.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                series.Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new[] { new SKColor(114, 212, 210), new SKColor(245, 185, 66) });
                series.DataLabelsSize = 50;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255));
            }));

        Real = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _Real,
                Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new [] { new SKColor(0, 255, 250), new SKColor(114, 212, 210, 0) },
                new SKPoint(1.0f, 0),
                new SKPoint(1.0f, 1)),
        GeometryFill = null,
                GeometryStroke = null,
                Stroke = new SolidColorPaint(new SKColor(0, 0, 0), 2),
                LineSmoothness = 0,
                IsHoverable = false,
                
            },

        };

        Diario = new ObservableCollection<ISeries>
        {
            new ColumnSeries<DateTimePoint>
            {
                Values = _Diario,
                Fill = new SolidColorPaint(new SKColor(188, 228, 247)),
                Stroke = new SolidColorPaint(new SKColor(64, 194, 255), 5),
            }
        };

        Semanal = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _Semanal,
                Fill = new SolidColorPaint(new SKColor(188, 228, 247)),
                GeometryFill = null,
                GeometryStroke = null,
                Stroke = new SolidColorPaint(new SKColor(64, 194, 255), 5),
                LineSmoothness = 0
            }
        };

        Mensal = new ObservableCollection<ISeries>
        {
            new ColumnSeries<Double>
            {
                Values = _Mensal,
                Fill = new SolidColorPaint(new SKColor(188, 228, 247)),
                Stroke = new SolidColorPaint(new SKColor(64, 194, 255), 5),

            }
        };

        _XcustomAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            CustomSeparators = GetSeparators(),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100)),
            IsVisible = false,
        };

        XAxes = new Axis[] { _XcustomAxis

        };
    }



    public async Task ReadDayData()
        {
        if(System.DateTime.Now == setDayTime)
        {
            try
            {
                url = "Https://youtube.com";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string value = await httpResponseMessage.Content.ReadAsStringAsync();
                    lock (Sync)
                    {

                        _Diario.Add(new DateTimePoint(DateTime.Now, valorAt));
                        if (_Diario.Count > 11) _Diario.RemoveAt(0);
                        _XcustomAxis.CustomSeparators = GetSeparators();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        else
        {
            
        }

            
        
    }

    public async Task ReadWeekData()
    {
        if (System.DateTime.Now == setWeekTime)
        {
            try
            {
                url = "Https://youtube.com";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode && System.DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                {
                    string value = await httpResponseMessage.Content.ReadAsStringAsync();
                    lock (Sync)
                    {
                        _Semanal.Add(new DateTimePoint(DateTime.Now, valorAt));
                        if (_Semanal.Count > 11) _Semanal.RemoveAt(0);
                    }


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        
    }

    public async Task ReadMonthData()
    {
        if(System.DateTime.Now == setMonthTime)
        {
            try
            {
                url = "Https://youtube.com";
                HttpResponseMessage RespostaServidor = await client.GetAsync(url);

                if (RespostaServidor.IsSuccessStatusCode)
                {
                    string value = await RespostaServidor.Content.ReadAsStringAsync();
                    float valueFinal = float.Parse(value);
                    lock (Sync)
                        _Mensal.Add(valueFinal);
                    if (_Mensal.Count > 12) _Mensal.Clear();
                    _XcustomAxis.CustomSeparators = GetSeparators();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


    }

    public async Task loadServerData()
    {
        try
        {

            var conteudo = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("comando", "desligar_tomada_1") // Exemplo de comando para ligar o LED 1
                });

            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string value = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }

    }

    public async Task loadData()
    {
        try
        {
            url = "Https://youtube.com";
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string value = await httpResponseMessage.Content.ReadAsStringAsync();


            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }

    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public DrawMarginFrame Frame { get; set; } =
    new()
    {
        Fill = null,
        Stroke = new SolidColorPaint
        {
            Color = s_gray,
            StrokeThickness = 2
        }
    };

    public ObservableCollection<ISeries> Real { get; set; }
    public ObservableCollection<ISeries> Diario { get; set; }
    public ObservableCollection<ISeries> Semanal { get; set; }
    public ObservableCollection<ISeries> Mensal { get; set; }
    public ObservableValue ObservableValue1 { get; set; }
    public ObservableValue ObservableValue2 { get; set; }
    public ObservableValue ObservableValue3 { get; set; }
    public ObservableValue ObservableValue4 { get; set; }
    public IEnumerable<ISeries> GaugeSeries1 { get; set; }
    public IEnumerable<ISeries> GaugeSeries2 { get; set; }
    public IEnumerable<ISeries> GaugeSeries3 { get; set; }
    public IEnumerable<ISeries> GaugeSeries4 { get; set; }
    public DayOfWeek DayOfWeek { get; }
    public Axis[] XAxes { get; set; }
    public Axis[] XMAxes { get; set; } =
    {
        new Axis
        {
            CustomSeparators = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
            MinLimit = 1, // forces the axis to start at 0
            MaxLimit = 12, // forces the axis to end at 100
            SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100)),
            Labels = new string[] { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" }
        }
    };

    public double widthScreen = DeviceDisplay.Current.MainDisplayInfo.Width;

    public object Sync { get; } = new object();

    public bool IsDayReading { get; set; } = true;
    public bool IsWeekReading { get; set; } = true;
    public bool IsMonthReading { get; set; } = true;

    public async Task ReadData()
    {
            lock (Sync)
            {

                _Real.Add(new DateTimePoint(DateTime.Now, valorAt));
                if (_Real.Count > 11) _Real.RemoveAt(0);
                _XcustomAxis.CustomSeparators = GetSeparators();
            }
    }
    public async Task CommandData()
    {
        lock (Sync)
        {
            ObservableValue1.Value = valorAt;
            if (ObservableValue1.Value > 1270) ;
        }
    }

    public double[] GetSeparators()
    {
        var now = DateTime.Now;

        return new double[]
        {

            now.AddSeconds(-30).Ticks,
            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
        };
    }

    private static string Formatter(DateTime date)
    {
        var secsAgo = (DateTime.Now - date).TotalSeconds;

        return secsAgo < 1
            ? "Agora"
            : $"{secsAgo:N0}s";
    }

    public async Task OnTimerElapsed()
    {

        valorAt = DadosCompartilhados.ValorCompartilhado;
        await loadData();
        if(DadosCompartilhados.status == true)
        {
            await ReadData();
        }
        if(DadosCompartilhados.commandStatus == true)
        {
            await CommandData();
        }
        
        
    }

    

}
