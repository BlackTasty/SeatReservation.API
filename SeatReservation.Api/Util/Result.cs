using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Util
{
    public class Result
    {
        private bool success;
        private Exception exception;
        private int id;

        public bool Success => success;

        public Exception Exception => exception;

        public int Id => id;

        public Result(bool success = true)
        {
            this.success = success;
        }

        public Result(Exception exception) : this(false)
        {
            this.exception = exception;
        }

        public Result(int id) : this()
        {
            this.id = id;
        }
    }
}
