# API Development Instructions

## API Controller Patterns

### Controller Structure

All API controllers in `/Controllers/` should follow these patterns:

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ConceptsController : ControllerBase
{
    private readonly IConceptService conceptService;
    private readonly ILogger<ConceptsController> logger;
    private readonly IHubContext<OntologyHub> hubContext;

    public ConceptsController(
        IConceptService conceptService,
        ILogger<ConceptsController> logger,
        IHubContext<OntologyHub> hubContext)
    {
        this.conceptService = conceptService;
        this.logger = logger;
        this.hubContext = hubContext;
    }
}
```

### HTTP Method Patterns

#### GET Endpoints

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<ConceptDto>>> GetConcepts()
{
    try
    {
        var concepts = await conceptService.GetAllAsync();
        return Ok(concepts.Select(c => c.ToDto()));
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error retrieving concepts");
        return StatusCode(500, "An error occurred while retrieving concepts");
    }
}

[HttpGet("{id}")]
public async Task<ActionResult<ConceptDto>> GetConcept(int id)
{
    var concept = await conceptService.GetByIdAsync(id);

    if (concept == null)
        return NotFound($"Concept with ID {id} not found");

    return Ok(concept.ToDto());
}
```

#### POST Endpoints

```csharp
[HttpPost]
public async Task<ActionResult<ConceptDto>> CreateConcept([FromBody] CreateConceptRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    try
    {
        var concept = await conceptService.CreateAsync(request.ToEntity());

        // Notify connected clients via SignalR
        await hubContext.Clients.All.SendAsync("ConceptCreated", concept.ToDto());

        return CreatedAtAction(nameof(GetConcept), new { id = concept.Id }, concept.ToDto());
    }
    catch (ValidationException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error creating concept");
        return StatusCode(500, "An error occurred while creating the concept");
    }
}
```

#### PUT Endpoints

```csharp
[HttpPut("{id}")]
public async Task<IActionResult> UpdateConcept(int id, [FromBody] UpdateConceptRequest request)
{
    if (id != request.Id)
        return BadRequest("ID mismatch");

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    try
    {
        var concept = await conceptService.UpdateAsync(request.ToEntity());

        if (concept == null)
            return NotFound($"Concept with ID {id} not found");

        // Notify connected clients
        await hubContext.Clients.All.SendAsync("ConceptUpdated", concept.ToDto());

        return Ok(concept.ToDto());
    }
    catch (ValidationException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error updating concept with ID {ConceptId}", id);
        return StatusCode(500, "An error occurred while updating the concept");
    }
}
```

#### DELETE Endpoints

```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteConcept(int id)
{
    try
    {
        var success = await conceptService.DeleteAsync(id);

        if (!success)
            return NotFound($"Concept with ID {id} not found");

        // Notify connected clients
        await hubContext.Clients.All.SendAsync("ConceptDeleted", id);

        return NoContent();
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error deleting concept with ID {ConceptId}", id);
        return StatusCode(500, "An error occurred while deleting the concept");
    }
}
```

### Authorization Patterns

#### Role-Based Authorization

```csharp
[HttpPost]
[Authorize(Roles = "Admin")]
public async Task<ActionResult> AdminOnlyEndpoint()
{
    // Implementation
}

[HttpDelete("{id}")]
[Authorize(Roles = "Admin,User")]
public async Task<ActionResult> DeleteResource(int id)
{
    // Check if user owns the resource
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var resource = await service.GetByIdAsync(id);

    if (resource.UserId != userId && !User.IsInRole("Admin"))
        return Forbid();

    // Proceed with deletion
}
```

### Request/Response DTOs

#### Request Models

```csharp
public class CreateConceptRequest
{
    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public Concept ToEntity()
    {
        return new Concept
        {
            Name = Name,
            Description = Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
```

#### Response Models

```csharp
public class ConceptDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RelationshipCount { get; set; }
}

// Extension method for mapping
public static class ConceptExtensions
{
    public static ConceptDto ToDto(this Concept concept)
    {
        return new ConceptDto
        {
            Id = concept.Id,
            Name = concept.Name,
            Description = concept.Description,
            CreatedAt = concept.CreatedAt,
            UpdatedAt = concept.UpdatedAt,
            UserName = concept.User?.UserName ?? "Unknown",
            RelationshipCount = concept.Relationships?.Count ?? 0
        };
    }
}
```

### Error Handling

#### Global Exception Handling

Controllers should rely on global exception middleware but can handle specific cases:

```csharp
try
{
    // API operation
}
catch (ValidationException ex)
{
    return BadRequest(ex.Message);
}
catch (UnauthorizedAccessException)
{
    return Forbid();
}
catch (Exception ex)
{
    logger.LogError(ex, "Unexpected error in {Action}", nameof(ActionName));
    return StatusCode(500, "An unexpected error occurred");
}
```

### SignalR Integration

#### Real-time Notifications

```csharp
// After successful operations, notify connected clients
await hubContext.Clients.All.SendAsync("ConceptCreated", conceptDto);
await hubContext.Clients.All.SendAsync("ConceptUpdated", conceptDto);
await hubContext.Clients.All.SendAsync("ConceptDeleted", conceptId);

// For user-specific notifications
await hubContext.Clients.User(userId).SendAsync("NotificationForUser", message);
```

### API Testing

#### Integration Tests

```csharp
public class ConceptsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public ConceptsControllerTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        this.client = factory.CreateClient();
    }

    [Fact]
    public async Task GetConcepts_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await client.GetAsync("/api/concepts");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var concepts = JsonSerializer.Deserialize<ConceptDto[]>(content);
        concepts.Should().NotBeNull();
    }
}
```

## API Documentation

### OpenAPI/Swagger Integration

- Use XML documentation comments for all public API methods
- Include examples in documentation
- Document all possible response codes
- Use appropriate HTTP status codes consistently

### Response Standards

- **200 OK**: Successful GET requests
- **201 Created**: Successful POST requests (include Location header)
- **204 No Content**: Successful DELETE requests
- **400 Bad Request**: Validation errors, malformed requests
- **401 Unauthorized**: Authentication required
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Unexpected server errors
