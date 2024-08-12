using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace HotelAccommodationManagementApplication.Services
{
    public class MailServices : ISendMail
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public MailServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task SendReservationEmail(int userId, Reservations reservation)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            string subject = "Confirmación de Reserva";
            string message = $"Hola {user.UserName},\n\n" +
                             $"Tu reserva para la habitación {reservation.RoomId} " +
                             $"desde {reservation.CheckInDate} hasta {reservation.CheckOutDate} " +
                             $"ha sido confirmada.\n\nGracias por usar nuestros servicios.";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Email:Username"]));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(
                    _configuration["Email:Host"],
                    int.Parse(_configuration["Email:Port"]),
                    SecureSocketOptions.StartTls
                );
                await smtp.AuthenticateAsync(
                    _configuration["Email:Username"],
                    _configuration["Email:Password"]
                );
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }

            Console.WriteLine($"Correo enviado a: {user.Email}\nAsunto: {subject}\nMensaje: {message}");
        }
    }
}
