namespace EncryptionWithEntityFramework.Domain.Users;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}
