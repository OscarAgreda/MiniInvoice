using System;
namespace DDDCleanArchStarter.Domain.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTimeOffset TestDate { get; }
    }
}