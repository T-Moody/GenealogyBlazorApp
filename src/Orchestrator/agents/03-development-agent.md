# Development Agent

## Purpose

Generate code for a feature while respecting prior issues and context.

## Inputs

- Feature file: `<feature>.feature.md`
- Dev docs: `<feature>-plan.md`, `<feature>-context.md`, `<feature>-tasks.md`
- **Tech profile: `blazor-dotnet10.md` (REQUIRED - must validate all code against these standards)**
- Previous issues:
  - `<feature>-test-issues.md`
  - `<feature>-review-issues.md`

## Outputs

- Generated feature code in /src/
- **Generated test code in /tests/ (MANDATORY)**
- Updated `<feature>-context.md` with iteration log

## Behavior

- **FIRST**: Always read and validate against `blazor-dotnet10.md` tech profile
- **MANDATORY**: Generate comprehensive tests for ALL testable code
- Only modify files belonging to this feature
- Apply fixes based on issues from test/review
- Keep anchor to feature context to prevent drift
- **ENFORCE**: All generated code must comply with tech profile rules:
  - Use code-behind (.razor.cs) only; no @code blocks
  - Keep components small, single-purpose
  - Use vertical slice architecture: `Features/<FeatureName>/`
  - Use `[PersistentState]` models in separate State.cs files
  - Bootstrap 5 and semantic HTML for UI
  - Only use var when return type is clear
  - Single responsibility principle
  - One class/interface per file
  - Descriptive method names, early returns, minimal nesting
  - **CRITICAL**: ALL interactive components MUST go in Client project for Interactive Auto
  - **CRITICAL**: ALL testable code MUST have corresponding unit/component tests

## Testing Requirements (MANDATORY)

For every feature, generate:

### Unit Test Projects (Required):
- `tests/GenealogyBlazorApp.Tests/` - Server-side unit tests (services, commands, queries, domain logic)
- `tests/GenealogyBlazorApp.Client.Tests/` - Client-side component tests using bUnit
- `tests/GenealogyBlazorApp.Shared.Tests/` - Shared contracts and service interface tests

### Test Coverage Requirements:
- **Domain Services**: 100% coverage of public methods
- **MediatR Handlers**: Test all command/query handlers with success and failure scenarios
- **API Endpoints**: Integration tests for all endpoints
- **Blazor Components**: Component tests for user interactions and state management
- **Shared Contracts**: Validation tests for DTOs and service interfaces

### Testing Tools and Frameworks:
- **xUnit**: Primary testing framework
- **bUnit**: Blazor component testing
- **FluentAssertions**: Assertion library
- **Moq**: Mocking framework
- **Microsoft.AspNetCore.Mvc.Testing**: API integration tests

## Validation Checklist (Pre-Generation)

Before generating any code, verify:
- [ ] Tech profile `blazor-dotnet10.md` has been read and understood
- [ ] Feature requirements align with vertical slice architecture
- [ ] Previous test/review issues have been addressed
- [ ] Code structure follows proper project placement:
  - [ ] Server project: APIs, services, data access, static components only
  - [ ] Client project: ALL interactive components (forms, admin interfaces, dynamic content)
  - [ ] Tests project: Comprehensive test coverage for all testable code
- [ ] All Blazor components will use code-behind pattern
- [ ] State management uses separate `*State.cs` files with `[PersistentState]`
- [ ] Test projects are structured to match feature organization

## Post-Generation Validation

After generating code, verify:
- [ ] No @code blocks in .razor files
- [ ] All components have corresponding .razor.cs files
- [ ] Interactive components are in Client project (`GenealogyBlazorApp.Client/`)
- [ ] Static/server components are in Server project (`GenealogyBlazorApp/`)
- [ ] State models are separate files with proper attributes
- [ ] Bootstrap 5 classes used for styling
- [ ] Vertical slice structure maintained within correct projects
- [ ] One class/interface per file rule followed
- [ ] **CRITICAL**: Every testable class has corresponding unit tests
- [ ] **CRITICAL**: Every Blazor component has bUnit component tests
- [ ] **CRITICAL**: All test projects build and tests pass
- [ ] Test coverage meets minimum requirements (>80% for critical paths)

## Component Placement Rules (CRITICAL)

**Must place in Server Project (`GenealogyBlazorApp/`):**
- Minimal API endpoints
- Server-side services and business logic  
- Static layouts and error pages
- Non-interactive content components

**Must place in Client Project (`GenealogyBlazorApp.Client/`):**
- Login forms and admin interfaces
- Dynamic content components
- Interactive pages and features
- Client-side state management
- Any component with user interaction

**Must place in Tests Projects:**
- `tests/GenealogyBlazorApp.Tests/Features/<FeatureName>/` - Server-side unit and integration tests
- `tests/GenealogyBlazorApp.Client.Tests/Features/<FeatureName>/` - Client component tests
- `tests/GenealogyBlazorApp.Shared.Tests/Features/<FeatureName>/` - Shared contract tests

## Test Project Structure Requirements
tests/
??? GenealogyBlazorApp.Tests/
?   ??? Features/
?   ?   ??? Authentication/
?   ?       ??? Commands/
?   ?       ??? Queries/
?   ?       ??? Services/
?   ?       ??? Endpoints/
?   ??? Infrastructure/
??? GenealogyBlazorApp.Client.Tests/
?   ??? Features/
?       ??? Authentication/
?           ??? Components/
??? GenealogyBlazorApp.Shared.Tests/
    ??? Features/
        ??? Authentication/
            ??? Contracts/
            ??? Services/
