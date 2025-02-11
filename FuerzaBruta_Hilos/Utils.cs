using System.Security.Cryptography;
using System.Text;

namespace FuerzaBruta_Hilos;

public static class Utils
{
    // Método para calcular el hash SHA256
    public static string CalcularHash256(string data)
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
    
    public static int PedirEntero(string mensaje)
    {
        int resultado;
        bool esValido;

        do
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();
            esValido = int.TryParse(entrada, out resultado) && resultado > 0;
            
            if (!esValido)
            {
                Console.WriteLine("Ingresa un numero valido positivo");
            }
        } while (!esValido);

        return resultado;
    }
}