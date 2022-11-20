var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Endpoint simples com m�todo GET
app.MapGet("/", () => "Hello World!");                              //Endpoint com m�todo de acesso GET - Retorna "Hello world"

//Endpoint simples com m�todo POST e enviando um objeto. Ser� automaticamente convertido para Json
app.MapPost("/", () => new {Name = "Jose Adriano", Age = 58});      //Endpoint com m�todo de acesso POST com um objeto - Retorna meu nome

//Endpoint com fun��o l�mbda com mais de uma linha
app.MapGet("/AddHeader", (HttpResponse response) =>                 //Endpoint com m�todo de acesso GET - Altera o cabe�alho e retorna un
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
    public string? Code { get; set; }               //No dot net 6 coloca-se um ponto de interroga��o para tirar
    public string? Name { get; set; }               //o erro de "n�o pode ser nulo".
}