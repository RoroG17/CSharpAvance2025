// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Calcul de performance");

var sw = Stopwatch.StartNew();

double sum = 1;

for (int i = 1; i <= 50_000_000; i++)
{
    sum += Math.Sin(i) + Math.Cos(i);

    sum += Math.Sqrt(i);

    sum += Math.Exp(i % 10) + Math.Log(i);

    sum += Math.Pow(i % 100, 3);

    sum *= 1.0000001;
}

sw.Stop();

Console.WriteLine($"Le templs de calcul est de {sw.ElapsedMilliseconds} ms");

sw = Stopwatch.StartNew();

sum = 1;

Parallel.For(1, 50_000_000, i =>
{
    sum += Math.Sin(i) + Math.Cos(i);

    sum += Math.Sqrt(i);

    sum += Math.Exp(i % 10) + Math.Log(i);

    sum += Math.Pow(i % 100, 3);

    sum *= 1.0000001;
});

sw.Stop();

Console.WriteLine($"Le templs de calcul parallèle est de {sw.ElapsedMilliseconds} ms");

using System.Diagnostics;

var sw = Stopwatch.StartNew();

string outputDir = "imagesDownloaded";
Directory.CreateDirectory(outputDir);

int count = 10; // nombre d'images à télécharger
string url = "https://picsum.photos/1920/1080";
sw.Restart();
DownloadImagesSequential(url, outputDir, count);
sw.Stop();
Console.WriteLine($"⏱️ Séquentiel : {sw.ElapsedMilliseconds} ms\n");

sw.Restart();
await DownloadImagesAsync(url, outputDir, count);
sw.Stop();
Console.WriteLine($"⚡ Asynchrone : {sw.ElapsedMilliseconds} ms\n");

// 🧱 VERSION 1 — Séquentielle (bloquante)
static void DownloadImagesSequential(string url, string outputDir, int count)
{
    using var http = new HttpClient();

    Parallel.For(0, count, i =>
    {
        var bytes = http.GetByteArrayAsync(url).Result; // ⚠️ blocant
        string path = Path.Combine(outputDir, $"seq_{i + 1}.jpg");
        File.WriteAllBytes(path, bytes);
        Console.WriteLine($"[Séquentiel] Image {i + 1}/{count} téléchargée");
    });
}

// ⚡ VERSION 2 — Asynchrone (non bloquante)
static async Task DownloadImagesAsync(string url, string outputDir, int count)
{
    using var http = new HttpClient();
    var tasks = new List<Task>();

    for (int i = 0; i < count; i++)
    {
        string path = Path.Combine(outputDir, $"async_{i + 1}.jpg");
        tasks.Add(DownloadSingleImageAsync(http, url, path, i + 1, count));
    }

    await Task.WhenAll(tasks); // ✅ tous les téléchargements en parallèle
}

static async Task DownloadSingleImageAsync(HttpClient http, string url, string path, int index, int total)
{
    var bytes = await http.GetByteArrayAsync(url);
    await File.WriteAllBytesAsync(path, bytes); // Écriture asynchrone
    Console.WriteLine($"[Async] Image {index}/{total} téléchargée");
}

static void DownloadSingleImage(HttpClient http, string url, string path, int index, int total)
{
    var bytes = http.GetByteArrayAsync(url).Result;
    File.WriteAllBytes(path, bytes); // Écriture synchrone
    Console.WriteLine($"[Sync] Image {index}/{total} téléchargée");
}

var numbers = new List<int>();

Parallel.For(0, 10000, i =>
{
    numbers.Add(i);
});

Console.WriteLine($"Total ajouté : {numbers.Count}");