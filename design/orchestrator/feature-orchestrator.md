# Feature Orchestrator

## Purpose

Coordinate development, testing, and review for a single feature.

## Workflow

1. Load `<feature>-context.md`

   - If empty → first iteration
   - Else → resume from last iteration

2. Repeat until feature complete or max iterations reached:

   1. Call Development Agent
      - Inputs: feature file + dev docs + tech profile + context + previous test/review issues
      - Output: updated code for feature
   2. Run build and tests
      - Capture failures in `<feature>-test-issues.md`
   3. Run Review Agent
      - Capture code quality/style issues in `<feature>-review-issues.md`
   4. Check issue files:
      - If both empty → mark feature complete in context → exit loop
      - Else → feed issues back to Development Agent for next iteration
   5. Update `<feature>-context.md` with iteration log

3. Log final feature status and summary
