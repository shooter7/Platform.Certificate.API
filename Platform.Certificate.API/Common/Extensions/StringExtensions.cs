namespace Platform.Certificate.API.Common.Extensions;
using Cipher = BCrypt.Net.BCrypt;
public static class StringExtensions
{
    public static string HashPassword(this string value)
    {
        return Cipher.HashPassword(value);
    }

    public static bool VerifyHash(this string hash, string value)
    {
        return Cipher.Verify(text: value, hash: hash);
    }
}