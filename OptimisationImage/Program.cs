// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;


void DownloadImages(int num, int width, int height)
{
    string url = $"https://picsum.photos/{width}/{height}";

    string path = Path.Combine("images", $"image_{num}.jpg");

    HttpClient client = new HttpClient();
    var bytes = client.GetByteArrayAsync(url).Result;
    File.WriteAllBytes(path, bytes);

    //Console.WriteLine($"Image {num} téléchargée dans {path}");
}


static async Task DownloadImagesOpti(int num, int width, int height)
{
    string url = $"https://picsum.photos/{width}/{height}";
    string path = Path.Combine("images", $"image_{num}.jpg");

    HttpClient client = new HttpClient();
    var bytes = client.GetByteArrayAsync(url).Result;
    File.WriteAllBytes(path, bytes);
    //Console.WriteLine($"Image {num} téléchargée dans {path}");

    await Task.CompletedTask;
}

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

for (int h = 1; h <= 10; h++)
{

    var sw = Stopwatch.StartNew();

    for (int i = 1; i <= 10; i++)
    {
        DownloadImages(i, 1920, 1080);
        string inputPath = Path.Combine("images", $"image_{i}.jpg");

        string outputPath1080 = Path.Combine("images", $"image_{i}_1080.jpg");
        var img1080 = Image.Load(inputPath);
        SaveResized(img1080, 720, outputPath1080);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath1080}");

        string outputPath720 = Path.Combine("images", $"image_{i}_720.jpg");
        var img720 = Image.Load(inputPath);
        SaveResized(img720, 720, outputPath720);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath720}");

        string outputPath480 = Path.Combine("images", $"image_{i}_480.jpg");
        var img480 = Image.Load(inputPath);
        SaveResized(img480, 480, outputPath480);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath480}");
    }

    sw.Stop();
    Console.WriteLine($"Le temps total du code est de {sw.ElapsedMilliseconds} ms");



    sw = Stopwatch.StartNew();

    Parallel.For(1, 10, async i =>
    {
        Task t = DownloadImagesOpti(i, 1920, 1080);
        string inputPath = Path.Combine("images", $"image_{i}.jpg");

        string outputPath1080 = Path.Combine("images", $"image_{i}_1080.jpg");
        var img1080 = Image.Load(inputPath);
        SaveResized(img1080, 720, outputPath1080);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath1080}");

        string outputPath720 = Path.Combine("images", $"image_{i}_720.jpg");
        var img720 = Image.Load(inputPath);
        await SaveResizedAsync(img720, 720, outputPath720);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath720}");

        string outputPath480 = Path.Combine("images", $"image_{i}_480.jpg");
        var img480 = Image.Load(inputPath);
        await SaveResizedAsync(img480, 480, outputPath480);
        //Console.WriteLine($"Image {i} redimensionnée et sauvegardée dans {outputPath480}");
    });

    sw.Stop();
    Console.WriteLine($"Le temps total du code optimisé est de {sw.ElapsedMilliseconds} ms");
}