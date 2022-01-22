using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.IntegrationEvents;
using BuildingBlocks.Email;
using BuildingBlocks.RabbitMq;
using Domain.Repositories;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using TheShow.Notifications.QrCode;

namespace TheShow.Notifications.EventHandlers
{
    internal class ReservationMadeEventHandler : IMessageHandler<ReservationMadeEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IQrGenerator _qrGenerator;
        private readonly UserManager<User> _userManager;
        private readonly IReservationRepository _reservationRepository;
        public ReservationMadeEventHandler(IEmailSender emailSender, IQrGenerator qrGenerator, UserManager<User> userManager, IReservationRepository reservationRepository)
        {
            _emailSender = emailSender;
            _qrGenerator = qrGenerator;
            _userManager = userManager;
            _reservationRepository = reservationRepository;
        }

        public async Task Handle(ReservationMadeEvent message, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(message.ReservationId.ToString() + ";" + message.UserId.ToString());
            var user = await _userManager.FindByIdAsync(message.UserId.ToString());
            var reservation = await _reservationRepository.Find(message.ReservationId);

            await _emailSender.SendAsync(new EmailMessage
            {
                Subject = "Twoja rezerwacja seansu",
                Content = $"Dziekujemy za rezerwacje seansu na film {reservation.MovieShowcase.Movie.Name}",
                ToAddresses = new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        Address = user.Email,
                        Name = user.FirstName
                    }
                }
            });
        }
    }
}
