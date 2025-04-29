
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;
using Entities;

class Program
{
    static void Main()
    {
        string jsonPath = "data/accounts.json";
        string xmlPath = "data/accounts.xml";

        // Read JSON
        var jsonData = File.ReadAllText(jsonPath);
        var accounts = JsonSerializer.Deserialize<List<BaseAccount>>(jsonData) ?? new List<BaseAccount>();
        
        foreach (var acc in accounts)
            Console.WriteLine($"[JSON] ID: {acc.Id}, Name: {acc.Name}, Email: {acc.Email}");

        // Add new entry
        accounts.Add(new BaseAccount { Id = 3, Name = "Faye", Email = "faye@demo.com" });
        File.WriteAllText(jsonPath, JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true }));

        // Read XML
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        XmlNodeList? nodes = doc.SelectNodes("/Accounts/Account");

        if (nodes != null)
        {
            foreach (XmlNode node in nodes)
            {
                var id = node["Id"]?.InnerText ?? "N/A";
                var name = node["Name"]?.InnerText ?? "N/A";
                var email = node["Email"]?.InnerText ?? "N/A";

                Console.WriteLine($"[XML] ID: {id}, Name: {name}, Email: {email}");
            }
        }
        else
        {
            Console.WriteLine("[XML] No account nodes found.");
        }
    }
}
