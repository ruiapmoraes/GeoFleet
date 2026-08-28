using GeoFleet.Domain.Common.Exceptions;
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
        const double latitude = -23.5505;
        const double longitude = -46.6333;
        var recordedAt = DateTimeOffset.UtcNow;

        // Act
        var location = new VehicleLocation(
            locationId,
            vehicleId,
            latitude,
            longitude,
            recordedAt);

        // Assert
        Assert.Equal(locationId, location.Id);
        Assert.Equal(vehicleId, location.VehicleId);

        Assert.Equal(longitude, location.Position.X);
        Assert.Equal(latitude, location.Position.Y);

        Assert.Equal(4326, location.Position.SRID);
        Assert.Equal(recordedAt, location.RecordedAt);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenLocationIdIsEmpty()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();

        // Act
        var action = () => new VehicleLocation(
            Guid.Empty,
            vehicleId,
            -23.5505,
            -46.6333,
            DateTimeOffset.UtcNow);

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Vehicle location id cannot be empty.",
            exception.Message);
    }

    [Theory]
    [InlineData(-90.1)]
    [InlineData(90.1)]
    [InlineData(-100)]
    [InlineData(100)]
    public void Constructor_ShouldThrow_WhenLatitudeIsInvalid(
        double latitude)
    {
        // Arrange
        var locationId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();

        // Act
        var action = () => new VehicleLocation(
            locationId,
            vehicleId,
            latitude,
            -46.6333,
            DateTimeOffset.UtcNow);

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Latitude must be between -90 and 90 degrees.",
            exception.Message);
    }

    [Theory]
    [InlineData(-180.1)]
    [InlineData(180.1)]
    [InlineData(-200)]
    [InlineData(200)]
    public void Constructor_ShouldThrow_WhenLongitudeIsInvalid(
        double longitude)
    {
        // Arrange
        var locationId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();

        // Act
        var action = () => new VehicleLocation(
            locationId,
            vehicleId,
            -23.5505,
            longitude,
            DateTimeOffset.UtcNow);

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Longitude must be between -180 and 180 degrees.",
            exception.Message);
    }

    [Theory]
    [InlineData(-90)]
    [InlineData(90)]
    public void Constructor_ShouldAcceptLatitudeBoundaryValues(
        double latitude)
    {
        // Act
        var location = new VehicleLocation(
            Guid.NewGuid(),
            Guid.NewGuid(),
            latitude,
            -46.6333,
            DateTimeOffset.UtcNow);

        // Assert
        Assert.Equal(latitude, location.Position.Y);
    }

    [Theory]
    [InlineData(-180)]
    [InlineData(180)]
    public void Constructor_ShouldAcceptLongitudeBoundaryValues(
        double longitude)
    {
        // Act
        var location = new VehicleLocation(
            Guid.NewGuid(),
            Guid.NewGuid(),
            -23.5505,
            longitude,
            DateTimeOffset.UtcNow);

        // Assert
        Assert.Equal(longitude, location.Position.X);
    }
}