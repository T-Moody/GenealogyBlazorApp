# GitHub Copilot Custom Instructions - Eidos Ontology Builder

## Project Overview

**Eidos** is a Blazor-based ontology and knowledge graph builder using Entity Framework Core, ASP.NET Core, and SignalR. The application enables collaborative ontology editing with real-time features, comprehensive CRUD operations, and export capabilities.

## Development Approach

### Agent-Based Development System

This project uses a **three-tier orchestration system** for coordinated development:

#### 1. Project Level

- **project-orchestrator**: Manages entire project lifecycle, cross-feature dependencies, and stakeholder communication

#### 2. Feature Level

- **feature-orchestrator**: Coordinates individual features from planning through deployment

#### 3. Workflow Level

- **01-requirements-agent**: Requirements analysis and validation
- **02-planning-agent**: Implementation planning and task breakdown
- **03-development-agent**: Development coordination and integration
- **04-testing-agent**: Testing orchestration and quality validation
- **05-review-agent**: Review coordination and quality gates
- **06-documentation-agent**: Documentation coordination and publication

#### 4. Specialist Level

- **backend-architect**: System architecture and API design
- **dotnet-data-specialist**: Data access and Entity Framework
- **blazor-developer**: Blazor components and UI implementation
- **dotnet-security-specialist**: Security implementation and validation
- **blazor-accessibility-performance-specialist**: Accessibility and performance optimization
- **supportability-lifecycle-specialist**: Operations, monitoring, and lifecycle management
- **documentation-specialist**: Technical writing and documentation
- **ui-design-specialist**: UI/UX design and styling
- **requirements-architect**: Requirements analysis and business logic

## Architecture & Technology Stack

### Core Technologies

- **Backend**: ASP.NET Core 8.0, Entity Framework Core, SignalR
- **Frontend**: Blazor Server with Bootstrap 5, Chart.js, Font Awesome
- **Database**: SQL Server (production), SQLite (development)
- **Authentication**: ASP.NET Core Identity with role-based authorization
- **Real-time**: SignalR for collaborative editing and presence indicators
- **Testing**: xUnit, FluentAssertions (157+ tests, 100% passing)

### Key Patterns

- **Repository Pattern**: For data access abstraction
- **Service Layer**: Business logic separation with interfaces
- **Component-Based Architecture**: Reusable Blazor components
- **Real-time Collaboration**: SignalR hubs for live updates
- **Responsive Design**: Mobile-first Bootstrap implementation
- **Role-Based Security**: Admin/User roles with feature-level permissions

## Development Standards

### Code Style

- **C# Conventions**: PascalCase for public members, camelCase for private
- **Component Structure**: One component per file, clear separation of concerns
- **Dependency Injection**: Constructor injection, interface-based services
- **Error Handling**: Custom exceptions with context, global exception middleware
- **Logging**: Structured logging with ILogger, correlation IDs for tracking
- **Testing**: Unit tests for business logic, integration tests for data layer

### Database Patterns

- **Entity Conventions**: PascalCase properties, Id suffix for primary keys
- **Relationships**: Explicit foreign key properties, navigation properties
- **Migrations**: Descriptive names, include sample data where appropriate
- **Indexes**: Performance-critical queries have appropriate indexes
- **Constraints**: Use data annotations and Fluent API for validation

### Blazor Component Guidelines

- **State Management**: Use cascading values for shared state
- **Event Handling**: Prefer async event handlers
- **Parameter Validation**: Validate component parameters
- **Lifecycle**: Proper disposal of resources and event subscriptions
- **CSS**: Component-scoped CSS where possible

## Key Features & Functionality

### Core Features

1. **Concept Management**: CRUD operations with hierarchical relationships
2. **Relationship Management**: Define and visualize concept relationships
3. **Real-time Collaboration**: Live editing with user presence indicators
4. **Export Functionality**: Multiple format support (RDF, JSON-LD, etc.)
5. **Search & Navigation**: Full-text search with advanced filtering
6. **User Management**: Registration, authentication, role-based access
7. **Audit Trail**: Complete change tracking and history
8. **Responsive UI**: Mobile-friendly design with accessibility support
9. **Batch Operations**: Bulk concept and relationship management
10. **Performance Optimization**: Caching, lazy loading, pagination

### Database Schema (Key Entities)

```csharp
- Concept: Id, Name, Description, CreatedAt, UpdatedAt, UserId
- Relationship: Id, ConceptId, RelatedConceptId, RelationshipType, UserId
- User: AspNetCore Identity with custom properties
- UserPresence: Real-time user activity tracking
- AuditLog: Change tracking and history
```

## Development Workflow

### Development Ledger Integration

- **Location**: `/DEVELOPMENT_LEDGER.md` (project root)
- **Purpose**: Single source of truth for project evolution
- **Update Frequency**: After each significant change or decision
- **Sections**: Current Status, Decision Log, Feature Log, Technical Debt, Architecture Evolution

### Quality Standards

- **Definition of Done**: Tests pass, code reviewed, documentation updated, ledger updated
- **Code Review**: Minimum 1 approval, all checks pass, link to work item
- **Testing Requirements**: Unit tests for business logic, integration tests for data operations
- **Security**: Input validation, authorization checks, SQL injection prevention
- **Performance**: Response times < 200ms, efficient queries, appropriate caching

## File Organization

### Project Structure

```
eidos/
├── Components/           # Blazor components
│   ├── Account/         # Authentication components
│   ├── Layout/          # Layout components
│   ├── Ontology/        # Core ontology components
│   ├── Pages/           # Page components
│   └── Shared/          # Shared UI components
├── Data/                # Data access layer
│   └── Repositories/    # Repository implementations
├── Models/              # Entity models and DTOs
├── Services/            # Business logic services
├── Hubs/               # SignalR hubs
├── Middleware/         # Custom middleware
├── wwwroot/            # Static assets
└── DEVELOPMENT_LEDGER.md
```

## When Working on This Project

### Always Consider

1. **Real-time Impact**: How changes affect SignalR functionality
2. **Performance**: Database query efficiency and response times
3. **Security**: Authorization, input validation, data protection
4. **Accessibility**: Screen readers, keyboard navigation, ARIA labels
5. **Mobile Experience**: Responsive design and touch interactions
6. **Testing**: Add appropriate test coverage for new functionality

### Before Making Changes

1. Check **DEVELOPMENT_LEDGER.md** for context and recent decisions
2. Understand existing patterns and conventions
3. Consider impact on real-time collaboration features
4. Verify changes don't break existing functionality
5. Plan appropriate test coverage

### After Making Changes

1. Update **DEVELOPMENT_LEDGER.md** with decisions and changes
2. Ensure tests pass and add new tests as needed
3. Update documentation if public APIs changed
4. Consider performance impact and optimization opportunities

## Common Patterns to Follow

### API Endpoints

```csharp
[Authorize]
[Route("api/[controller]")]
public class ConceptsController : ControllerBase
{
    // Standard CRUD pattern with proper HTTP methods
    // Include authorization attributes
    // Return appropriate HTTP status codes
    // Handle exceptions gracefully
}
```

### Service Classes

```csharp
public class ConceptService : IConceptService
{
    // Constructor injection for dependencies
    // Async methods where appropriate
    // Proper exception handling
    // Logging for important operations
}
```

### Blazor Components

```razor
@* Clear component structure *@
@* Proper parameter validation *@
@* Async event handlers *@
@* Component-scoped CSS when possible *@
```

## Integration Points

### External Systems

- **None currently**: Self-contained application
- **Future**: Consider integration patterns when adding external dependencies

### Key Services

- **ConceptService**: Core business logic for concept management
- **RelationshipService**: Handles concept relationships
- **UserPresenceService**: Manages real-time user presence
- **ExportService**: Handles various export formats

## Testing Strategy

### Test Types

- **Unit Tests**: Business logic in services
- **Integration Tests**: Database operations and repositories
- **Component Tests**: Blazor component functionality
- **End-to-End Tests**: Critical user workflows

### Test Patterns

- Arrange-Act-Assert pattern
- Use FluentAssertions for readable assertions
- Mock external dependencies
- Test both success and failure scenarios

This context will help Copilot understand the project structure, patterns, and development approach to provide more relevant and consistent suggestions.
