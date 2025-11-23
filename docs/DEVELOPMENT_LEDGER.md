# Development Ledger - Thumb of Michigan Genealogy

## Current Status
**Phase**: Phase 3: Core Development
**Sprint**: 1
**Last Updated**: November 22, 2025
**Active Work**: Home Feature Implementation
**Blockers**: None
**Next Milestone**: Complete Home Feature (Public & Admin)

## Decision Log

### November 21, 2025 - Technology Stack Selection
- **Context**: Choosing the foundation for the application.
- **Decision**: .NET 10 with Blazor United (Auto render mode).
- **Alternatives**: Blazor Server (higher latency), Blazor WASM (slow initial load).
- **Consequences**: Optimal performance, requires .NET 10 environment.
- **Status**: Active
- **Related ADR**: ADR-001

### November 21, 2025 - Styling Strategy
- **Context**: Defining the visual design approach.
- **Decision**: Bootstrap 5 only, no custom CSS.
- **Alternatives**: Tailwind (more setup), Custom CSS (maintenance overhead).
- **Consequences**: Faster development, consistent look, less maintenance.
- **Status**: Active
- **Related ADR**: ADR-002

### November 21, 2025 - Code Organization
- **Context**: Structuring Blazor components.
- **Decision**: Razor components with .razor.cs code-behind files.
- **Alternatives**: Single file (markup + code).
- **Consequences**: Better separation of concerns, easier testing.
- **Status**: Active
- **Related ADR**: ADR-003

### November 21, 2025 - Architecture Pattern
- **Context**: Organizing the codebase.
- **Decision**: Vertical Slice Architecture.
- **Alternatives**: Clean Architecture (Layered).
- **Consequences**: Feature-focused, easier to maintain and scale.
- **Status**: Active
- **Related ADR**: ADR-004

## Feature Log

### Feature: Home
- **Status**: In Progress
- **Priority**: High
- **Started**: November 22, 2025
- **Completed**: -
- **Requirements**: [Business Requirements](../requirements/01_BUSINESS_REQUIREMENTS.md)
- **Architecture**: Vertical Slice (Server Endpoints + Client Pages)
- **Implementation**:
  - Code Location: `src/GenealogyBlazorApp/Features/Home`, `src/GenealogyBlazorApp.Client/Features/Home`
- **Testing**:
  - Unit Test Coverage: Pending
- **Dependencies**: None
- **Notes**: Includes Hero section, About Me, and dynamic content management.

## Architecture Evolution

### November 22, 2025 - Initial Architecture Defined
- **Component**: Overall System
- **Change**: Initial project structure created with Server, Client, and Shared projects.
- **Reason**: Starting development with clear architectural foundation.
- **Impact**: All components defined with clear boundaries.

## Environment Log

### Environment: Development
- **Purpose**: Local Development
- **URL**: https://localhost:7000
- **Infrastructure**: Local Machine
- **Database**: SQL Server LocalDB / SQLite
- **Status**: Active

## Metrics & KPIs

### Development Metrics (Updated November 22, 2025)
- **Sprint Velocity**: N/A (First Sprint)
- **Code Coverage**: Pending
