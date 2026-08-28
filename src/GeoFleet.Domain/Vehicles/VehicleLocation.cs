using GeoFleet.Domain.Common.Exceptions;
using NetTopologySuite.Geometries;

namespace GeoFleet.Domain.Vehicles;

public sealed class VehicleLocation
{
    private VehicleLocation()
    {            
    }

    public VehicleLocation(
        Guid id,
        Guid vehicleId,
        double latitude,
        double longitude,
        DateTimeOffset recordedAt)
    {
        if(id == Guid.Empty)
            throw new DomainException(
                "Vehicle location id cannot be empty.");

        if (vehicleId == Guid.Empty)
            throw new DomainException(
                "Vehicle id cannot be empty.");

        if (latitude is < -90 or > 90)
            throw new DomainException(
                "Latitude must be between -90 and 90 degrees.");

        if (longitude is < -180 or > 180)
            throw new DomainException(
                "Longitude must be between -180 and 180 degrees.");

        Id = id;
        VehicleId = vehicleId;

        Position = new Point(longitude, latitude)
        {
            SRID = 4326
        };

        RecordedAt = recordedAt;
    }

    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Point Position { get; private set; } = default;
    public DateTimeOffset RecordedAt { get; private set; }


}
