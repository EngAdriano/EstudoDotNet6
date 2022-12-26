using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();
var configuracao = app.Configuration;                   //Inicializando nossa lista pelo
ProdutoRepositorio.Init(configuracao);                  //appsettings.json

app.MapGet("/", () => "Hello World!");

//Estudo da criação de CRUD simples

app.MapPost("/produtos", (Produto produto) =>
{
    ProdutoRepositorio.Add(produto);                                   //Recebe um produto e grava na lista
    return Results.Created($"/products/{produto.Id}", produto.Id);     //Retornar o statusCode da operação + o Id 
});

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

app.MapPut("/produtos", (Produto produto) =>
{
    var produtoSalvo = ProdutoRepositorio.GetBy(produto.Id);
    produtoSalvo.Nome = produto.Nome;
    produtoSalvo.Descricao = produto.Descricao;
    return Results.Ok();
});

app.MapDelete("produtos/{Id}", ([FromRoute] int Id) =>
{
    var produtoSalvo = ProdutoRepositorio.GetBy(Id);
    ProdutoRepositorio.Remove(produtoSalvo);
    return Results.Ok();
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

public class ApplicationDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }        //Indica que a class Produto precisa ser mapeada para um banco de dados

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlServer("Server=localhost;Database=Produtos;User Id=sa;Password=Liukin@1208;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES");
}
