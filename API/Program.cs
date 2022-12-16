//Estudo de DotNet 6.0
//
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//Endpoint simples com método GET
app.MapGet("/", () => "Hello World!");                              //Endpoint com método de acesso GET - Retorna "Hello world"


/*
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


//Passar informações por meio da URL duas maneiras

//api.app.com/users?datastart={date}&dataend={data}
//Através de Query parameter
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});


//api.app.com/users/{code}
//Através de rota
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;
});


//Enviando informações pelo Header
app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
}
);
*/


//==============================================================================================================
//
//CRUD básico com repositório em lista
//

//Verbo POST - Insrir produtos na lista
app.MapPost("/products", (Product product) =>
{
    ProductRepository.Add(product);                                        //Recebe um produto e grava na lista
    return Results.Created($"/products/{product.Code}", product.Code);     //Retornar o statusCode da operação
});

//Verbo GET - adquirir produto da lista no servidor
app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    if(product != null)
    {
        return Results.Ok(product);
    }
    else
    {
        return Results.NotFound();
    }
    
});

app.MapPut("/products", (Product product) => 
{
    var productSave = ProductRepository.GetBy(product.Code);
    productSave.Name = product.Name;
    return Results.Ok();
});

app.MapDelete("products/{code}", ([FromRoute] string code) => 
{
    var productSave = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSave);
    return Results.Ok();
});




app.Run();

public static class ProductRepository       //static para sobreviver, continuar existindo na memória
{
    public static List<Product>? Products { get; set; }     //Lista Products (Plural)

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new List<Product>();

        Products.Add(product);                          //Adicionando produto a nossa lista
    }

    public static Product GetBy(string? code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);                          //Removendo produto de nossa lista
    }
}


//Classe que vai representar nosso produto para efetuar os testes
public class Product        //Product no singular
{
    public string? Code { get; set; }               //No dot net 6 coloca-se um ponto de interrogação para tirar
    public string? Name { get; set; }               //o erro de "não pode ser nulo".
}