var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello this is the HomePages");

var products = new List<Product>()
{
    new Product("Samsung s15", 15250),
    new Product("Redmi 8A",12450),
};

app.MapGet("/products",()=>
{
    return Results.Ok(products);
});

app.Run();
public record Product(string Name, decimal Price);