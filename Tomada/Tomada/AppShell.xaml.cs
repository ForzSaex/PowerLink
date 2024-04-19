namespace Tomada
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Comandos/Menu", typeof(Comandos));
        }
    }
}
