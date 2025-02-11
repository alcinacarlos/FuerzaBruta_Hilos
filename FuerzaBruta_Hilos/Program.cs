namespace FuerzaBruta_Hilos;

/*
 * 1. Monohilo
 * 2. Multihilo, numero fijo, variable repartienda contraseñas
 * 3. Investigar nºhilos optimo
 * 4. Pool de hilos
 * 5. Test unitarios
 * 6. Interfaz personalizada
 */


class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../2151220-passwords.txt";
        
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        
        // 1. Leer el archivo .txt con el diccionario de contraseñas
        if (!File.Exists(fullPath))
        {
            Console.WriteLine("El archivo no existe");
            return;
        }

        var passwords = new List<String>(File.ReadAllLines(fullPath));
        if (passwords.Count == 0)
        {
            Console.WriteLine("El archivo está vacío");
            return;
        }
        
        // 2. Elegir una contraseña de forma aleatoria y calcular su hash
        Random random = new Random();
        string selectedPassword = passwords[random.Next(passwords.Count)];
        string hashedPassword = Utils.CalcularHash256(selectedPassword);

        Console.WriteLine($"Contraseña seleccionada: {selectedPassword}");
        Console.WriteLine($"Hash de la contraseña: {hashedPassword}");
        
        FinishEvent finalEvent = new FinishEvent();
        Wrapper<Action> eventoFinalizar = new Wrapper<Action>(() => { });
        
        var numeroHilos = Utils.PedirEntero("Introduce el numero de hilos: ");
        var passwordsNum = passwords.Count / numeroHilos;
        var hilos = new List<BruteForceHilo>();
        
        for (int i = 0; i < passwords.Count - numeroHilos; i+= passwordsNum)
        {
            hilos.Add(new BruteForceHilo(eventoFinalizar, passwords.GetRange(i, passwordsNum), hashedPassword));
        }
        
        for (var i = 0; i < hilos.Count; i++)
        {
            hilos[i].Start();
        }
        
    }
}
