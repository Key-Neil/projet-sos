namespace Infrastructure.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string Role { get; set; } = "Client"; // On ajoute la propriété Role

    // On ajoute '?' pour indiquer que ces champs peuvent être nuls
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}