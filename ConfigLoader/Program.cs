using System.Text.Json;

string DIfilename = "DI.json";
string jsonString = File.ReadAllText(DIfilename);
tDI templateDI = JsonSerializer.Deserialize<tDI>(jsonString)!;

Console.WriteLine($"iostrin {templateDI.io_string}");
Console.WriteLine(templateDI.io_string, "DYNAMIC");
public struct tDI
{
    public string inv_string { get; set; }
    public string io_string {get; set; }
    public string access_string {get; set; }
    public string var_string {get; set; }
}
public struct tAI
{
    public string inv_string;
    public string io_string;
    public string access_string;
    public string var_string;
}
public struct tDO
{
    public string inv_string;
    public string io_string;
    public string access_string;
    public string var_string;
}
public struct tAO
{
    public string inv_string;
    public string io_string;
    public string access_string;
    public string var_string;
}
