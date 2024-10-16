var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

app.MapGet("/", () => "Hello this is the HomePages");

//GET: /api/categories => Read categories.
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories);
});

//POST: /api/categories => Create categories.
app.MapPost("/api/categories", () =>
{
    var newCategory = new Category
    {
        CategoryId = Guid.Parse("12345678"),
        Name = "Electronics",
        Description = "Devices and Gadgets including phone",
        CreateAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

//DELETE: /api/categories => Delete categories.
app.MapDelete("/api/categories", () =>
{
    var foundCategory=categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse("12345678"));
    if (foundCategory == null)
    {
        return Results.NotFound("Category doesn't found for delete");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});

//PUT: /api/categories => Update a categories.
app.MapPut("/api/categories", () =>
{
    var foundCategory=categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse("12345678"));
    if (foundCategory == null)
    {
        return Results.NotFound("Category doesn't found");
    }
    foundCategory.Name ="Smart Phone";
    foundCategory.Description = "This is the update smart phone";
    return Results.NoContent();
});
app.Run();
public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; }

};