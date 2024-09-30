namespace Casa_Inteligente
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Registra las rutas de navegación
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("ResetPasswordPage", typeof(ResetPasswordPage)); // Agrega esta línea
        }
    }
}
