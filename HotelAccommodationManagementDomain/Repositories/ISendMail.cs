using HotelAccommodationManagementDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface ISendMail
    {
        Task SendReservationEmail(int userId, Reservations reservation);
    }
}
