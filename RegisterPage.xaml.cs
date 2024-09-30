using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Casa_Inteligente
{
    public partial class RegisterPage : ContentPage
    {
        private string verificationCode;
        private string enteredEmail;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnSendVerificationCodeButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;

            // Validar el correo electr�nico
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                await DisplayAlert("Error", "Por favor, ingresa un correo electr�nico v�lido.", "OK");
                return;
            }

            // Generar y enviar el c�digo de verificaci�n
            verificationCode = GenerateVerificationCode();
            await SendVerificationCodeEmail(email, verificationCode);

            // Hacer visibles los campos para el c�digo de verificaci�n y el bot�n
            VerificationCodeEntry.IsVisible = true;
            VerifyCodeButton.IsVisible = true;
        }

        private async void OnVerifyCodeButtonClicked(object sender, EventArgs e)
        {
            string enteredCode = VerificationCodeEntry.Text;

            if (enteredCode == verificationCode)
            {
                await DisplayAlert("�xito", "C�digo de verificaci�n correcto. Ahora puede registrarse.", "OK");
                RegisterButton.IsVisible = true;
                VerifyCodeButton.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Error", "El c�digo de verificaci�n es incorrecto.", "OK");
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
                return;
            }

            if (!IsValidPassword(password))
            {
                await DisplayAlert("Error", "La contrase�a no cumple con los requisitos de seguridad.\r\nM�nimo 7 caracteres\r\n� Debe utilizar m�nimo una may�scula\r\n� Debe utilizar m�nimo un s�mbolo especial\r\n� Debe utilizar m�nimo un numero\r\n� Debe utilizar m�nimo una min�scula", "OK");
                return;
            }

            SaveUser(email, username, password);
            await DisplayAlert("�xito", "Usuario registrado con �xito.", "OK");

            await Navigation.PopAsync();
        }

        private void SaveUser(string email, string username, string password)
        {
            // Obtener la ruta del archivo de usuarios
            string userFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.txt");

            // Escribir la informaci�n del usuario en el archivo
            using (StreamWriter writer = new StreamWriter(userFilePath, true))
            {
                writer.WriteLine($"{email},{username},{password}");
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 7)
                return false;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigits = false;
            bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                if (char.IsLower(c)) hasLowerCase = true;
                if (char.IsDigit(c)) hasDigits = true;
                if (char.IsSymbol(c) || char.IsPunctuation(c)) hasSpecialChar = true;
            }

            return hasUpperCase && hasLowerCase && hasDigits && hasSpecialChar;
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();
        }

        private async Task SendVerificationCodeEmail(string email, string verificationCode)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("moreravalverde@gmail.com", "deiler");
            var to = new EmailAddress(email);
            var subject = "C�digo de Verificaci�n";
            var plainTextContent = $"Tu c�digo de verificaci�n es: {verificationCode}";
            var htmlContent = $"<strong>Tu c�digo de verificaci�n es: {verificationCode}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Correo enviado a: {email}, Estado: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

        private async void OnLoginPageTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}




