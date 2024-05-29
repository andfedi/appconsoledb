using System.Text.Json;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();

        var url = "https://jsonplaceholder.typicode.com/posts";
        var postData = new
        {
            title = "foo",
            body = "bar",
            userId = 1
        };

        var json = JsonSerializer.Serialize(postData);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, data);

        string result = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response Status Code: {response.StatusCode}");
        Console.WriteLine("Response Content:");
        Console.WriteLine(result);
    }
}