# Technical Requirements Document - Thumb of Michigan Genealogy

## Document Information
**Project Name**: Thumb of Michigan Genealogy  
**Document Version**: 1.0  
**Document Date**: November 21, 2025  
**Document Owner**: T-Moody  
**Status**: Phase 1 Technical Specifications  

---

## 1. Technology Stack Requirements

### 1.1 Framework and Runtime
- **Primary Framework**: .NET 10 (latest stable)
- **Web Framework**: Blazor United with Auto render mode
- **Target Framework**: net10.0
- **Runtime Environment**: ASP.NET Core 10
- **Language**: C# 13.0

### 1.2 Architecture Patterns
- **Primary Pattern**: Vertical Slice Architecture
- **Secondary Patterns**: 
  - CQRS (Command Query Responsibility Segregation) with MediatR
  - Repository pattern for data access abstraction
  - Dependency Injection container pattern
- **Code Organization**: Feature-based folders with .razor.cs code-behind files

### 1.3 Project Structure
```
Solution: GenealogyBlazorApp.sln
├── src/
│   ├── GenealogyBlazorApp/              # Server/Host project
│   │   ├── Components/                  # Server-side components
│   │   │   ├── App.razor               # Root application component
│   │   │   ├── Layout/                 # Layout components
│   │   │   └── Pages/                  # Server-rendered pages
│   │   ├── Features/                   # Vertical slices
│   │   │   ├── Home/                   
│   │   │   │   ├── Endpoints/          # API endpoints
│   │   │   │   ├── Handlers/           # Command/Query handlers
│   │   │   │   └── Models/             # Domain models
│   │   │   ├── Tutorials/
│   │   │   ├── Resources/
│   │   │   ├── Categories/
│   │   │   └── Counties/
│   │   ├── Infrastructure/             # Cross-cutting concerns
│   │   │   ├── Data/                   # DbContext, configurations
│   │   │   ├── Services/               # Shared services
│   │   │   └── Extensions/             # Extension methods
│   │   └── Program.cs                  # Application entry point
│   ├── GenealogyBlazorApp.Client/      # Interactive client components
│   │   ├── Features/                   # Feature-based organization
│   │   │   ├── Home/
│   │   │   │   ├── Pages/              # Interactive pages
│   │   │   │   ├── Components/         # Reusable components
│   │   │   │   └── Services/           # Client-side services
│   │   │   └── [Other Features]/
│   │   └── Program.cs                  # Client startup
│   └── GenealogyBlazorApp.Shared/      # Shared contracts
│       ├── DTOs/                       # Data transfer objects
│       ├── Models/                     # Shared models
│       ├── Services/                   # Shared service interfaces
│       └── Extensions/                 # Shared extension methods
└── tests/                              
    ├── GenealogyBlazorApp.Tests/       # Server-side unit tests
    ├── GenealogyBlazorApp.Client.Tests/# Client-side tests (bUnit)
    └── GenealogyBlazorApp.IntegrationTests/ # End-to-end tests
```

---

## 2. Infrastructure Requirements

### 2.1 Database Requirements
- **Primary Database**: SQL Server 2022 (production) / SQLite (development)
- **ORM**: Entity Framework Core 10
- **Migration Strategy**: Code-first with automatic migrations in development
- **Connection Pooling**: Enabled with optimized pool size
- **Backup Strategy**: Automated daily backups with 30-day retention

### 2.2 Authentication and Authorization
- **Authentication Provider**: ASP.NET Core Identity
- **Session Management**: Cookie-based authentication with secure configuration
- **Authorization**: Policy-based authorization with role claims
- **Password Requirements**: 
  - Minimum 8 characters
  - Mixed case, numbers, special characters
  - Password strength validation

### 2.3 File Storage
- **Development**: Local file system storage
- **Production**: Azure Blob Storage or AWS S3 (cloud-ready)
- **Image Processing**: ImageSharp for resize/optimization
- **Supported Formats**: JPEG, PNG, WebP for images

### 2.4 Caching Strategy
- **Memory Caching**: In-memory cache for frequently accessed data
- **Distributed Caching**: Redis for production scalability
- **Response Caching**: HTTP response caching for static content
- **Cache Invalidation**: Event-driven cache invalidation

---

## 3. Component Architecture

### 3.1 Blazor Component Requirements
- **Render Modes**: 
  - InteractiveAuto for dynamic components
  - Static Server Side Rendering for content pages
  - InteractiveWebAssembly for complex client-side interactions
- **Component Structure**: All components use .razor.cs code-behind files
- **State Management**: Component-level state with optional shared state service
- **Event Handling**: Async event handlers throughout

### 3.2 Layout Architecture
```
App.razor (Root)
├── MainLayout.razor (Public site layout)
│   ├── NavMenu.razor
│   ├── Header.razor
│   ├── Sidebar.razor
│   └── Footer.razor
└── AdminLayout.razor (Admin area layout)
    ├── AdminNav.razor
    ├── AdminSidebar.razor
    └── AdminBreadcrumb.razor
```

### 3.3 Styling and UI Framework
- **CSS Framework**: Bootstrap 5.3+ (CDN or local)
- **Custom CSS**: Minimal custom styles, prefer Bootstrap utility classes
- **Icons**: Bootstrap Icons or Font Awesome
- **Responsive Design**: Mobile-first approach
- **Theme**: Bootstrap default theme with custom color variables

---

## 4. API and Endpoint Design

### 4.1 API Architecture
- **API Style**: Minimal APIs with endpoint mapping
- **Endpoint Organization**: Feature-based endpoint grouping
- **Request/Response**: DTOs for all external contracts
- **Validation**: FluentValidation for request validation
- **Error Handling**: Global exception handling with structured error responses

### 4.2 Endpoint Examples
```csharp
// Feature-based endpoint registration
app.MapGroup("/api/tutorials")
   .MapTutorialEndpoints()
   .RequireAuthorization("Admin");

app.MapGroup("/api/resources")
   .MapResourceEndpoints()
   .RequireAuthorization("Admin");

// Public endpoints
app.MapGroup("/api/public")
   .MapPublicEndpoints()
   .AllowAnonymous();
```

### 4.3 Data Transfer Objects (DTOs)
```csharp
// Example DTO structure
public record TutorialDto(
    int Id,
    string Title,
    string Description,
    string Content,
    int CategoryId,
    string? YouTubeUrl,
    bool IsPublished,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateTutorialCommand(
    string Title,
    string Description,
    string Content,
    int CategoryId,
    string? YouTubeUrl
);
```

---

## 5. Data Layer Architecture

### 5.1 Entity Framework Configuration
```csharp
// DbContext structure
public class AppDbContext : DbContext
{
    public DbSet<Tutorial> Tutorials { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<User> Users { get; set; }
    
    // Configuration through fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
```

### 5.2 Entity Models
```csharp
// Example entity structure
public class Tutorial : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? YouTubeUrl { get; set; }
    public bool IsPublished { get; set; }
    
    // Navigation properties
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
}

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
}
```

### 5.3 Repository Pattern Implementation
```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

// Feature-specific repositories
public interface ITutorialRepository : IRepository<Tutorial>
{
    Task<IEnumerable<Tutorial>> GetByCategory(int categoryId);
    Task<IEnumerable<Tutorial>> GetPublishedAsync();
    Task<Tutorial?> GetBySlugAsync(string slug);
}
```

---

## 6. Security Requirements

### 6.1 Authentication Implementation
```csharp
// Program.cs authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });
```

### 6.2 Authorization Policies
```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => 
        policy.RequireAuthenticatedUser()
              .RequireClaim("Role", "Admin"));
              
    options.AddPolicy("ContentManager", policy =>
        policy.RequireAuthenticatedUser()
              .RequireClaim("Role", "Admin", "ContentManager"));
});
```

### 6.3 Input Validation and Security
```csharp
// FluentValidation example
public class CreateTutorialValidator : AbstractValidator<CreateTutorialCommand>
{
    public CreateTutorialValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200)
            .Must(BeValidTitle);
            
        RuleFor(x => x.YouTubeUrl)
            .Must(BeValidYouTubeUrl)
            .When(x => !string.IsNullOrEmpty(x.YouTubeUrl));
    }
}
```

---

## 7. Performance and Scalability

### 7.1 Performance Targets
- **Page Load Time**: <3 seconds (first contentful paint)
- **API Response Time**: <500ms for CRUD operations
- **Database Query Time**: <100ms for simple queries, <500ms for complex
- **Memory Usage**: <512MB baseline, <2GB under load
- **Concurrent Users**: Support 100+ concurrent users

### 7.2 Optimization Strategies
```csharp
// Query optimization examples
public async Task<IEnumerable<TutorialDto>> GetTutorialsAsync()
{
    return await _context.Tutorials
        .Where(t => t.IsPublished)
        .Include(t => t.Category)
        .OrderByDescending(t => t.CreatedAt)
        .Select(t => new TutorialDto(
            t.Id,
            t.Title,
            t.Description,
            t.Content,
            t.CategoryId,
            t.YouTubeUrl,
            t.IsPublished,
            t.CreatedAt,
            t.UpdatedAt
        ))
        .ToListAsync();
}
```

### 7.3 Caching Implementation
```csharp
// Service with caching
public class CachedTutorialService : ITutorialService
{
    private readonly ITutorialService _tutorialService;
    private readonly IMemoryCache _cache;
    
    public async Task<IEnumerable<TutorialDto>> GetPublishedTutorialsAsync()
    {
        return await _cache.GetOrCreateAsync("published_tutorials", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return await _tutorialService.GetPublishedTutorialsAsync();
        });
    }
}
```

---

## 8. Testing Requirements

### 8.1 Unit Testing Framework
- **Framework**: xUnit with FluentAssertions
- **Mocking**: Moq or NSubstitute
- **Coverage Target**: >80% code coverage
- **Test Organization**: Mirror source structure in test projects

### 8.2 Integration Testing
- **Framework**: ASP.NET Core TestHost
- **Database**: In-memory database for testing
- **Authentication**: Test authentication services
- **API Testing**: End-to-end API testing

### 8.3 UI Testing
- **Framework**: bUnit for Blazor component testing
- **Rendering Tests**: Component rendering validation
- **Interaction Tests**: User interaction simulation
- **Visual Testing**: Component visual regression testing

### 8.4 Test Examples
```csharp
// Unit test example
public class TutorialServiceTests
{
    [Fact]
    public async Task GetTutorialAsync_WithValidId_ReturnsTutorial()
    {
        // Arrange
        var mockRepo = new Mock<ITutorialRepository>();
        var tutorial = new Tutorial { Id = 1, Title = "Test" };
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tutorial);
        
        var service = new TutorialService(mockRepo.Object);
        
        // Act
        var result = await service.GetTutorialAsync(1);
        
        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test");
    }
}

// bUnit component test example
public class TutorialCardTests : TestContext
{
    [Fact]
    public void TutorialCard_RendersTitle()
    {
        // Arrange
        var tutorial = new TutorialDto(1, "Test Title", "Description", 
            "Content", 1, null, true, DateTime.Now, DateTime.Now);
        
        // Act
        var component = RenderComponent<TutorialCard>(parameters => 
            parameters.Add(p => p.Tutorial, tutorial));
        
        // Assert
        component.Find("h3").TextContent.Should().Be("Test Title");
    }
}
```

---

## 9. Deployment and Infrastructure

### 9.1 Development Environment
- **IDE**: Visual Studio 2022 or VS Code with C# extensions
- **Database**: SQLite for local development
- **Web Server**: Kestrel development server
- **Package Management**: NuGet with package.lock.json

### 9.2 Production Environment
- **Hosting**: Azure App Service or AWS Elastic Beanstalk
- **Database**: SQL Server (Azure SQL or AWS RDS)
- **CDN**: Azure CDN or CloudFront for static assets
- **SSL**: Automatic certificate management (Let's Encrypt)

### 9.3 CI/CD Pipeline
```yaml
# Example GitHub Actions workflow
name: Build and Deploy
on:
  push:
    branches: [main]
    
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '10.0.x'
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Test
        run: dotnet test --no-build --verbosity normal
        
  deploy:
    needs: test
    runs-on: ubuntu-latest
    steps:
      - name: Deploy to production
        run: # Deployment steps
```

---

## 10. Monitoring and Logging

### 10.1 Logging Configuration
```csharp
// Program.cs logging setup
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsProduction())
{
    builder.Logging.AddApplicationInsights();
}
```

### 10.2 Health Checks
```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>()
    .AddUrlGroup(new Uri("https://youtube.com"), "YouTube")
    .AddCheck<LinkValidationHealthCheck>("external-links");
```

### 10.3 Application Insights (Production)
- **Performance Monitoring**: Response times, throughput
- **Error Tracking**: Exception tracking and alerting
- **User Analytics**: Page views, user flows
- **Custom Metrics**: Business-specific metrics

---

## 11. Phase Gate Requirements

### Phase 1 → Phase 2 Technical Checklist
- [x] Technology stack finalized and documented
- [x] Architecture patterns selected and documented
- [x] Database design requirements specified
- [x] Security requirements defined
- [x] Performance targets established
- [x] Testing strategy outlined
- [x] Deployment strategy planned
- [ ] Development environment setup completed
- [ ] Initial project scaffolding created
- [ ] Core infrastructure implemented

---

**Document Status**: Complete for Phase 1  
**Next Phase**: Architecture Implementation and Core Development  
**Technical Lead Approval**: Required before Phase 2 initiation