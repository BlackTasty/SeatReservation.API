using SeatReservation.Api.Database;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using SeatReservation.Api.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Repositories.Implementation
{
    public class SeatTypeRepository : ISeatTypeRepository
    {
        private readonly DatabaseContext databaseContext;

        public SeatTypeRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Result AddSeatType(SeatType seatType)
        {
            try
            {
                databaseContext.SeatTypes.Add(seatType);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public SeatType GetSeatTypeById(int seatTypeId)
        {
            return databaseContext.SeatTypes.FirstOrDefault(x => x.Id == seatTypeId);
        }

        public ICollection<SeatType> GetSeatTypes()
        {
            return databaseContext.SeatTypes.ToList();
        }

        public Result RemoveSeatType(int seatTypeId)
        {
            try
            {
                SeatType seatType = databaseContext.SeatTypes.FirstOrDefault(x => x.Id == seatTypeId);

                if (seatType == null)
                {
                    return new Result(false);
                }

                databaseContext.SeatTypes.Remove(seatType);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result UpdateSeatType(SeatType seatType)
        {
            try
            {
                databaseContext.SeatTypes.Update(seatType);
                databaseContext.SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
