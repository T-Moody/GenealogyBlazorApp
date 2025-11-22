# GitHub Copilot Repository Instructions

## Project Overview

- This is a **.NET 9 Blazor Web App (Blazor United / Auto render mode)**.
- Solution structure:
  - `GenealogyBlazorApp`          → Server/host project (root App, layouts, endpoints, infrastructure wiring)
  - `GenealogyBlazorApp.Client`   → Interactive Blazor components/pages (InteractiveAuto / InteractiveWebAssembly)
  - `GenealogyBlazorApp.Shared`   → Shared contracts (DTOs, results, basic abstractions, maybe shared enums)

- Purpose: A data-driven genealogy helper site with:
  - Public pages (home, tutorials, resources)
  - Admin-style pages for managing tutorials, resources, categories, homepage content

---

## Architecture Guidelines

### Server project: `GenealogyBlazorApp`

- Treat this as the **host + composition root**.
- Responsibilities:
  - Root component and layout shell:
    - `Components/App.razor`   → top-level router and render mode setup
    - `Components/Layout/MainLayout.razor`
    - `Components/Layout/NavMenu.razor` (and other shared layout components)
  - Minimal API endpoints, grouped by feature:
    - Example: `Features/Home/Endpoints`, `Features/Tutorials/Endpoints`, etc.
  - Infrastructure wiring:
    - Dependency injection
    - EF Core `AppDbContext`, migrations
    - Logging, configuration
- SSR-only / simple pages can live here under `Components/Pages` if they don’t need interactive client behavior.

### Client project: `GenealogyBlazorApp.Client`

- This is where **interactive Blazor content lives**.
- Use **Blazor United InteractiveAuto** (or other interactive render modes) on components/pages that need it.
- Organize code **by feature (vertical slice)**:
  - Example:
    - `Features/Home`
    - `Features/Tutorials`
    - `Features/Resources`
    - `Features/Categories`
- Inside a feature, prefer:
  - `Pages/`      → Blazor pages/components (e.g., `Index.razor`, `List.razor`, `Editor.razor`)
  - `Models/`     → View models, UI DTOs (or feature-local DTOs that map to Shared contracts)
  - `Services/`   → Feature-specific client-side services for calling server endpoints (optional)
- Interactive pages should:
  - Use `@rendermode="InteractiveAuto"` (or equivalent) where appropriate.
  - Call server endpoints defined in `GenealogyBlazorApp` and consume DTOs from `GenealogyBlazorApp.Shared`.

### Shared project: `GenealogyBlazorApp.Shared`

- Hold cross-project types:
  - DTOs used by both server and client (`HomeDto`, `TutorialDto`, `ResourceDto`, `CategoryDto`, etc.).
  - Common result wrappers, e.g. `ApiResult`, pagination models.
  - Shared enums or basic abstractions.
  - Shared services to handle the switch between SSR and Web Assembly (example: ISharedHomeService)
- Avoid putting business logic here; keep it for **contracts**, not behavior.

---

## Patterns & Practices

- Favor **Vertical Slice Architecture**:
  - Group by feature (Home, Tutorials, Resources, Categories), not by generic layers.
  - A “slice” spans:
    - Server: endpoints + handlers
    - Shared: DTOs/contracts
    - Client: interactive pages/components that consume those endpoints
- Use **MediatR-style commands/queries and handlers** (or similar pattern) on the server for application logic:
  - `Commands/`, `Queries/`, `Handlers/` inside a feature folder.
- Use **FluentValidation** for validating commands/queries on the server.
- EF Core:
  - Single `AppDbContext` for the app.
  - Helpful extension methods on `DbSet` / `IQueryable` where reuse is clear (e.g. `FindByIdAsync`, paging, filters).

---

## Coding Practices

- Use **clean code**:
  - Descriptive names
  - Small, focused methods and components
  - Avoid “god services” and giant multi-purpose classes
  - Code should be self-explanatory; minimal comments
  - One class per file
  - Only use var when return types are clear
  - Avoid deep nesting
  - Avoid using continue
  - Single purpose methods that describe the code
- Use **async/await** for all I/O.
- Prefer **constructor injection** for dependencies.
- Enable and respect **nullable reference types**.
- Use **record types** for DTOs and messages when appropriate.

---

## Blazor Guidelines

### Server (Layouts & Shell)

- `Components/App.razor`:
  - Hosts the router and sets up render modes.
- `Components/Layout/MainLayout.razor`, `NavMenu.razor`, etc.:
  - Define the global layout/shell (header, nav, footer, sidebar).
  - These live in the **server project**, even if the inner content is interactive.

### Client (Interactive Features)

- Put all interactive feature pages/components in `GenealogyBlazorApp.Client`.
- Use `@rendermode="InteractiveAuto"` (or equivalent) for interactive routes.
- Components:
  - One component per file.
  - `[Parameter]` properties with clear, explicit names.
  - Prefer async event handlers, e.g. `async Task OnSaveAsync()`.
- Styling:
  - Use a consistent styling approach (e.g., Bootstrap or utility classes).
  - Keep styling simple and maintainable.

---

## Testing

- Use **xUnit** for unit tests.
- Use **bUnit** for Blazor UI tests (Client).
- Organize tests by project and feature:
  - `tests/GenealogyBlazorApp.UnitTests/Features/...` (server-side handlers, validators, etc.)
  - `tests/GenealogyBlazorApp.Client.Tests/Features/...` (Blazor components/pages)
- When adding new behavior:
  1. Add or update tests (prefer TDD where reasonable).
  2. Implement or modify the feature (endpoints, handlers, pages).
  3. Run `dotnet test` and ensure everything passes.

---

## Development Workflow

- Typical workflow for a new feature (e.g. Home page slice):
  1. Define/update DTOs in `GenealogyBlazorApp.Shared`.
  2. Add server-side query/command, handler, and endpoint under `GenealogyBlazorApp/Features/<FeatureName>`.
  3. Add client-side page/components under `GenealogyBlazorApp.Client/Features/<FeatureName>`.
  4. Wire up the Blazor route and render mode.
  5. Add/update unit tests (handlers, validators) and bUnit tests (components).
  6. Run `dotnet build` and `dotnet test`.

- When in doubt:
  - Prefer **feature-based organization** over scattered layers.
  - Prefer **simplicity and clarity** over premature abstraction.
