using GeoFleet.Domain.Common.Exceptions;
using GeoFleet.Domain.Common.ValueObjects;

namespace GeoFleet.Domain.Tests.Common.ValueObjects;

public sealed class GeoCoordinateTests
{
    [Fact]
    public void Create_ShouldCreateCoordinate()
    {
        // Arrange
        const double latitude = -23.5505;
        const double longitude = -46.6333;

        // Act
        var coordinate = GeoCoordinate.Create(
            latitude,
            longitude);

        // Assert
        Assert.Equal(
            latitude,
            coordinate.Latitude);

        Assert.Equal(
            longitude,
            coordinate.Longitude);
    }

    [Theory]
    [InlineData(-90.1)]
    [InlineData(90.1)]
    [InlineData(-100)]
    [InlineData(100)]
    public void Create_ShouldThrow_WhenLatitudeIsInvalid(
        double latitude)
    {
        // Act
        var action = () => GeoCoordinate.Create(
            latitude,
            -46.6333);

        // Assert
        var exception =
            Assert.Throws<DomainException>(action);

        Assert.Contains(
            "Latitude must be between -90 and 90 degrees.",
            exception.Message);
    }

    [Theory]
    [InlineData(-180.1)]
    [InlineData(180.1)]
    [InlineData(-200)]
    [InlineData(200)]
    public void Create_ShouldThrow_WhenLongitudeIsInvalid(
        double longitude)
    {
        // Act
        var action = () => GeoCoordinate.Create(
            -23.5505,
            longitude);

        // Assert
        var exception =
            Assert.Throws<DomainException>(action);

        Assert.Contains(
            "Longitude must be between -180 and 180 degrees.",
            exception.Message);
    }

    [Theory]
    [InlineData(-90)]
    [InlineData(90)]
    public void Create_ShouldAcceptLatitudeBoundaryValues(
        double latitude)
    {
        // Act
        var coordinate = GeoCoordinate.Create(
            latitude,
            -46.6333);

        // Assert
        Assert.Equal(
            latitude,
            coordinate.Latitude);
    }

    [Theory]
    [InlineData(-180)]
    [InlineData(180)]
    public void Create_ShouldAcceptLongitudeBoundaryValues(
        double longitude)
    {
        // Act
        var coordinate = GeoCoordinate.Create(
            -23.5505,
            longitude);

        // Assert
        Assert.Equal(
            longitude,
            coordinate.Longitude);
    }

    [Fact]
    public void CoordinatesWithSameValues_ShouldBeEqual()
    {
        // Arrange
        var first = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        var second = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        // Assert
        Assert.Equal(first, second);
    }

    [Fact]
    public void CoordinatesWithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var first = GeoCoordinate.Create(
            -23.5505,
            -46.6333);

        var second = GeoCoordinate.Create(
            -22.9068,
            -43.1729);

        // Assert
        Assert.NotEqual(first, second);
    }
}