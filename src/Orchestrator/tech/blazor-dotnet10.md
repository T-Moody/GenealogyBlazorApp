# Blazor / .NET 10 Tech Profile

## Rules

- Use code-behind (.razor.cs) only; no @code blocks.
- Keep components small, single-purpose.
- Use dependency injection for services.
- Maintain vertical slice architecture: Pages → Handlers → Domain → Infrastructure.
- Persistent state using `[PersistentState]` models.
- Async programming with cancellation tokens; dispose properly.
- Use EF Core for database access; minimal APIs for server interaction.
- Unit test all handlers, services, and components.
- Clear code paths, no `continue`, minimal nesting, descriptive method names.
- Bootstrap 5 and semantic HTML for UI.
- **CRITICAL**: All interactive components MUST be placed in the Client project for Interactive Auto mode.

## Summary

This profile defines how to implement features using:

- Blazor Interactive Auto (Server Interactive + WASM)
- .NET 10
- Minimal APIs
- Entity Framework Core
- Vertical slice architecture by feature

## Solution Structure

- `GenealogyBlazorApp` – server host (Minimal APIs, DI root, static components only)
- `GenealogyBlazorApp.Client` – Blazor client (ALL interactive components go here)
- `GenealogyBlazorApp.Shared` – shared DTOs, API contracts, services
- `Domain` – core domain logic
- `Infrastructure` – EF Core, persistence, external systems

---

## Architecture & Structure

- Use **vertical slice architecture**, grouped by feature:

  - Server: `Features/TaskManagement` (APIs, services, data access)
  - Client: `Features/TaskManagement` (interactive components, client services)

- Use **Minimal APIs** for HTTP endpoints in `GenealogyBlazorApp`.
- Use **Entity Framework Core** in `Infrastructure` when persistence is required.
- Use **code-behind** for all components:

  - `.razor` = markup only
  - `.razor.cs` = logic only
  - No `@code` blocks.

- **One class or interface per file.**
- Keep components **thin**:

  - Components orchestrate UI and delegate business logic to services / domain.

- Use **dependency injection (DI)** for all services (typically Scoped for per-user behavior).

---

## Component Placement Rules (CRITICAL)

**Server Project (`GenealogyBlazorApp`):**
- Static components only (layouts, error pages, non-interactive content)
- Minimal APIs endpoints
- Server-side services and business logic
- Database context and infrastructure

**Client Project (`GenealogyBlazorApp.Client`):**
- **ALL interactive components** (forms, admin interfaces, dynamic content)
- Client-side state management
- API client services
- Interactive pages and features

**Why:** Interactive Auto mode requires components to be in the Client project so they can:
- Start with Server Interactive rendering
- Seamlessly transition to WebAssembly when downloaded
- Maintain state across rendering mode transitions

---

## Blazor Interactive Auto & State

- Assume **Blazor Interactive Auto**: prerender + server interactive + potential WASM takeover.
- Use dedicated **state model classes** per component when loading or persisting data:

  - `TaskListState`, `TaskEditState`, etc.

- Decorate persistent state models with `[PersistentState]`.
- In lifecycle methods (e.g., `OnInitializedAsync`), always guard against double-load:
protected override async Task OnInitializedAsync()
{
    if (State is not null)
    {
        return;
    }

    // Initial load logic...
  }
- Keep state models in separate `*State.cs` files (not nested inside components).

---

## UI & Styling

- Use semantic HTML with proper labels and accessibility attributes where appropriate.
- Prefer simple, clean layouts. Bootstrap 5 utilities are acceptable, but avoid heavy custom CSS unless needed.
- Keep components small and focused; split them if they take on multiple responsibilities.

---

## Performance & Lifecycle

- Use `@key` when rendering lists to avoid unnecessary re-renders.
- Avoid redundant `StateHasChanged()` calls.
- Use `CancellationToken` in async operations when appropriate.
- Dispose of resources via `IDisposable` / `IAsyncDisposable` when necessary.

---

## Data, Domain & Shared Layer

- **Do NOT bind EF entities directly in the UI.**
- `Domain`:
  - Entities, value objects, domain services, business rules.
- `Infrastructure`:
  - EF Core DbContext, configurations, repositories (if used).
- `GenealogyBlazorApp.Shared`:
  - DTOs, API contracts, and interfaces for API client services.
  - Abstractions for services that must work in both Server Interactive and WASM modes (e.g. `ITaskApiClient`).

---

## Testing

- Unit test:
  - Domain logic (`Domain.Tests`)
  - Services & vertical slice logic (`GenealogyBlazorApp.Tests`, `Infrastructure.Tests`)
- Component tests:
  - Use bUnit (in `GenealogyBlazorApp.Client.Tests`) when relevant.
- Focus tests on behavior and contracts, not implementation details.

---

## C# / .NET 10 Coding Style

- Descriptive, **single-purpose methods**.
- Minimize nesting; prefer **early returns**.
- **Do not** use `continue` in loops.
- Always use braces, even for single-line `if` / `else`.
- Use `var` **only** when the type is obvious from the right-hand side:
var count = 0;
var person = new Person();
// But:
Customer result = GetCustomer();
- Use file-scoped namespaces.
- Keep logical blocks separated by empty lines for readability.

---

## Component Lifecycle & Events

- Use standard Blazor lifecycle methods:
  - `OnInitializedAsync`
  - `OnParametersSetAsync`
  - `OnAfterRenderAsync`
- Avoid heavy synchronous work in lifecycle; prefer async.
- Use `EventCallback` / `EventCallback<T>` for child-to-parent communication.
- Put event handlers in code-behind, not markup.
- Do not put try/catch in markup; handle errors in code-behind or services.

---

## Forms & Validation

- Use `EditForm` with validation (DataAnnotations or FluentValidation) when forms are needed.
- Keep validation logic in validators, services, or domain—not directly in markup.

---

## Security & Configuration (High Level)

- Use DI-friendly options pattern for configuration.
- Validate all input on the backend (even if validated in UI).
- Assume HTTPS and good security hygiene, but do not generate infra-specific config unless requested.

---

## Docker / DevOps

- **Do not assume Docker** or deployment specifics unless explicitly requested.
- Do not generate Dockerfiles, Kubernetes manifests, or CI/CD pipelines unless asked.

---

## Output Expectations (for Copilot / AI)

When generating Blazor / .NET 10 code in this repo:

1. Use **vertical slice** by feature with proper project placement:
   - Server: `GenealogyBlazorApp/Features/<FeatureName>` (APIs, services, data)
   - Client: `GenealogyBlazorApp.Client/Features/<FeatureName>` (interactive components)
2. **ALL interactive components go in the Client project**
3. Use **Minimal APIs** for endpoints in `GenealogyBlazorApp`.
4. Use **EF Core** in `Infrastructure` for DB interactions when persistence is needed.
5. Use `.razor` + `.razor.cs` with separate `*State.cs` state models and `[PersistentState]`.
6. One class/interface per file.
7. No `@code` blocks.
8. Follow the C# style rules above (early returns, no `continue`, careful `var` usage).
9. Keep UI simple, accessible, and easy to maintain.
