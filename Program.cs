using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Generating dictionary...");
        DictionaryGenerator.GenerateDictionary();

        Console.WriteLine("Finding correct password...");
        string username = "John";
        string password = FindCorrectPassword("dict.txt", username);

        if (password != null)
        {
            Console.WriteLine("Password found: {0}", password);
            string uploadUrl = Authenticate(username, password);

            if (!string.IsNullOrEmpty(uploadUrl))
            {
                Console.WriteLine("Uploading CV...");
                UploadCV(uploadUrl);
                Console.WriteLine("CV uploaded successfully!");
            }
        }
        else
        {
            Console.WriteLine("Password not found in dictionary.");
        }
    }

    static string FindCorrectPassword(string filePath, string username)
    {
        foreach (var password in File.ReadLines(filePath))
        {
            if (Authenticate(username, password) != null)
            {
                return password;
            }
        }
        return null;
    }

    static string? Authenticate(string username, string password)
    {
        string url = "http://recruitment.warpdevelopment.co.za/api/authenticate";
        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers["Authorization"] = $"Basic {auth}";
        request.Method = "GET";

        try
        {
            var response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        catch (WebException)
        {
            return null;
        }
    }

    static void UploadCV(string uploadUrl)
    {
        string zipPath = "submission.zip";
        CreateZipFile(zipPath);

        byte[] zipBytes = File.ReadAllBytes(zipPath);
        string encodedZip = Convert.ToBase64String(zipBytes);

        var payload = new
        {
            Data = encodedZip,
            Name = "Your Name",
            Surname = "Your Surname",
            Email = "email@domain.com"
        };

        string json = JsonConvert.SerializeObject(payload);
        var request = (HttpWebRequest)WebRequest.Create(uploadUrl);
        request.Method = "POST";
        request.ContentType = "application/json";

        using (var writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(json);
        }

        var response = (HttpWebResponse)request.GetResponse();
        Console.WriteLine("Upload Response: {0}", response.StatusCode);
    }

    static void CreateZipFile(string zipPath)
    {
        using (var archive = System.IO.Compression.ZipFile.Open(zipPath, System.IO.Compression.ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile("Resume.pdf", "Resume.pdf");
            archive.CreateEntryFromFile("dict.txt", "dict.txt");
            archive.CreateEntryFromFile("Program.cs", "Program.cs");
            archive.CreateEntryFromFile("DictionaryGenerator.cs", "DictionaryGenerator.cs");
        }
    }
}
