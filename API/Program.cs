//Estudo de DotNet 6.0
//
using Microsoft.AspNetCore.Mvc;

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

//Passar informa��es por meio da URL duas maneiras

//api.app.com/users?datastart={date}&dataend={data}
//Atrav�s de Query parameter
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});

//api.app.com/users/{code}
//Atrav�s de rota
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;
});


//Enviando informa��es pelo Header
app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
}
);

app.Run();

public static class ProductRepository       //static para sobreviver, continuar existindo na mem�ria
{
    public static List<Product>? Products { get; set; }     //Lista Products (Plural)

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
        return Products.First(p => p.Code == code);
    }
}


//Classe que vai representar nosso produto para efetuar os testes
public class Product        //Product no singular
{
    public string? Code { get; set; }               //No dot net 6 coloca-se um ponto de interroga��o para tirar
    public string? Name { get; set; }               //o erro de "n�o pode ser nulo".
}