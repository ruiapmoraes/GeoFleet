using GeoFleet.Domain.Common.Exceptions;
using GeoFleet.Domain.Vehicles;

namespace GeoFleet.Domain.Tests.Vehicles;

public sealed class VehicleTests
{
    [Fact]
    public void Constructor_ShouldCreateVehicle()
    {
        var vehicle = new Vehicle(
            Guid.NewGuid(),
            "abc1d23");

        Assert.Equal("ABC1D23", vehicle.Plate);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenPlateIsEmpty()
    {
        Assert.Throws<DomainException>(() =>
            new Vehicle(
                Guid.NewGuid(),
                ""));
    }
}