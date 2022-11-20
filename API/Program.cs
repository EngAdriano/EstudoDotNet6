var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Endpoint simples com método GET
app.MapGet("/", () => "Hello World!");                              //Endpoint com método de acesso GET - Retorna "Hello world"

//Endpoint simples com método POST e enviando um objeto. Será automaticamente convertido para Json
app.MapPost("/", () => new {Name = "Jose Adriano", Age = 58});      //Endpoint com método de acesso POST com um objeto - Retorna meu nome

//Endpoint com função lâmbda com mais de uma linha
app.MapGet("/AddHeader", (HttpResponse response) =>                 //Endpoint com método de acesso GET - Altera o cabeçalho e retorna un
{                                                                   //objeto
    response.Headers.Add("Teste", "Jose Adriano");                  
    return new { Name = "Jose Adriano", Age = 58 };
});

app.MapPost("/saveproduct", (Product product) =>
{
    return product.Code + " - " + product.Name;
});


app.Run();


//Classe que vai representar nosso produto para efetuar os testes
public class Product
{
    public string? Code { get; set; }               //No dot net 6 coloca-se um ponto de interrogação para tirar
    public string? Name { get; set; }               //o erro de "não pode ser nulo".
}