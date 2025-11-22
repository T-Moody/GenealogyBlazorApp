# Data Layer Development Instructions

## Vertical Slice Architecture - Data Access Patterns

### Direct DbContext Usage in Handlers

This project uses **Vertical Slice Architecture**. Data access is handled directly in MediatR handlers, NOT through repository patterns:

```csharp
public class GetHomeContentQueryHandler : IRequestHandler<GetHomeContentQuery, HomeContentDto?>
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetHomeContentQueryHandler> _logger;

    public GetHomeContentQueryHandler(AppDbContext context, ILogger<GetHomeContentQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HomeContentDto?> Handle(GetHomeContentQuery request, CancellationToken cancellationToken)
    {
        return await _context.HomeContent
            .AsNoTracking() // Use for read-only operations
            .Where(h => h.IsActive)
            .Select(h => new HomeContentDto
            {
                Id = h.Id,
                SiteTitle = h.SiteTitle,
                Tagline = h.Tagline,
                AboutContent = h.AboutContent,
                // ... other mappings
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
```

### Entity Conventions

#### Model Structure

```csharp
public class EntityName
{
    public int Id { get; set; } // Primary key with Id suffix

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key properties (explicit)
    public int UserId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<RelatedEntity> RelatedEntities { get; set; } = new List<RelatedEntity>();
}
```

#### Fluent API Configuration

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Concept>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
        entity.HasIndex(e => e.Name); // Add indexes for performance

        entity.HasOne(e => e.User)
              .WithMany(u => u.Concepts)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);
    });
}
```

### Migration Guidelines

#### Creating Migrations

```bash
# Use descriptive names
dotnet ef migrations add AddConceptHierarchySupport
dotnet ef migrations add UpdateRelationshipConstraints
```

#### Migration Structure

```csharp
public partial class AddConceptHierarchySupport : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Forward migration
        // Include sample data if appropriate
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Rollback migration
        // Ensure data safety
    }
}
```

### Query Performance Patterns

#### Efficient Queries

```csharp
// Good: Use projection for large datasets
public async Task<IEnumerable<ConceptSummary>> GetConceptSummariesAsync()
{
    return await context.Concepts
        .AsNoTracking()
        .Select(c => new ConceptSummary
        {
            Id = c.Id,
            Name = c.Name,
            RelationshipCount = c.Relationships.Count()
        })
        .ToListAsync();
}

// Good: Include related data efficiently
public async Task<Concept?> GetConceptWithRelationshipsAsync(int id)
{
    return await context.Concepts
        .Include(c => c.Relationships)
            .ThenInclude(r => r.RelatedConcept)
        .FirstOrDefaultAsync(c => c.Id == id);
}
```

#### Avoid N+1 Problems

```csharp
// Bad: Will cause N+1 queries
var concepts = await context.Concepts.ToListAsync();
foreach (var concept in concepts)
{
    var relationshipCount = concept.Relationships.Count(); // Lazy loading
}

// Good: Load relationships in single query
var concepts = await context.Concepts
    .Include(c => c.Relationships)
    .ToListAsync();
```

### Transaction Handling

#### Service Layer Transactions

```csharp
public async Task<bool> CreateConceptWithRelationshipsAsync(Concept concept, List<Relationship> relationships)
{
    using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
        await context.Concepts.AddAsync(concept);
        await context.SaveChangesAsync();

        // Use the generated concept ID
        foreach (var relationship in relationships)
        {
            relationship.ConceptId = concept.Id;
        }

        await context.Relationships.AddRangeAsync(relationships);
        await context.SaveChangesAsync();

        await transaction.CommitAsync();
        return true;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to create concept with relationships");
        await transaction.RollbackAsync();
        return false;
    }
}
```

### Testing Data Layer

#### Repository Tests

```csharp
public class ConceptRepositoryTests : IDisposable
{
    private readonly OntologyDbContext context;
    private readonly ConceptRepository repository;

    public ConceptRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<OntologyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new OntologyDbContext(options);
        repository = new ConceptRepository(context, Mock.Of<ILogger<ConceptRepository>>());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllConcepts_OrderedByName()
    {
        // Arrange
        await SeedTestDataAsync();

        // Act
        var concepts = await repository.GetAllAsync();

        // Assert
        concepts.Should().HaveCount(3);
        concepts.First().Name.Should().Be("Alpha");
    }
}
```

## Database Performance Guidelines

### Indexing Strategy

- Add indexes for frequently queried columns
- Consider composite indexes for multi-column queries
- Monitor query execution plans in production

### Connection Management

- Use connection pooling (configured automatically)
- Avoid long-running connections
- Use `AsNoTracking()` for read-only operations

### Data Validation

- Use data annotations for basic validation
- Implement custom validators for complex business rules
- Validate at both client and server side
