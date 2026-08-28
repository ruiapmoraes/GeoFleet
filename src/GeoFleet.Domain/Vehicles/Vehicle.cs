using GeoFleet.Domain.Common.Exceptions;

namespace GeoFleet.Domain.Vehicles;

public sealed class Vehicle
{
    private Vehicle()
    {
    }

    public Vehicle(Guid id, string plate)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException(
                "Vehicle id cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(plate))
        {
            throw new DomainException(
                "Vehicle plate cannot be null or empty.");
        }

        Id = id;

        // Normaliza a placa:
        // remove espaços nas extremidades
        // e converte para maiúsculas sem depender da cultura atual.
        Plate = plate.Trim().ToUpperInvariant();
    }

    public Guid Id { get; private set; }

    public string Plate { get; private set; } = string.Empty;
}