class FileSystemAnalyzer { 
    static void Main(String[] args) {

        try {
            Console.WriteLine("Ingrese la ruta");
            string path = Console.ReadLine();

            if ( !Directory.Exists(path) ) {
                Console.WriteLine("La ruta no es válida");
                return;
            }
            Console.WriteLine("Ingrese el filtro de extensiones");
            string extensions = Console.ReadLine();

            FileDataAnalyzer analyzer = new FileDataAnalyzer();
            Func<string, bool> extensionFilter = null;

            if ( !string.IsNullOrEmpty( extensions ) ) { 
                extensionFilter = fileName => Path.GetExtension( fileName ).Equals(extensions, StringComparison.OrdinalIgnoreCase);
            
            }

            FileData data = analyzer.Analyze(path, extensionFilter);

            //Resultados
            Console.WriteLine($"Cantidad de Archivos: {data.TotalFiles}");
            Console.WriteLine($"Tamaño de los Archivos: {data.TotalSize}");
            Console.WriteLine($"Tamaño promedio: {data.AverageSize}");
            
            if ( data.LargestFile != null ) 
                Console.WriteLine($"{data.LargestFile.Name} es el archivo más grande. Pesa: {data.LargestFile.Length}");
            if ( data.LargestFile != null )
                Console.WriteLine($"{data.SmallesFile.Name} es el archivo más pequeño. Pesa: {data.SmallesFile.Length}");

        } catch (Exception e) { 
            Console.WriteLine(e.ToString());
        }
        
        


    }
}

class FileData {
    public int TotalFiles { get; set; }
    public double TotalSize { get; set; }

    public double AverageSize { get; set; }

    public FileInfo LargestFile { get; set; }

    public FileInfo SmallesFile { get; set; }
}

class FileDataAnalyzer { 
    public FileData Analyze(string path, Func <string, bool> extensionFilter) { 
        FileData data = new FileData();
        DirectoryInfo directory = new DirectoryInfo(path);

        FileInfo[] files = extensionFilter != null 
            ? directory.GetFiles().Where( file => extensionFilter(file.Name)).ToArray() 
            : directory.GetFiles();


        data.TotalFiles = files.Length;

        data.TotalSize = files.Sum( file => file.Length );

        if ( data.TotalFiles > 0 ) { 
            data.AverageSize = (double)data.TotalSize / data.TotalFiles;

            data.LargestFile = files.OrderByDescending( file => file.Length).First();   
            data.SmallesFile = files.OrderBy( file => file.Length ).First();

        }
        return data;

    }
}