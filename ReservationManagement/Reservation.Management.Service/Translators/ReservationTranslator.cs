
using Hotel.Booking.Common.Constant;

namespace Reservation.Management.Service.Translators
{
    public static class ReservationTranslator
    {
        public static DataAccess.Entities.Reservation ToNewEntity(this Model.CreateUpdateReservation reservation)
        {
            return new DataAccess.Entities.Reservation
            {            
                 EndDate = reservation.EndDate,
                 Observations = reservation.Observations,   
                 PaymentMethodInfo = reservation.PaymentMethodInfo,
                 PaymentMethodType = reservation.PaymentMethodType,
                 StartDate = reservation.StartDate,
                 Status = (int)ReservationStatus.Booked,
                 UserId = reservation.UserId,
                 Id = reservation.ReservationId
            };
        }

        public static DataAccess.Entities.Reservation ToEntity(this Model.CreateUpdateReservation reservation)
        {
            return new DataAccess.Entities.Reservation
            {
                EndDate = reservation.EndDate,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                StartDate = reservation.StartDate,
                Status = reservation.Status,
                UserId = reservation.UserId,
                Id = reservation.ReservationId
            };
        }

        public static Model.Reservation ToBaseModel(this DataAccess.Entities.Reservation reservation)
        {
            return new Model.Reservation
            {
                Id = reservation.Id,
                EndDate = reservation.EndDate,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                StartDate = reservation.StartDate,
                Status = reservation.Status,
                UserId = reservation.UserId
            };
        }

        public static Model.ReservationDetails ToDetailModel(this DataAccess.Entities.Reservation reservation)
        {
            return new Model.ReservationDetails
            {
                Id = reservation.Id,
                EndDate = reservation.EndDate,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                StartDate = reservation.StartDate,
                Status = reservation.Status,
                UserId = reservation.UserId,
                RoomReservations = reservation.RoomReservations.Select(r => r.ToModel()).ToList(),
                User = reservation.User?.ToModel(),
                UserProfile = reservation.User?.UserProfiles?.FirstOrDefault()?.ToModel()                
            };
        }

        public static DataAccess.Entities.Reservation UpdateFromModel(this DataAccess.Entities.Reservation reservation, Model.CreateUpdateReservation model)
        {
            if(reservation.Status != model.Status)
            {
                reservation.Status = model.Status;
            }
            if (reservation.EndDate != model.EndDate)
            {
                reservation.EndDate = model.EndDate;
            }
            if (reservation.StartDate != model.StartDate)
            {
                reservation.StartDate = model.StartDate;
            }
            if (reservation.Observations != model.Observations)
            {
                reservation.Observations = model.Observations;
            }
            if (reservation.PaymentMethodInfo != model.PaymentMethodInfo)
            {
                reservation.PaymentMethodInfo = model.PaymentMethodInfo;
            }
            if (reservation.PaymentMethodType != model.PaymentMethodType)
            {
                reservation.PaymentMethodType = model.PaymentMethodType;
            }
            return reservation;
        }
    }
}
