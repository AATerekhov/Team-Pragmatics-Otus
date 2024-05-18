namespace Services.Implementations.Exceptions;
public class FuellingNotException(Guid Id) :Exception($"Place with id: {Id} not exist.");
