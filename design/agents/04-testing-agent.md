# Testing Agent

## Purpose

Generate and run tests for a feature; produce issue reports.

## Inputs

- Generated code from Development Agent
- Dev docs: `<feature>-plan.md`, `<feature>-context.md`, `<feature>-tasks.md`
- Tech profile: `blazor-dotnet10.md`

## Outputs

- `<feature>-test-issues.md` with failing tests or errors

## Behavior

- Only create issues related to files for this feature
- Overwrite issues file each iteration
