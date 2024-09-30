using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace Casa_Inteligente
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (IsValidUser(username, password))
            {
                DisplayAlert("Login Exitoso", "Bienvenido " + username, "OK");
                // Navegar a otra página o realizar alguna acción
            }
            else
            {
                DisplayAlert("Fallo en Login", "Usuario o contraseña incorrectos", "OK");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // Obtener la ruta del archivo de usuarios
            string userFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.txt");

            if (File.Exists(userFilePath))
            {
                var lines = File.ReadAllLines(userFilePath);
                foreach (var line in lines)
                {
                    var credentials = line.Split(',');
                    if (credentials[1] == username && credentials[2] == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnCreateAccountButtonClicked(object sender, EventArgs e)
        {
            // Navega a la página de registro
            Shell.Current.GoToAsync("RegisterPage");
        }

        private async void OnRecoverPasswordButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página de restablecimiento de contraseña
            await Shell.Current.GoToAsync("ResetPasswordPage");
        }

    }
}


