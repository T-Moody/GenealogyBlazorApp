# Orchestration Agent Instructions

## Agent-Based Development Approach

This project uses a **three-tier orchestration system** for coordinated development. When working with or suggesting agent usage:

### Tier 1: Project Orchestration

- **project-orchestrator**: Use for project-wide coordination, cross-feature dependencies, major milestones
- **When to suggest**: "Consider using @project-orchestrator for this cross-cutting concern"

### Tier 2: Feature Orchestration

- **feature-orchestrator**: Use for individual feature development from planning through deployment
- **When to suggest**: "This looks like a complete feature - consider @feature-orchestrator for coordination"

### Tier 3: Workflow Orchestration

- **01-requirements-agent**: Requirements analysis and validation
- **02-planning-agent**: Implementation planning and task breakdown
- **03-development-agent**: Development coordination and integration
- **04-testing-agent**: Testing orchestration and quality validation
- **05-review-agent**: Review coordination and quality gates
- **06-documentation-agent**: Documentation coordination and publication
- **When to suggest**: "For this [requirements/planning/development/testing/review/documentation] work, consider the appropriate workflow agent"

### Tier 4: Specialist Agents

- **backend-architect**: System architecture and API design
- **dotnet-data-specialist**: Entity Framework and data access
- **blazor-developer**: Blazor components and UI
- **dotnet-security-specialist**: Security implementation
- **blazor-accessibility-performance-specialist**: Accessibility and performance
- **supportability-lifecycle-specialist**: Operations and monitoring
- **documentation-specialist**: Technical writing
- **ui-design-specialist**: UI/UX design
- **requirements-architect**: Requirements analysis

## Integration with Development Ledger

### Always Reference Context

When suggesting solutions, remind to check:

- **DEVELOPMENT_LEDGER.md**: Current project status, recent decisions, existing patterns
- **DEVELOPMENT_LEDGER_GUIDE.md**: How to properly update the ledger
- **PROJECT_PHASES_GUIDE.md**: Current phase requirements and next steps

### Suggest Ledger Updates

When significant work is being done, remind to:

```markdown
"Don't forget to update DEVELOPMENT_LEDGER.md with:

- Decision Log: [the decision made]
- Feature Log: [feature progress]
- Architecture Evolution: [if architecture changed]
- Technical Debt Log: [if shortcuts taken]"
```

### Reference Existing Patterns

Before suggesting new approaches, suggest checking:

```markdown
"Check DEVELOPMENT_LEDGER.md under 'Architecture Patterns' and 'Code Style Guidelines' for existing approaches before implementing new patterns."
```

## Orchestration vs Direct Usage

### Suggest Orchestration When:

- Multiple agents/specialists needed
- Complex cross-cutting work
- Feature-level coordination required
- Quality gates needed
- Timeline/milestone coordination

### Suggest Direct Agent Usage When:

- Single specialist domain (e.g., just data layer work)
- Quick fixes or minor changes
- Specific technical implementation
- Research or exploration

## Quality Gates Integration

Always remind about quality standards:

- Tests required for new functionality
- Code review needed
- Documentation updates required
- Performance impact considered
- Security review if applicable
- Accessibility compliance verified
