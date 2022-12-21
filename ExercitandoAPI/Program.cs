using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.MapGet("/produtos/{Id}", ( [FromRoute] int Id) =>
{
    var produto = ProdutoRepositorio.GetBy(Id);
    if (produto != null)
    {
        return Results.Ok(produto);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

public static class ProdutoRepositorio
{
    public static List<Produto>? Produtos { get; set; } = Produtos = new List<Produto>();

    public static void Init(IConfiguration configuracao)
    {
        var produtos = configuracao.GetSection("Produtos").Get<List<Produto>>();
        Produtos = produtos;
    }

    public static void Add(Produto produto)
    {
        if (Produtos == null)
            Produtos = new List<Produto>();             //Inicialização da lista para armazenamento dos produtos

        Produtos.Add(produto);                          //Adicionando produto a nossa lista
    }

    public static Produto GetBy(int Id)
    {
        return Produtos.FirstOrDefault(p => p.Id == Id);
    }

    public static void Remove(Produto produto)
    {
        Produtos.Remove(produto);                          //Removendo produto de nossa lista
    }
}

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}
