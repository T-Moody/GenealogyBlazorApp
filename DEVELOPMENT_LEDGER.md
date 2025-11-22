# Development Ledger - Thumb of Michigan Genealogy

## Project Overview
- **Name**: Thumb of Michigan Genealogy
- **Purpose**: Modern Blazor (.NET 10) web app to help people learn genealogy research
- **Vision**: Teach everyday people to conduct their own genealogy research independently
- **Target Region**: Michigan's "Thumb" region (Huron, Tuscola, Sanilac counties)
- **Start Date**: November 21, 2025
- **Technology Stack**: .NET 10, Blazor United (Auto), Entity Framework, Bootstrap 5

## Project Phases

### Phase 1: Requirements Gathering ⏳ IN PROGRESS
**Started**: November 21, 2025  
**Status**: 🟡 Active - Creating requirement documents

#### Phase 1 Deliverables
- [x] Development Ledger (this document)
- [ ] Business Requirements Document
- [ ] Functional Requirements Document  
- [ ] Technical Requirements Document
- [ ] Non-Functional Requirements Document
- [ ] Updated README with project overview

#### Phase 1 Key Decisions Made
- **Architecture**: Vertical Slice Architecture with clean separation
- **Styling**: Bootstrap 5 only (no custom CSS)
- **Render Mode**: Blazor United (Auto) for optimal performance
- **Code-Behind**: All Razor components will use .razor.cs code-behind files
- **Regional Focus**: Initial launch with 3 counties (Huron, Tuscola, Sanilac)

### Phase 2: Architecture & Design 📋 PENDING
**Status**: ⏸️ Waiting for Phase 1 completion

#### Phase 2 Planned Deliverables
- [ ] System architecture documentation
- [ ] Database schema design
- [ ] API contract definitions
- [ ] Component hierarchy design
- [ ] Authentication strategy
- [ ] Deployment architecture

### Phase 3: Core Development 🛠️ PENDING
**Status**: ⏸️ Waiting for Phase 2 completion

### Phase 4: Testing & Quality Assurance 🧪 PENDING
**Status**: ⏸️ Waiting for Phase 3 completion

### Phase 5: Deployment & Launch 🚀 PENDING
**Status**: ⏸️ Waiting for Phase 4 completion

## Architecture Decisions Record

### ADR-001: Technology Stack Selection
**Date**: November 21, 2025  
**Status**: Accepted  
**Decision**: .NET 10 with Blazor United (Auto render mode)  
**Rationale**: 
- Latest stable .NET version for performance and features
- Blazor United provides optimal rendering flexibility (SSR + Interactive)
- Auto render mode adapts to network conditions automatically

### ADR-002: Styling Strategy
**Date**: November 21, 2025  
**Status**: Accepted  
**Decision**: Bootstrap 5 only, no custom CSS  
**Rationale**:
- Reduces maintenance overhead
- Ensures consistency across components
- Faster development with utility classes
- Better accessibility out of the box

### ADR-003: Code Organization
**Date**: November 21, 2025  
**Status**: Accepted  
**Decision**: Razor components with .razor.cs code-behind files  
**Rationale**:
- Separates markup from logic
- Better IntelliSense and debugging support
- Follows C# best practices
- Easier unit testing of component logic

### ADR-004: Architecture Pattern
**Date**: November 21, 2025  
**Status**: Accepted  
**Decision**: Vertical Slice Architecture  
**Rationale**:
- Feature-focused organization
- Reduces coupling between features
- Easier to maintain and extend
- Clear separation of concerns

## Current Technical State

### Existing Project Structure
```
GenealogyBlazorApp.sln
├── src/
│   ├── GenealogyBlazorApp/ (Server/Host)
│   ├── GenealogyBlazorApp.Client/ (Interactive Components)
│   └── GenealogyBlazorApp.Shared/ (Contracts/DTOs)
└── tests/
    ├── GenealogyBlazorApp.Tests/
    ├── GenealogyBlazorApp.Client.Tests/
    └── GenealogyBlazorApp.Shared.Tests/
```

### Current Program.cs Configuration
- ✅ Blazor components configured (Server + WebAssembly)
- ✅ HTTP Context Accessor added
- ✅ Authentication/Authorization middleware configured
- ⚠️ Missing: Database context configuration
- ⚠️ Missing: Feature endpoint registration
- ⚠️ Missing: Dependency injection for services

### Outstanding Technical Debt
- [ ] Database context and connection string
- [ ] Authentication implementation
- [ ] Feature folder structure creation
- [ ] Endpoint routing configuration
- [ ] Entity Framework migrations setup
- [ ] Logging configuration enhancement

## Risk Register

### High Priority Risks
1. **Content Quality Risk**: Outdated or incorrect genealogy guidance
   - **Mitigation**: Establish content review processes with domain experts
   
2. **Admin Usability Risk**: Complex admin interface deterring content updates
   - **Mitigation**: User testing with target admin persona

3. **External Link Risk**: Resource links becoming unavailable over time
   - **Mitigation**: Automated link validation and monitoring

### Medium Priority Risks
1. **User Adoption Risk**: Competition from established genealogy education sites
   - **Mitigation**: Focus on regional specialization and unique value proposition

2. **Content Volume Risk**: Limited initial content reducing user engagement
   - **Mitigation**: Phased content development with priority tutorials

## Next Phase Gate Criteria

**Phase 1 → Phase 2 Requirements:**
- ✅ Development ledger established
- ⏳ All requirement documents completed and reviewed
- ⏳ Architecture decisions documented
- ⏳ Risk mitigation strategies defined
- ⏳ Stakeholder approval obtained

**Phase 2 → Phase 3 Requirements:**
- System architecture designed and approved
- Database schema finalized
- API contracts defined
- Authentication strategy implemented
- Development environment fully configured

---

**Last Updated**: November 21, 2025  
**Next Review**: After Phase 1 completion  
**Project Orchestrator**: Active monitoring of phase gates and deliverable completion