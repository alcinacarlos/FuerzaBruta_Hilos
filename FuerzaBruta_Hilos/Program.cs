namespace FuerzaBruta_Hilos;

class Program
{
    static void Main(string[] args)
    {
        // ruta absoluta, cambiar
        string filePath = "/home/seta/RiderProjects/Fuerza_bruta/Fuerza_bruta/2151220-passwords.txt";

        // 1. Leer el archivo .txt con el diccionario de contraseñas
        if (!File.Exists(filePath))
        {
            Console.WriteLine("El archivo no existe");
            return;
        }

        var passwords = new List<String>(File.ReadAllLines(filePath));
        if (passwords.Count == 0)
        {
            Console.WriteLine("El archivo está vacío");
            return;
        }
        
        // 2. Elegir una contraseña de forma aleatoria y calcular su hash
        Random random = new Random();
        string selectedPassword = passwords[random.Next(passwords.Count)];
        string hashedPassword = Utils.calcularHash256(selectedPassword);

        Console.WriteLine($"Contraseña seleccionada: {selectedPassword}");
        Console.WriteLine($"Hash de la contraseña: {hashedPassword}");
        
        FinishEvent finalEvent = new FinishEvent();
        Wrapper<Action> eventoFinalizar = new Wrapper<Action>(() => { });
        
        var numeroHilos = PedirEntero("Introduce el numero de hilos: ");
        var passwordsNum = passwords.Count / numeroHilos;
        var hilos = new List<BruteForceHilo>();
        
        for (int i = 0; i < passwords.Count; i+= passwordsNum)
        {
            hilos.Add(new BruteForceHilo(eventoFinalizar, passwords.GetRange(i, passwordsNum), hashedPassword));
        }
        
        for (var i = 0; i < hilos.Count; i++)
        {
            hilos[i].Start();
        }
        
    }
    
    static int PedirEntero(string mensaje)
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
