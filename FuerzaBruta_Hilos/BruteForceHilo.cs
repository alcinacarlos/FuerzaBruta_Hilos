using System.Security.Cryptography;
using System.Text;

namespace FuerzaBruta_Hilos;

public class BruteForceHilo
{
    private Thread _hilo;
    private string _hashedPassword;
    private List<string> _passwords;
    private Wrapper<Action> _finalizar;
    private string _passwordFinalizada;

    public BruteForceHilo(Wrapper<Action> finalizar, List<string> passwords, string hashedPassword)
    {
        _hashedPassword = hashedPassword;
        _passwords = passwords;
        _finalizar = finalizar;
        _hilo = new Thread(_process);
    }

    public void Start()
    {
        _hilo.Start();
    }

    void _process()
    {
        // 3. Simular un ataque de fuerza bruta para adivinar la contraseña elegida
        Console.WriteLine("Iniciando ataque: ");
        foreach (string password in _passwords)
        {
            string hash = Utils.CalcularHash256(password);
            if (hash == _hashedPassword)
            {
                _passwordFinalizada = password;
                _finalizar.Value.Invoke();
                Console.WriteLine($"Contraseña encontrada: {password}");
                break;
            }
        }
    }
}