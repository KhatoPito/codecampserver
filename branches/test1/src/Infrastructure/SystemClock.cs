using System;
using July09v31.Core.Services.Bases;


namespace July09v31.Infrastructure
{
    public class SystemClock : ISystemClock
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}