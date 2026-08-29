using GeoFleet.Domain.Common.Exceptions;

namespace GeoFleet.Domain.Common.ValueObjects;

public sealed record GeoCoordinate
{
    private GeoCoordinate(
        double latitude,
        double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }

    public double Longitude { get; }

    public static GeoCoordinate Create(
        double latitude,
        double longitude)
    {
        if (latitude is < -90 or > 90)
        {
            throw new DomainException(
                $"Latitude must be between -90 and 90 degrees. Provided value: {latitude}");
        }

        if (longitude is < -180 or > 180)
        {
            throw new DomainException(
                $"Longitude must be between -180 and 180 degrees. Provided value: {longitude}");
        }

        return new GeoCoordinate(
            latitude,
            longitude);
    }
}