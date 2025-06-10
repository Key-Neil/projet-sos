namespace Infrastructure.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
   
}

