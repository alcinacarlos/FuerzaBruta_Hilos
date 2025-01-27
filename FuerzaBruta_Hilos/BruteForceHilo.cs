using System.Security.Cryptography;
using System.Text;

namespace Fuerza_bruta;

public class BruteForceHilo
{
    Thread hilo;
    private List<string> passwords;
    private Wrapper<Action> finalizar;
    private string passwordFinalizada;

    public BruteForceHilo(Wrapper<Action> finalizar, List<string> passwords)
    {
        this.finalizar = finalizar;
        finalizar.Value += () => { Console.WriteLine(passwordFinalizada); };
        hilo = new Thread(_process);
    }

    public void Start()
    {
        hilo.Start();
    }

    void _process()
    {
        // 2. Elegir una contraseña de forma aleatoria y calcular su hash
        Random random = new Random();
        string selectedPassword = passwords[random.Next(passwords.Count)];
        string hashedPassword = calcularHash256(selectedPassword);

        Console.WriteLine($"Contraseña seleccionada: {selectedPassword}");
        Console.WriteLine($"Hash de la contraseña: {hashedPassword}");

        // 3. Simular un ataque de fuerza bruta para adivinar la contraseña elegida
        Console.WriteLine("Iniciando ataque: ");
        foreach (string password in passwords)
        {
            string hash = calcularHash256(password);
            if (hash == hashedPassword)
            {
                Console.WriteLine($"Contraseña encontrada: {password}");
                break;
            }
        }
        finalizar.Value.Invoke();
    }
    // Método para calcular el hash SHA256
    static string calcularHash256(string data)
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