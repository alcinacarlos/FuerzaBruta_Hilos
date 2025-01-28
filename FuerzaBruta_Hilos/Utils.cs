using System.Security.Cryptography;
using System.Text;

namespace FuerzaBruta_Hilos;

public static class Utils
{
    // Método para calcular el hash SHA256
    public static string calcularHash256(string data)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}