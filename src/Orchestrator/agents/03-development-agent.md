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
- Updated `<feature>-context.md` with iteration log

## Behavior

- **FIRST**: Always read and validate against `blazor-dotnet10.md` tech profile
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

## Validation Checklist (Pre-Generation)

Before generating any code, verify:
- [ ] Tech profile `blazor-dotnet10.md` has been read and understood
- [ ] Feature requirements align with vertical slice architecture
- [ ] Previous test/review issues have been addressed
- [ ] Code structure follows `Features/<FeatureName>/` organization
- [ ] All Blazor components will use code-behind pattern
- [ ] State management uses separate `*State.cs` files with `[PersistentState]`

## Post-Generation Validation

After generating code, verify:
- [ ] No @code blocks in .razor files
- [ ] All components have corresponding .razor.cs files
- [ ] State models are separate files with proper attributes
- [ ] Bootstrap 5 classes used for styling
- [ ] Vertical slice structure maintained
- [ ] One class/interface per file rule followed
