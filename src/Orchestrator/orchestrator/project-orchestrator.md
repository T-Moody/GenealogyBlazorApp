# Project Orchestrator

## Purpose

Coordinate agents for end-to-end project workflow.

## Workflow

1. Read project-idea.md
2. Call Requirements Agent → generate project-requirements.md
3. Call Planning Agent → generate domain-model.md, api-contracts.md, ui-flows.md, dev-docs
4. For each feature:
   - Call Feature Orchestrator
5. Aggregate review summaries and testing results
