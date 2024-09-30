using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Casa_Inteligente
{
    public partial class ResetPasswordPage : ContentPage
    {
        private string verificationCode; // Variable para almacenar el código de verificación

        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        // Evento al hacer clic en "Enviar Código de Verificación"
        private async void OnSendVerificationCodeButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Por favor ingresa tu correo electrónico.", "OK");
                return;
            }

            // Aquí deberías enviar el código de verificación al correo electrónico del usuario.
            // Simularemos el envío con un código aleatorio.
            verificationCode = GenerateVerificationCode();
            await DisplayAlert("Código Enviado", $"Se ha enviado un código de verificación al correo: {email}", "OK");

            // Mostrar las entradas y botones para verificar el código y restablecer la contraseña
            VerificationCodeEntry.IsVisible = true;
            NewPasswordEntry.IsVisible = true;
            ConfirmNewPasswordEntry.IsVisible = true;
            (sender as Button).IsVisible = false; // Ocultar el botón de enviar código
        }

        // Evento al hacer clic en "Verificar Código"
        private async void OnVerifyCodeButtonClicked(object sender, EventArgs e)
        {
            string enteredCode = VerificationCodeEntry.Text;
            string newPassword = NewPasswordEntry.Text;
            string confirmNewPassword = ConfirmNewPasswordEntry.Text;

            if (string.IsNullOrEmpty(enteredCode) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
                await DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
                return;
            }

            if (enteredCode != verificationCode)
            {
                await DisplayAlert("Error", "El código de verificación es incorrecto.", "OK");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                return;
            }

            // Aquí deberías realizar la lógica para restablecer la contraseña en la base de datos.
            await DisplayAlert("Éxito", "Tu contraseña ha sido restablecida exitosamente.", "OK");

            // Opcional: Navegar a otra página después del restablecimiento exitoso.
            // await Navigation.PushAsync(new LoginPage());
        }

        // Método para generar un código de verificación aleatorio
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString(); // Código de 4 dígitos
        }
    }
}

