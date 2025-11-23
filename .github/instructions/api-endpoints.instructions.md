# Minimal API Development Instructions

## API Endpoint Patterns

### Endpoint Structure

All Minimal API endpoints should be defined in the `Features` folder, grouped by feature. Each feature's endpoints should be in a static class that extends `IEndpointRouteBuilder`.

```csharp
// In /Features/Concepts/ConceptEndpoints.cs

public static class ConceptEndpoints
{
    public static void MapConceptEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/concepts").WithTags("Concepts");

        // Define endpoints within this group
        group.MapGet("/", GetConcepts);
        group.MapGet("/{id}", GetConceptById);
        group.MapPost("/", CreateConcept);
        group.MapPut("/{id}", UpdateConcept);
        group.MapDelete("/{id}", DeleteConcept);
    }

    // Handlers are static methods within the same class
    private static async Task<IResult> GetConcepts(IMediator mediator)
    {
        var concepts = await mediator.Send(new GetAllConceptsQuery());
        return Results.Ok(concepts);
    }
    
    // ... other handlers
}
```

### Registering Endpoints

In `Program.cs`, map the feature endpoints:

```csharp
// In Program.cs
app.MapConceptEndpoints();
```

### HTTP Method Patterns

#### GET Endpoints

Handlers should be `async` and use MediatR to fetch data.

```csharp
private static async Task<IResult> GetConcepts(IMediator mediator)
{
    var query = new GetAllConceptsQuery();
    var result = await mediator.Send(query);
    return Results.Ok(result);
}

private static async Task<IResult> GetConceptById(int id, IMediator mediator)
{
    var query = new GetConceptByIdQuery(id);
    var result = await mediator.Send(query);
    
    return result is not null ? Results.Ok(result) : Results.NotFound();
}
```

#### POST Endpoints

Use a command and return a `201 Created` response.

```csharp
private static async Task<IResult> CreateConcept(
    [FromBody] CreateConceptCommand command, 
    IMediator mediator)
{
    var result = await mediator.Send(command);
    
    // Assuming result contains the created DTO with an ID
    return Results.CreatedAtRoute("GetConceptById", new { id = result.Id }, result);
}
```
*Note: You must name the `GET` by ID endpoint using `.WithName("GetConceptById")` for `Results.CreatedAtRoute` to work.*

#### PUT Endpoints

Return `204 No Content` for successful updates.

```csharp
private static async Task<IResult> UpdateConcept(
    int id, 
    [FromBody] UpdateConceptRequest request, 
    IMediator mediator)
{
    if (id != request.Id)
    {
        return Results.BadRequest("ID mismatch");
    }

    var command = new UpdateConceptCommand(request);
    await mediator.Send(command);
    
    return Results.NoContent();
}
```

#### DELETE Endpoints

Return `204 No Content`.

```csharp
private static async Task<IResult> DeleteConcept(int id, IMediator mediator)
{
    var command = new DeleteConceptCommand(id);
    await mediator.Send(command);
    
    return Results.NoContent();
}
```

### Authorization Patterns

Use extension methods on the endpoint or group.

```csharp
var group = app.MapGroup("/api/admin")
               .WithTags("Admin")
               .RequireAuthorization("AdminPolicy");

group.MapPost("/", ...); // This endpoint now requires the "AdminPolicy"

// Endpoint-specific authorization
app.MapDelete("/api/concepts/{id}", ...)
   .RequireAuthorization(policy => policy.RequireRole("Admin"));
```

### Request/Response DTOs & Validation

- Use **FluentValidation** with MediatR pipelines for validating commands/queries.
- DTOs and commands/queries live in the `Shared` project or within the server-side feature folder.

```csharp
// Example: A command in a feature folder
public record CreateConceptCommand(string Name, string? Description) : IRequest<ConceptDto>;

// Its validator
public class CreateConceptCommandValidator : AbstractValidator<CreateConceptCommand>
{
    public CreateConceptCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
```

### Error Handling

- A MediatR pipeline behavior should handle exceptions and validation failures globally.
- Minimal API endpoints should remain clean of `try-catch` blocks.
- The pipeline translates exceptions into appropriate `IResult` types (e.g., `400 Bad Request`, `500 Internal Server Error`).

### API Testing

Use `WebApplicationFactory` for integration tests.

```csharp
public class ConceptsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ConceptsApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetConcepts_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/concepts");

        // Assert
        response.EnsureSuccessStatusCode();
        var concepts = await response.Content.ReadFromJsonAsync<List<ConceptDto>>();
        concepts.Should().NotBeNull();
    }
}
```

### OpenAPI/Swagger Integration

- Use `.WithTags()` to group endpoints in the Swagger UI.
- Use `.WithName()` to provide unique IDs for endpoints, especially for `CreatedAtRoute`.
- Use `.Produces()` to document response types and status codes.

```csharp
group.MapGet("/{id}", GetConceptById)
     .WithName("GetConceptById")
     .Produces<ConceptDto>(200)
     .Produces(404);
```
