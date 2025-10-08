using DataSources;
using System.Xml.Linq;

Console.WriteLine("####################\nEXERCICE 1\n####################");
var allAlbums = ListAlbumsData.ListAlbums;

var toShow = from album in allAlbums
             orderby album.AlbumId
             let stringDIsplay = $"Album n°{album.AlbumId}: {album.Title}"
             select stringDIsplay;

foreach (var line in toShow)
{
    Console.WriteLine(line);
}


Console.WriteLine("####################\nEXERCICE 2\n####################");
Console.WriteLine("Quelle est votre recherche");
var recherche = Console.ReadLine();

if (recherche != "" && recherche is not null)
{
    var toFind = from album in allAlbums
                 where album.Title.Contains(recherche, StringComparison.InvariantCultureIgnoreCase)
                 let stringDIsplay = $"Album n°{album.AlbumId}: {album.Title}"
                 select stringDIsplay;

    foreach (var line in toFind)
    { Console.WriteLine(line); }
}

Console.WriteLine("####################\nEXERCICE 3\n####################");
Console.WriteLine("Quelle est votre recherche");
var recherche3 = Console.ReadLine();

if (recherche3 != "" && recherche3 is not null)
{
    var toFind = from album in allAlbums
                 where album.Title.Contains(recherche3, StringComparison.InvariantCultureIgnoreCase)
                 orderby album.Title, album.AlbumId descending
                 let stringDIsplay = $"Album n°{album.AlbumId}: {album.Title}"
                 select stringDIsplay;

    foreach (var line in toFind)
    { Console.WriteLine(line); }
    Console.WriteLine("--------------------");
    var toFinfMethode = allAlbums.Where(album => album.Title.Contains(recherche3, StringComparison.InvariantCultureIgnoreCase))
                                .OrderBy(album => album.Title)
                                .ThenByDescending(album => album.AlbumId)
                                .Select(album => $"Album n°{album.AlbumId}: {album.Title}");

    foreach (var line in toFinfMethode)
    { Console.WriteLine(line); }
}

Console.WriteLine("####################\nEXERCICE 4\n####################");
Console.WriteLine("Quelle est votre recherche");
var recherche4 = Console.ReadLine();

if (recherche4 != "" && recherche4 is not null)
{
    var toFind = from album in allAlbums
                 where album.Title.Contains(recherche4, StringComparison.InvariantCultureIgnoreCase)
                 orderby album.Title, album.AlbumId descending
                 group album by album.ArtistId;

    foreach (var artist in toFind)
    {
        Console.WriteLine($"Artiste n°{artist.Key}:");

        foreach (var album in artist)
        {
            Console.WriteLine($"\t + {album.Title}");
        }
    }

}

Console.WriteLine("####################\nEXERCICE 5\n####################");
Console.WriteLine("Quelle est votre recherche");
var rechercheArtist = Console.ReadLine();

var allArtists = ListArtistsData.ListArtists;

if (rechercheArtist is not null)
{
    var toFind = from album in allAlbums
                 join artist in allArtists on album.ArtistId equals artist.ArtistId
                 where artist.Name.Contains(rechercheArtist, StringComparison.InvariantCultureIgnoreCase)
                 group album by new { artist.ArtistId, artist.Name };

    foreach (var artist in toFind)
    {
        Console.WriteLine($"Artiste n°{artist.Key.ArtistId}: {artist.Key.Name}");

        foreach (var album in artist)
        {
            Console.WriteLine($"\t + {album.Title}");
        }
    }
}

Console.WriteLine("####################\nEXERCICE 6\n####################");

var enter = "";
var rep = 0;
while (enter != "q")
{
    var liste = (from album in allAlbums
                 let affichageAlbum = $"\tALbum n°{album.AlbumId} : {album.Title}"
                 orderby album.AlbumId
                 select affichageAlbum).Skip(rep * 20).Take(20);

    foreach (var album in liste)
    {
        Console.WriteLine(album);
    }
    rep++;
    Console.WriteLine("Appuyez sur Q pour quiter");
    enter = Console.ReadLine();
}

Console.WriteLine("####################\nEXERCICE 7\n####################");

Console.WriteLine("QUelle est votre recherhce :");
var recherche7 = Console.ReadLine();

var text = from lines in File.ReadAllLines($@"{Directory.GetCurrentDirectory()}/text/Albums.txt")
           where lines.Contains(recherche7, StringComparison.OrdinalIgnoreCase)
           select lines;

var xml = new XElement("Root",
            from line in text
            select new XElement("Album",
                  new XElement("AlbumId", line.Split(':')[0]),
                  new XElement("title", line.Split(':')[1])

              )
         );

Console.WriteLine(xml);


