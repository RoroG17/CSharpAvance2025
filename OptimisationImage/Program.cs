// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;


/*
 * Entrées : 
 * - num : une entier repésentant le numéro de l'image
 * - width : une entier repésentant la largeur de l'image
 * - height : une entier repésentant la hauteur de l'image
 * 
 * Cette fonction permet de télécharger une image depuis une URL et de la sauvegarder dans un dossier local.
 */
void DownloadImages(int num, int width, int height)
{
    string url = $"https://picsum.photos/{width}/{height}";

    string path = Path.Combine("images", $"image_{num}.jpg");

    HttpClient client = new HttpClient();
    var bytes = client.GetByteArrayAsync(url).Result;
    File.WriteAllBytes(path, bytes);

    Console.WriteLine($"Image {num} téléchargée dans {path}");
}

/*
 * Entrées : 
 * - num : une entier repésentant le numéro de l'image
 * - width : une entier repésentant la largeur de l'image
 * - height : une entier repésentant la hauteur de l'image
 * 
 * Cette fonction permet de télécharger une image depuis une URL et de la sauvegarder dans un dossier local.
 * La fonction est asynchrone, ce qui permet d'améliorer les performances en téléchargeant plusieurs images en parallèle.
 */
static async Task DownloadImagesOpti(int num, int width, int height)
{
    string url = $"https://picsum.photos/{width}/{height}";
    string path = Path.Combine("images", $"image_{num}.jpg");

    HttpClient client = new HttpClient();
    var bytes = client.GetByteArrayAsync(url).Result;
    File.WriteAllBytes(path, bytes);
    Console.WriteLine($"Image {num} téléchargée dans {path}");

    await Task.CompletedTask;
}

/*
 * Entrées : 
 * - src : une image à redimensionner
 * - maxSize : une entier repésentant la taille maximale de l'image
 * 
 * Cette fonction permet de redimensionner une image et de la sauvegarder dans un dossier local.
 */
static void SaveResized(Image src, int maxSize, string outputPath)
{
    using var clone = src.Clone(ctx =>
    {
        ctx.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,          // conserve le ratio
            Size = new Size(maxSize, maxSize),
        });
    });

    var encoder = new JpegEncoder { Quality = 85 }; // qualité du JPEG
    clone.Save(outputPath, encoder);
}

/*
 * Entrées : 
 * - src : une image à redimensionner
 * - maxSize : une entier repésentant la taille maximale de l'image
 * 
 * Cette fonction permet de redimensionner une image et de la sauvegarder dans un dossier local.
 * Cette fonction est asynchrone
 */
static async Task SaveResizedAsync(Image src, int maxSize, string outputPath)
{
    using var clone = src.Clone(ctx =>
    {
        ctx.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(maxSize, maxSize),
        });
    });

    var encoder = new JpegEncoder { Quality = 85 };
    await clone.SaveAsync(outputPath, encoder);
}

Directory.CreateDirectory("images"); // créer le dossier "images" s'il n'existe pas

// Algorithme séquenciel
var sw = Stopwatch.StartNew();

for (int i = 1; i <= 10; i++)
{
    // Télécharger l'image
    DownloadImages(i, 1920, 1080);

    // Récupération du chemin de l'image
    string inputPath = Path.Combine("images", $"image_{i}.jpg");

    // Redimensionner et sauvegarder l'image en 1080p
    string outputPath1080 = Path.Combine("images", $"image_{i}_1080.jpg");
    var img1080 = Image.Load(inputPath);
    SaveResized(img1080, 720, outputPath1080);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath1080}");

    // Redimensionner et sauvegarder l'image en 720p
    string outputPath720 = Path.Combine("images", $"image_{i}_720.jpg");
    var img720 = Image.Load(inputPath);
    SaveResized(img720, 720, outputPath720);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath720}");

    // Redimensionner et sauvegarder l'image en 480p
    string outputPath480 = Path.Combine("images", $"image_{i}_480.jpg");
    var img480 = Image.Load(inputPath);
    SaveResized(img480, 480, outputPath480);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath480}");
}

sw.Stop();
Console.WriteLine($"Le temps total du code est de {sw.ElapsedMilliseconds} ms");


// Algorithme optimisé
sw = Stopwatch.StartNew();

Parallel.For(1, 10, async i =>
{
    // Télécharger l'image
    Task t = DownloadImagesOpti(i, 1920, 1080);

    // Récupération du chemin de l'image
    string inputPath = Path.Combine("images", $"image_{i}.jpg");

    // Redimensionner et sauvegarder l'image en 1080p
    string outputPath1080 = Path.Combine("images", $"image_{i}_1080.jpg");
    var img1080 = Image.Load(inputPath);
    SaveResized(img1080, 720, outputPath1080);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath1080}");

    // Redimensionner et sauvegarder l'image en 720p
    string outputPath720 = Path.Combine("images", $"image_{i}_720.jpg");
    var img720 = Image.Load(inputPath);
    await SaveResizedAsync(img720, 720, outputPath720);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath720}");

    // Redimensionner et sauvegarder l'image en 480p
    string outputPath480 = Path.Combine("images", $"image_{i}_480.jpg");
    var img480 = Image.Load(inputPath);
    await SaveResizedAsync(img480, 480, outputPath480);
    Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath480}");
});

sw.Stop();
Console.WriteLine($"Le temps total du code optimisé est de {sw.ElapsedMilliseconds} ms");