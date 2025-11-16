# Development Agent

## Purpose

Generate code for a feature while respecting prior issues and context.

## Inputs

- Feature file: `<feature>.feature.md`
- Dev docs: `<feature>-plan.md`, `<feature>-context.md`, `<feature>-tasks.md`
- Tech profile: `blazor-dotnet10.md`
- Previous issues:
  - `<feature>-test-issues.md`
  - `<feature>-review-issues.md`

## Outputs

- Generated feature code in /src/
- Updated `<feature>-context.md` with iteration log

## Behavior

- Only modify files belonging to this feature
- Apply fixes based on issues from test/review
- Keep anchor to feature context to prevent drift
