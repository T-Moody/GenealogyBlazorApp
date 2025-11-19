# Orchestration Workflow Instructions

## Critical Orchestration Rules

### 1. Always Follow PROJECT_PHASES_GUIDE.md Workflow

**MANDATORY**: All orchestration agents MUST follow the seven-phase workflow defined in PROJECT_PHASES_GUIDE.md:

1. **Requirements Gathering** - Generate documents, collect user input
2. **Architecture & Design** - Only after requirements complete
3. **Development Setup & Infrastructure** - Only after architecture complete
4. **Feature Development** - Only after setup complete
5. **Quality Assurance & Testing** - Only after development complete
6. **Production Readiness** - Only after QA complete
7. **Production Deployment & Hypercare** - Only after production ready

### 2. Phase Gate Enforcement

**Before proceeding to next phase**, verify completion criteria from PROJECT_PHASES_GUIDE.md:

```markdown
Phase 1 Completion Criteria:
- [ ] All stakeholder interviews completed
- [ ] Business requirements documented and approved
- [ ] Functional requirements documented with acceptance criteria
- [ ] Non-functional requirements quantified
- [ ] User stories created and prioritized
- [ ] Constraints and assumptions documented
- [ ] Risks identified and mitigation strategies proposed
- [ ] Development ledger updated with requirements phase
- [ ] Requirements reviewed and signed off by stakeholders
```

**DO NOT PROCEED** to Phase 2 until ALL Phase 1 criteria are met.

### 3. Document Generation Requirements

When starting **any** project or feature, orchestrators MUST:

#### Phase 1: Generate Requirements Documents

Use `documentation-specialist` to create these files:

```markdown
REQUIRED DOCUMENTS FOR USER INPUT:

/docs/requirements/BUSINESS_REQUIREMENTS.md
/docs/requirements/FUNCTIONAL_REQUIREMENTS.md  
/docs/requirements/NON_FUNCTIONAL_REQUIREMENTS.md
/docs/requirements/USER_STORIES.md
/docs/requirements/CONSTRAINTS_AND_ASSUMPTIONS.md
```

#### Phase 2: Generate Architecture Documents

Use `documentation-specialist` + `backend-architect` to create:

```markdown
REQUIRED ARCHITECTURE DOCUMENTS:

/docs/architecture/SYSTEM_ARCHITECTURE.md
/docs/architecture/DATA_ARCHITECTURE.md
/docs/architecture/SECURITY_ARCHITECTURE.md
/docs/architecture/decisions/ADR-001-[decision].md
```

### 4. User Interaction Patterns

**CRITICAL**: Orchestrators must:

1. **Generate template documents** with clear sections for user input
2. **Ask user to fill out** the generated documents
3. **Wait for user completion** before proceeding
4. **Validate completeness** before moving to next phase

#### Example Workflow:
```markdown
1. Project Orchestrator: "I'm creating requirements documents for you to fill out..."
2. Creates: /docs/requirements/BUSINESS_REQUIREMENTS.md
3. "Please fill out the Business Requirements document with your project details"
4. WAIT for user completion
5. Validate requirements are complete
6. THEN proceed to Phase 2
```

### 5. Development Ledger Integration

**Every phase transition** must update DEVELOPMENT_LEDGER.md with:

- Current phase status
- Completion criteria met
- Next phase ready to begin
- Decisions made during phase
- Blockers or risks identified

### 6. Orchestration Agent Responsibilities

#### Project Orchestrator
- **MUST** enforce phase gates
- **MUST** generate required documents before proceeding
- **MUST** wait for user input on requirements
- **MUST** validate completion criteria

#### Feature Orchestrator  
- **MUST** ensure requirements are complete before design
- **MUST** follow same phase validation for feature-level work
- **MUST** integrate with project-level phases

#### Workflow Agents
- **MUST** reference PROJECT_PHASES_GUIDE.md for their specific phase
- **MUST** create deliverables defined in their phase
- **MUST** validate inputs from previous phases

## Error Recovery

If orchestration flow is broken:

1. **Stop current work**
2. **Identify missing phase deliverables**
3. **Generate missing documents**
4. **Ask user to complete missing inputs**
5. **Validate phase completion**
6. **Resume at correct phase**

## Quality Gates

Before ANY phase transition, verify:

- [ ] All deliverables from current phase complete
- [ ] User has provided required input
- [ ] Quality criteria met
- [ ] Next phase prerequisites satisfied
- [ ] Development Ledger updated

**NO EXCEPTIONS** - Quality gates prevent technical debt and scope creep.