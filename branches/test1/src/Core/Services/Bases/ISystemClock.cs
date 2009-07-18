using System;


namespace July09v31.Core.Services.Bases
{
    public interface ISystemClock
    {
        DateTime GetCurrentDateTime();
    }
}