// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

string fileName = "joueurs.csv";

XElement convertXML(IEnumerable<string> text , string separator)
{
    var header = text.First().Split(separator);

    var xml = new XElement("Root",
        from line in text.Skip(1)
        let values = line.Split(separator)
        select new XElement("Document",
            Enumerable.Range(0, header.Length)
                      .Select(i => new XElement(header[i], values[i]))
        )
    );

    Console.WriteLine(xml);
    return xml;
}

void AfficherColonnes (string[] header)
{
    for (int i = 0; i < header.Length; i++)
    {
        Console.WriteLine($"{i} - {header[i]}");
    }
}
IEnumerable<string> AfficherDonnees(string path, string separator)
{
    var text = from lines in File.ReadAllLines(path)
               select lines;

    return text;
}

IEnumerable<string>? TrierDonnees(string path, string separator)
{
    string[] header = File.ReadAllLines(path).First().Split(separator);
    AfficherColonnes(header);
    var column = Console.ReadLine();

    if (!int.TryParse(column, out int columnIndex))
    {
        Console.WriteLine("Entrée invalide.");
        return null;
    }
    Console.WriteLine("Souhaitez-vous ordonnez les données dans l'ordre croissant ?\n" +
            "1 - Oui\n" +
            "2 - Non\n");
    var order = Console.ReadLine();

    

    if (order == "1")
    {
        var text = from line in File.ReadAllLines(path).Skip(1)
                   orderby line.Split(separator)[int.Parse(column)]
                   select line;
        return text.Prepend(string.Join(separator, header));
    }
    else if (order == "2")
    {
        var text = from line in File.ReadAllLines(path).Skip(1)
                   orderby line.Split(separator)[int.Parse(column)] descending
                   select line;

        return text.Prepend(string.Join(separator, header));
    }

    return null;
}

IEnumerable<string>? FiltrerDonnees(string path, string separator)
{
    string[] header = File.ReadAllLines(path).First().Split(separator);
    Console.WriteLine("Sur quelle colonne souhaitez-vous filtrer ?");
    AfficherColonnes(header);
    var column = Console.ReadLine();

    if (!int.TryParse(column, out int columnIndex))
    {
        Console.WriteLine("Entrée invalide. Veuillez entrer un numéro de colonne valide.");
        return null;
    }
    Console.WriteLine("Quelle est votre recherche ?");
    var recherche = Console.ReadLine();

    var text = from line in File.ReadAllLines(path).Skip(1)
               where line.Split(separator)[int.Parse(column)].Contains(recherche, StringComparison.InvariantCultureIgnoreCase)
               select line;

    return text.Prepend(string.Join(',', header));
}

string directory = $@"{Directory.GetCurrentDirectory()}\{fileName}";

Console.WriteLine(directory);

if (!File.Exists(directory))
{
    Console.WriteLine("Le fichier n'existe pas.");
}
else
{

    Console.WriteLine("Quelle est le séparateur de votre fichier CSV ?");
    var separator = Console.ReadLine();

    if (separator == null || separator == "")
    {
        Console.WriteLine($"Le séparateur ne peut pas être vide");
    }
    else
    {


        Console.WriteLine("###########\n" +
            "MENU\n" +
            "1 - Afficher les données\n" +
            "2 - Trier les données\n" +
            "3 - Filtrer les données\n" +
            "##########");

        var menu = Console.ReadLine();

        IEnumerable<string>? data = new List<string>();
        switch (menu)
        {
            case "1":
                data = AfficherDonnees(directory, separator);
                break;
            case "2":
                data = TrierDonnees(directory, separator);
                break;
            case "3":
                data = FiltrerDonnees(directory, separator);
                break;
            default:
                Console.WriteLine("Option invalide.");
                break;
        }

        if (data != null)
        {
            XElement xml = convertXML(data, separator);

            Console.WriteLine("###########\n" +
                "Souhiatez-vous exporter ses résultats en format xml ?\n" +
                "1 - Oui\n" +
                "2 - Non\n" +
                "##########");

            var export = Console.ReadLine();

            switch (export)
            {
                case "1":
                    Console.WriteLine("Exportation des résultats...");
                    xml.Save("export.xml");
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "export.xml");
                    Console.WriteLine("Résultats exportés dans le fichier : " + filePath);
                    break;
                case "2":
                    Console.WriteLine("Fin du programme.");
                    break;
                default:
                    Console.WriteLine("Option invalide.");
                    break;
            }
        }
    }
}