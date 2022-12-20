using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public static class ProdutoRepositorio
{
    public static List<Produto>? Produtos { get; set; } = Produtos = new List<Produto>();

    public static void Init(IConfiguration configuracao)
    {
        var produtos = configuracao.GetSection("Produtos").Get<List<Produto>>();
        Produtos = produtos;
    }
}

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}
