namespace Hackathon.Repository.Seed;

public static class SeedHelper
{
    /// <summary>
    /// BCrypt EnhancedHash (SHA256) of password "String1@" + Pepper "Matkhaubimat123",
    /// matching Auths/Service.cs login/register logic.
    /// Generated once and cached — same hash used for all seed users.
    /// </summary>
    public static string HashDefaultPassword()
    {
        return "$2a$11$6JmxlzTY.JGQBNoJCaMl4OSWS3ZGEGHMLglBlK8viRNX5pGT7dizK";
    }
}
