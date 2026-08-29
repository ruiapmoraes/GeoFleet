using GeoFleet.Domain.Common.Exceptions;
using GeoFleet.Domain.Common.ValueObjects;
using GeoFleet.Domain.Vehicles;

namespace GeoFleet.Domain.Tests.Vehicles;

public sealed class VehicleLocationTests
{
    [Fact]
    public void Constructor_ShouldCreatePointWithSrid4326()
    {
        // Arrange
        var locationId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var coordinate = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        var recordedAt = DateTimeOffset.UtcNow;

        // Act
        var location = new VehicleLocation(
            locationId,
            vehicleId,
            coordinate,
            recordedAt);

        // Assert
        Assert.Equal(locationId, location.Id);
        Assert.Equal(vehicleId, location.VehicleId);
        Assert.Equal(coordinate, location.Coordinate);

        Assert.Equal(
            coordinate.Longitude,
            location.Position.X);

        Assert.Equal(
            coordinate.Latitude,
            location.Position.Y);

        Assert.Equal(
            4326,
            location.Position.SRID);

        Assert.Equal(
            recordedAt,
            location.RecordedAt);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenLocationIdIsEmpty()
    {
        var coordinate = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        var action = () => new VehicleLocation(
            Guid.Empty,
            Guid.NewGuid(),
            coordinate,
            DateTimeOffset.UtcNow);

        var exception =
            Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Vehicle location id cannot be empty.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenVehicleIdIsEmpty()
    {
        var coordinate = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        var action = () => new VehicleLocation(
            Guid.NewGuid(),
            Guid.Empty,
            coordinate,
            DateTimeOffset.UtcNow);

        var exception =
            Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Vehicle id cannot be empty.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenCoordinateIsNull()
    {
        var action = () => new VehicleLocation(
            Guid.NewGuid(),
            Guid.NewGuid(),
            null!,
            DateTimeOffset.UtcNow);

        Assert.Throws<ArgumentNullException>(action);
    }
}