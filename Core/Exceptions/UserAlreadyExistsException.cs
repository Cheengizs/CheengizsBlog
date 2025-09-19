namespace Core.Exceptions;

public class UserAlreadyExistsException(string username) : Exception($"User '{username}' already exists.");