class FileAnalyzer { 
    static void Main(String[] args) {
        Console.WriteLine("Ingrese la ruta");
        string path = Console.ReadLine();

        if ( !Directory.Exists(path) ) {
            Console.WriteLine("La ruta no es válida");
            return;
        }
        


    }
}

class 