using FluentValidation;

namespace CheengizsBlog_ex.Contracts;

public record UserResponse(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    string Role);