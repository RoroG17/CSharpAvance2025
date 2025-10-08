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

var sw2 = Stopwatch.StartNew();

string outputDir = "imagesDownloaded";
Directory.CreateDirectory(outputDir);

int count = 10; // nombre d'images à télécharger
string url = "https://picsum.photos/1920/1080";
sw2.Restart();
DownloadImagesSequential(url, outputDir, count);
sw2.Stop();
Console.WriteLine($"⏱️ Séquentiel : {sw2.ElapsedMilliseconds} ms\n");

sw2.Restart();
await DownloadImagesAsync(url, outputDir, count);
sw2.Stop();
Console.WriteLine($"⚡ Asynchrone : {sw2.ElapsedMilliseconds} ms\n");

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

var numbers = new List<int>();

Parallel.For(0, 10000, i =>
{
    numbers.Add(i);
});

Console.WriteLine($"Total ajouté : {numbers.Count}");