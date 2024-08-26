namespace Institutional.Api.Boilerplate;

public class RegisterRequestWithPhone
{
    /// <summary>
    /// The user's PhoneNumber.
    /// </summary>
    public required string Phone { get; init; }
    
    /// <summary>
    /// The user's email address which acts as a user name.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string Password { get; init; }
}