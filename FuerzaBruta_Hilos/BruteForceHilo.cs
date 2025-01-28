using System.Security.Cryptography;
using System.Text;

namespace FuerzaBruta_Hilos;

public class BruteForceHilo
{
    Thread hilo;
    private string hashedPassword;
    private List<string> passwords;
    private Wrapper<Action> finalizar;
    private string passwordFinalizada;

    public BruteForceHilo(Wrapper<Action> finalizar, List<string> passwords, string hashedPassword)
    {
        this.hashedPassword = hashedPassword;
        this.passwords = passwords;
        this.finalizar = finalizar;
        hilo = new Thread(_process);
    }

    public void Start()
    {
        hilo.Start();
    }

    void _process()
    {
        // 3. Simular un ataque de fuerza bruta para adivinar la contraseña elegida
        Console.WriteLine("Iniciando ataque: ");
        foreach (string password in passwords)
        {
            string hash = Utils.calcularHash256(password);
            if (hash == hashedPassword)
            {
                passwordFinalizada = password;
                finalizar.Value.Invoke();
                Console.WriteLine($"Contraseña encontrada: {password}");
                break;
            }
        }
    }
}