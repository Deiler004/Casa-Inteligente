using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Casa_Inteligente
{
    public partial class ResetPasswordPage : ContentPage
    {
        private string verificationCode; // Variable para almacenar el c�digo de verificaci�n

        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        // Evento al hacer clic en "Enviar C�digo de Verificaci�n"
        private async void OnSendVerificationCodeButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Por favor ingresa tu correo electr�nico.", "OK");
                return;
            }

            // Aqu� deber�as enviar el c�digo de verificaci�n al correo electr�nico del usuario.
            // Simularemos el env�o con un c�digo aleatorio.
            verificationCode = GenerateVerificationCode();
            await DisplayAlert("C�digo Enviado", $"Se ha enviado un c�digo de verificaci�n al correo: {email}", "OK");

            // Mostrar las entradas y botones para verificar el c�digo y restablecer la contrase�a
            VerificationCodeEntry.IsVisible = true;
            NewPasswordEntry.IsVisible = true;
            ConfirmNewPasswordEntry.IsVisible = true;
            (sender as Button).IsVisible = false; // Ocultar el bot�n de enviar c�digo
        }

        // Evento al hacer clic en "Verificar C�digo"
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
                await DisplayAlert("Error", "El c�digo de verificaci�n es incorrecto.", "OK");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
                return;
            }

            // Aqu� deber�as realizar la l�gica para restablecer la contrase�a en la base de datos.
            await DisplayAlert("�xito", "Tu contrase�a ha sido restablecida exitosamente.", "OK");

            // Opcional: Navegar a otra p�gina despu�s del restablecimiento exitoso.
            // await Navigation.PushAsync(new LoginPage());
        }

        // M�todo para generar un c�digo de verificaci�n aleatorio
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString(); // C�digo de 4 d�gitos
        }
    }
}

