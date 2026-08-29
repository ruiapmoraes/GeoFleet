using GeoFleet.Domain.Common.Exceptions;
using GeoFleet.Domain.Common.ValueObjects;
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
        GeoCoordinate coordinate,
        DateTimeOffset recordedAt)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException(
                "Vehicle location id cannot be empty.");
        }

        if (vehicleId == Guid.Empty)
        {
            throw new DomainException(
                "Vehicle id cannot be empty.");
        }

        ArgumentNullException.ThrowIfNull(coordinate);

        Id = id;
        VehicleId = vehicleId;

        // Importante:
        // mantém o Value Object dentro da entidade.
        Coordinate = coordinate;

        Position = new Point(
            coordinate.Longitude,
            coordinate.Latitude)
        {
            SRID = 4326
        };

        RecordedAt = recordedAt;
    }

    public Guid Id { get; private set; }

    public Guid VehicleId { get; private set; }

    public GeoCoordinate Coordinate { get; private set; } = default!;

    public Point Position { get; private set; } = default!;

    public DateTimeOffset RecordedAt { get; private set; }
}