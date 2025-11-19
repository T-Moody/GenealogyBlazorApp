# Enhanced Agent Usage Guide

## Overview

This guide covers the enhanced agent system including orchestration agents, workflow agents, and integration patterns. The system provides intelligent coordination for complex development projects while maintaining deep specialist expertise.

## Agent Architecture

### Three-Tier System

```
PROJECT LEVEL
├── project-orchestrator          # Project-wide coordination and management

FEATURE LEVEL
├── feature-orchestrator          # Feature-specific coordination and delivery

WORKFLOW LEVEL
├── 01-requirements-agent         # Requirements analysis and validation
├── 02-planning-agent            # Implementation planning and task breakdown
├── 03-development-agent         # Development coordination and integration
├── 04-testing-agent             # Testing orchestration and quality validation
├── 05-review-agent              # Review coordination and quality gates
└── 06-documentation-agent       # Documentation coordination and publication

SPECIALIST LEVEL (Existing)
├── backend-architect            # System architecture and API design
├── dotnet-data-specialist       # Data access and Entity Framework
├── csharp-developer            # C# implementation and best practices
├── blazor-developer            # Blazor components and UI implementation
├── dotnet-security-specialist   # Security implementation and validation
├── blazor-accessibility-performance-specialist  # Accessibility and performance
├── supportability-lifecycle-specialist         # Operations and monitoring
├── documentation-specialist     # Technical writing and documentation
├── ui-design-specialist        # UI/UX design and styling
├── requirements-architect      # Requirements gathering and analysis
└── context-manager             # Context engineering and orchestration
```

## Updated Agent Selection Decision Tree

### Starting a New Project?

→ **project-orchestrator** (overall project coordination)
→ **requirements-architect** (comprehensive requirements gathering)
→ **backend-architect** (system architecture design)
→ **supportability-lifecycle-specialist** (infrastructure and operations setup)

### Working on a Complete Feature?

→ **feature-orchestrator** (feature-specific coordination)
→ **01-requirements-agent** (if requirements need clarification)
→ **02-planning-agent** (implementation planning)
→ **03-development-agent** (development coordination)
→ **04-testing-agent** (quality validation)
→ **05-review-agent** (systematic reviews)
→ **06-documentation-agent** (documentation coordination)

### Need Specific Workflow Coordination?

**Requirements unclear?** → **01-requirements-agent**
**Need implementation plan?** → **02-planning-agent**
**Coordinating development?** → **03-development-agent**
**Need comprehensive testing?** → **04-testing-agent**
**Quality reviews needed?** → **05-review-agent**
**Documentation coordination?** → **06-documentation-agent**

### Working on Specific Technical Implementation?

**Backend Development:**
→ **dotnet-data-specialist** (data layer first)
→ **backend-architect** (API and service logic)
→ **csharp-developer** (implementation details)
→ **dotnet-security-specialist** (security implementation)

**Frontend Development:**
→ **blazor-developer** (component implementation)
→ **ui-design-specialist** (visual design and styling)
→ **blazor-accessibility-performance-specialist** (accessibility and performance)

**Operations/Deployment:**
→ **supportability-lifecycle-specialist** (all operational concerns)

## Enhanced Usage Patterns

### Pattern 1: New Project Kickoff

```markdown
1. project-orchestrator
   "Start new [project type] with [requirements summary]"
2. Automatic coordination of:

   - requirements-architect (detailed requirements)
   - backend-architect (system design)
   - supportability-lifecycle-specialist (infrastructure)
   - documentation-specialist (project documentation)

3. Output: Complete project setup with feature roadmap
```

### Pattern 2: Feature Development

```markdown
1. feature-orchestrator
   "Implement [feature name] with [requirements]"
2. Automatic workflow coordination:

   - 01-requirements-agent (if clarification needed)
   - 02-planning-agent (implementation planning)
   - 03-development-agent (coordinates specialists)
   - 04-testing-agent (quality validation)
   - 05-review-agent (systematic reviews)
   - 06-documentation-agent (documentation)

3. Output: Production-ready feature with complete documentation
```

### Pattern 3: Specialist Coordination

```markdown
1. 03-development-agent
   "Coordinate backend implementation for [feature]"
2. Manages specialist sequence:

   - dotnet-data-specialist (data layer)
   - backend-architect (API design)
   - csharp-developer (implementation)
   - dotnet-security-specialist (security)
   - supportability-lifecycle-specialist (observability)

3. Output: Integrated backend implementation
```

### Pattern 4: Quality Assurance

```markdown
1. 04-testing-agent
   "Execute comprehensive testing for [feature]"
2. Coordinates quality validation:

   - Unit and integration testing
   - Performance testing (blazor-accessibility-performance-specialist)
   - Security testing (dotnet-security-specialist)
   - Accessibility testing (blazor-accessibility-performance-specialist)
   - User acceptance testing

3. Output: Quality-validated feature ready for production
```

## Orchestration vs Direct Agent Usage

### Use Orchestration When:

- **Multiple agents needed**: Feature requires coordination across specialists
- **Complex workflows**: Multi-phase development with dependencies
- **Quality gates**: Systematic validation and review processes
- **Cross-cutting concerns**: Security, performance, accessibility across components
- **Timeline coordination**: Managing development phases and milestones
- **Stakeholder communication**: Regular reporting and status updates

### Use Direct Agents When:

- **Single specialist task**: Specific technical implementation within one domain
- **Quick fixes**: Bug fixes or minor enhancements
- **Exploration**: Research or proof-of-concept work
- **Maintenance**: Routine updates or minor improvements
- **Individual expertise**: Need deep specialist knowledge without coordination

## Enhanced Prompt Templates

### Project Orchestrator Templates

#### New Project Kickoff

```markdown
Agent: project-orchestrator

"I'm starting a new [project type] project with the following requirements:

**Business Objectives:**

- [Primary business goal]
- [Success metrics]
- [User benefits]

**Functional Requirements:**

- [Core feature 1]
- [Core feature 2]
- [Integration requirements]

**Non-Functional Requirements:**

- Performance: [response time, throughput]
- Security: [compliance, authentication]
- Accessibility: [WCAG level]
- Scalability: [user volume, data volume]

**Constraints:**

- Timeline: [project duration]
- Team: [team size and skills]
- Technology: [technology constraints]
- Budget: [resource limitations]
- Integration: [existing systems]

Please coordinate the project initialization including requirements gathering, architecture planning, infrastructure setup, and feature roadmap creation."
```

#### Cross-Feature Coordination

```markdown
Agent: project-orchestrator

"I have conflicting requirements between features that need resolution:

**Feature A:** [Description and requirements]
**Feature B:** [Description and requirements]

**Conflicts:**

- [Specific conflict 1]
- [Specific conflict 2]

**Impact:**

- [Timeline impact]
- [Resource impact]
- [Technical impact]

**Stakeholders:**

- [Affected teams/people]

Please analyze the conflicts, propose resolution options, and coordinate the decision-making process."
```

### Feature Orchestrator Templates

#### Complete Feature Development

```markdown
Agent: feature-orchestrator

"I need to implement [feature name] with comprehensive coordination:

**Feature Description:**
[Detailed feature description]

**Business Value:**
[Why this feature matters]

**Requirements:**
[Detailed requirements or link to requirements document]

**Technical Constraints:**

- Architecture: [architectural constraints]
- Performance: [performance requirements]
- Security: [security requirements]
- Integration: [systems to integrate with]

**Quality Standards:**

- Code coverage: [minimum percentage]
- Performance benchmarks: [specific targets]
- Security compliance: [standards to meet]
- Accessibility: [WCAG level required]

**Timeline:**

- Target completion: [date]
- Key milestones: [important dates]

Please coordinate the complete feature development including requirements validation, planning, implementation, testing, review, and documentation."
```

#### Feature Integration

```markdown
Agent: feature-orchestrator

"I need to integrate [feature name] with existing system components:

**Feature Summary:**
[Brief feature description]

**Integration Points:**

- [System/component 1]: [integration details]
- [System/component 2]: [integration details]
- [External service]: [integration details]

**Integration Challenges:**

- [Challenge 1 and impact]
- [Challenge 2 and impact]

**Quality Requirements:**

- Integration testing: [requirements]
- Performance impact: [acceptable impact]
- Error handling: [requirements]

Please coordinate the integration planning, implementation, testing, and validation."
```

### Workflow Agent Templates

#### Requirements Clarification

```markdown
Agent: 01-requirements-agent

"I have unclear requirements that need clarification and validation:

**Current Requirement:**
[Existing requirement statement]

**Ambiguities:**

- [Specific ambiguity 1]
- [Specific ambiguity 2]

**Missing Information:**

- [What's missing 1]
- [What's missing 2]

**Business Context:**
[Business context and objectives]

**Stakeholders:**
[Who can provide clarification]

Please analyze the requirements, identify gaps, create clarifying questions, and propose refined acceptance criteria."
```

#### Implementation Planning

```markdown
Agent: 02-planning-agent

"I have validated requirements and need a detailed implementation plan:

**Feature Requirements:**
[Detailed requirements or link]

**Technical Context:**

- Architecture: [current architecture]
- Technology stack: [technologies to use]
- Integration points: [systems to integrate]
- Performance targets: [specific benchmarks]

**Team Context:**

- Available specialists: [available agents/team members]
- Skill level: [team capabilities]
- Timeline: [available time]

**Constraints:**

- [Technical constraint 1]
- [Resource constraint 2]
- [Business constraint 3]

Please create a comprehensive implementation plan with task breakdown, effort estimates, risk assessment, and specialist coordination strategy."
```

#### Development Coordination

```markdown
Agent: 03-development-agent

"I need to coordinate implementation across multiple specialists:

**Implementation Plan:**
[Link to plan or detailed plan summary]

**Specialists Required:**

- [Specialist 1]: [tasks and dependencies]
- [Specialist 2]: [tasks and dependencies]
- [Specialist 3]: [tasks and dependencies]

**Integration Points:**

- [Component 1] ↔ [Component 2]: [integration details]
- [API contracts]: [contract specifications]
- [Data flows]: [data flow requirements]

**Quality Standards:**

- Code quality: [standards and tools]
- Testing requirements: [coverage and types]
- Performance targets: [benchmarks]
- Security requirements: [security standards]

Please coordinate the implementation, manage integration, enforce quality standards, and track progress."
```

#### Comprehensive Testing

```markdown
Agent: 04-testing-agent

"I need comprehensive testing coordination for a completed feature:

**Feature Summary:**
[Feature description and scope]

**Testing Requirements:**

- Unit testing: [coverage requirements]
- Integration testing: [integration points to test]
- E2E testing: [user journeys to validate]
- Performance testing: [benchmarks to meet]
- Security testing: [security aspects to validate]
- Accessibility testing: [WCAG level to achieve]

**Quality Gates:**

- [Quality gate 1]: [criteria]
- [Quality gate 2]: [criteria]

**Test Environment:**

- Environment setup: [requirements]
- Test data: [data requirements]
- External dependencies: [mock/real services]

Please coordinate comprehensive testing across all quality dimensions and validate against quality gates."
```

#### Systematic Reviews

```markdown
Agent: 05-review-agent

"I need systematic review coordination for a feature ready for review:

**Feature Details:**
[Feature name and implementation summary]

**Review Requirements:**

- Code review: [focus areas and standards]
- Architecture review: [architectural aspects]
- Security review: [security concerns]
- Performance review: [performance aspects]
- Business logic review: [business requirements]

**Review Scope:**

- Components: [list of components/files]
- Integration points: [systems and APIs]
- Documentation: [docs to review]

**Quality Standards:**

- [Standard 1]: [specific requirements]
- [Standard 2]: [specific requirements]

**Timeline:**

- Review completion needed by: [date]

Please coordinate multi-dimensional reviews, manage review workflow, and ensure quality standards are met."
```

#### Documentation Coordination

```markdown
Agent: 06-documentation-agent

"I need comprehensive documentation coordination for a completed feature:

**Feature Summary:**
[Feature description and scope]

**Documentation Requirements:**

- API documentation: [endpoints and integration guides]
- User documentation: [user guides and workflows]
- Technical documentation: [architecture and implementation]
- Operational documentation: [runbooks and monitoring]

**Audience:**

- Developers: [integration and maintenance docs]
- Users: [user guides and help content]
- Operations: [deployment and support docs]
- Business: [feature descriptions and benefits]

**Quality Standards:**

- Accuracy: [technical validation requirements]
- Completeness: [coverage requirements]
- Usability: [user experience standards]
- Accessibility: [documentation accessibility]

Please coordinate comprehensive documentation creation, quality assurance, and publication."
```

## Best Practices for Enhanced System

### 1. Choose the Right Orchestration Level

- **Project-level issues**: Use project-orchestrator
- **Feature-level work**: Use feature-orchestrator
- **Workflow-specific needs**: Use appropriate workflow agent
- **Specialist tasks**: Use specialist agents directly

### 2. Provide Rich Context

- Include business objectives and success criteria
- Specify technical constraints and requirements
- Identify stakeholders and communication needs
- Document known risks and dependencies
- Reference existing documentation and decisions

### 3. Leverage Orchestration Intelligence

- Let orchestrators manage agent coordination
- Trust workflow agents to coordinate specialists
- Use escalation triggers appropriately
- Maintain context through Development Ledger

### 4. Maintain Quality Standards

- Define quality gates at appropriate orchestration level
- Use systematic review processes
- Enforce standards through coordination
- Document and share quality metrics

### 5. Optimize for Learning and Improvement

- Capture lessons learned through orchestration
- Share knowledge across features and projects
- Evolve processes based on orchestration feedback
- Build institutional knowledge in Development Ledger

## Migration from Existing System

### Phase 1: Introduce Orchestration Gradually

1. **Start with feature-orchestrator** for new features
2. **Keep using specialist agents** for maintenance and fixes
3. **Use workflow agents** when multi-agent coordination is needed
4. **Introduce project-orchestrator** for new projects

### Phase 2: Optimize Coordination

1. **Identify coordination pain points** in current workflow
2. **Apply appropriate orchestration** to resolve coordination issues
3. **Measure improvement** in delivery speed and quality
4. **Refine orchestration usage** based on results

### Phase 3: Full Orchestration Adoption

1. **Use orchestration by default** for new work
2. **Migrate existing features** to orchestration when making major changes
3. **Establish orchestration best practices** for team
4. **Train team** on orchestration patterns and usage

The enhanced system maintains all the power of your existing specialist agents while adding intelligent coordination that reduces overhead, improves quality, and ensures better outcomes. Start with the orchestration level that matches your immediate needs and gradually adopt more comprehensive orchestration as you see the benefits.
