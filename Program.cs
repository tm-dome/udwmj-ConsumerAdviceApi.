using System.Text.Json;

// Configurar o HttpClient
using HttpClient client = new();

Console.WriteLine("=== CONSUMER ADVICE API ===");
Console.WriteLine("Buscando conselho do dia...");
Console.WriteLine();

try
{
    // Fazer requisição para a API
    HttpResponseMessage response = await client.GetAsync("https://api.adviceslip.com/advice");
    response.EnsureSuccessStatusCode();

    // Ler e processar o JSON
    string responseBody = await response.Content.ReadAsStringAsync();
    var jsonDocument = JsonDocument.Parse(responseBody);
    var advice = jsonDocument.RootElement
        .GetProperty("slip")
        .GetProperty("advice")
        .GetString();

    // Exibir o resultado
    Console.WriteLine("Conselho de Hoje:");
    Console.WriteLine(advice ?? "Não foi possível obter o conselho.");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Erro de conexão: {ex.Message}");
    Console.WriteLine("Verifique sua conexão com a internet.");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();