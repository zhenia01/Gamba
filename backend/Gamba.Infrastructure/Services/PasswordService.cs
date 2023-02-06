using BC = BCrypt.Net.BCrypt;

namespace Gamba.Infrastructure.Services;

public static class PasswordService
{
    public static string HashPassword(string password) => BC.HashPassword(password);

    public static bool CheckPassword(string candidatePassword, string hashedPassword) =>
        BC.Verify(candidatePassword, hashedPassword);
}