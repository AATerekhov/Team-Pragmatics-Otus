namespace Services.Implementations.Exceptions;
public class PlaceTypeNotExistException(int studentId) : Exception($"PlaceType with id: {studentId} not exist.");
