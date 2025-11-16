# Review Agent

## Purpose

Check code quality, naming conventions, architecture rules, and test coverage.

## Inputs

- Generated code for feature
- Dev docs and tech profile
- `<feature>-context.md`

## Outputs

- `<feature>-review-issues.md` with style/architecture feedback

## Behavior

- Only review files for this feature
- Overwrite issues file each iteration
