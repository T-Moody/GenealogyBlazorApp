# Home Page Management Feature Plan

## Status and Overview

**Feature Name**: Home Page Management  
**Status**: In Progress  
**Priority**: High  
**Estimated Effort**: 2-3 days  
**Target Completion**: TBD  

### Purpose
Provide admin users with the ability to manage homepage content including site title, tagline, hero image, about content, profile image, and sidebar links. This enables content updates without code deployments and supports the vintage/historical theme of the genealogy education site.

### Business Value
- Enable non-technical content updates
- Maintain fresh, current homepage content
- Support marketing and engagement initiatives
- Reduce developer dependencies for content changes

## Requirements and Architecture

### Functional Requirements
1. **Content Management Interface**
   - Admin-only access to homepage content editor via `/admin/home/editor`
   - Inline editing experience: Admin sees the page exactly as it renders publicly, but with editable fields
   - Shared components between public and admin views to ensure consistency
   - Image upload and management for hero/profile images
   - **County Cards Management**: Admin can update Title and Image for the 3 fixed county cards (Huron, Sanilac, Tuscola)
   - **Genealogy Websites Management**: Admin can manage quick links to external genealogy sites
   - Preview functionality is inherent in the inline editor design

2. **Homepage Display**
   - Public homepage displays current active content
   - **County Links**: Prominent, large clickable cards/buttons for Huron, Sanilac, and Tuscola immediately visible
   - **Navigation**: Remove "About" link from top right; "Home" and "Counties" menu persist
   - **Scroll Icon**: Add text "Read More" to the scroll icon
   - **Genealogy Websites**: Quick links section (Ancestry, FamilySearch, etc.)
   - Responsive design with vintage/historical theme
   - Graceful fallback for missing content
   - SEO-optimized meta tags from content

3. **Content Validation**
   - Required field validation
   - Image format and size validation
   - Content length limits
   - XSS protection for rich text content

### Non-Functional Requirements
- **Performance**: Page load < 2 seconds
- **Security**: Admin authentication required, XSS protection
- **Accessibility**: WCAG 2.1 AA compliance
- **Theme**: Vintage/historical design consistency

### Architecture Integration
- Uses existing HomeContent entity and AppDbContext
- Integrates with ASP.NET Core Identity for admin auth
- Follows vertical slice architecture pattern
- Uses MediatR for CQRS commands/queries
- **Client-Side**: Reuses Blazor components with an `IsEditor` parameter to toggle between view and edit modes

## Design Decisions

### ADR-003: Use Rich Text Editor for About Content
**Context**: About content needs formatting capabilities (bold, italic, links, lists)
**Decision**: Implement Quill.js rich text editor with vintage styling
**Alternatives**: Plain textarea, Markdown editor
**Consequences**: Enhanced UX but additional JS dependency

### ADR-004: Single HomeContent Record Pattern
**Context**: Homepage has multiple editable sections
**Decision**: Use single database record with multiple properties
**Alternatives**: Separate records per section, key-value store
**Consequences**: Simplified queries but larger record updates

### ADR-006: Inline Admin Editing with Shared Components
**Context**: We want the admin editing experience to closely match the public view to reduce "preview" cycles and ensure visual consistency.
**Decision**: Use the same Blazor components (`HomeHero`, `AboutSection`, `Sidebar`) for both public and admin pages. These components accept an `IsEditor` parameter. When `true`, they render input fields; when `false`, they render display content.
**Alternatives**: Separate admin form components, "Edit" mode on public page.
**Consequences**: Reduces code duplication, ensures WYSIWYG experience, but requires components to handle two modes.

### ADR-007: Structured County Card Storage
**Context**: The home page features 3 specific counties (Huron, Sanilac, Tuscola). The admin needs to edit their titles and images.
**Decision**: Store these as structured fields (or a related entity) rather than a generic JSON blob, to allow for specific validation and easier file handling for images.
**Alternatives**: JSON blob in HomeContent, Hardcoded.
**Consequences**: Requires schema update to HomeContent or new entity.

### ADR-008: JSON Storage for Variable Lists (Genealogy Links)
**Context**: The "Genealogy Websites" section contains a variable list of external links.
**Decision**: Store these as a serialized JSON string in a single column (`GenealogyLinks`) on the `HomeContent` entity.
**Rationale**: 
- The data is always retrieved and displayed with the Home page.
- There is no need to query *inside* the list (e.g. filtering by URL).
- It avoids the complexity of a separate one-to-many table for a simple display list.
**Consequences**: Application logic must handle serialization/deserialization and validation.

## Implementation Plan

### Backend Tasks (backend-architect, csharp-developer)

*Following Vertical Slice Architecture with MediatR CQRS pattern*

#### Data Layer
- [x] Review existing HomeContent entity (already created)
- [ ] Add HomeContentConfiguration for any missing constraints
- [ ] Create MediatR handlers with direct DbContext access
- [ ] Add audit logging for content changes

#### Feature Slice Implementation
- [x] Create GetHomeContentQuery and Handler (direct DbContext access)
- [x] Create UpdateHomeContentCommand and Handler (direct DbContext access)
- [x] Create GetPublicHomeContentQuery and Handler
- [ ] Add FluentValidation rules for HomeContent commands
- [ ] **Update HomeContent entity/DTOs to support County Cards (Title, Image)**
- [ ] **Update HomeContent entity/DTOs to support Genealogy Links (replacing SidebarLinks?)**
- [x] Create HomeContent feature endpoints:
  - `GET /api/home-content/public` - Get published content (Implemented in `HomeEndpoints.cs`)
  - `GET /api/home-content/admin` - Get content for admin (Implemented in `AdminHomeEndpoints.cs`)
  - `PUT /api/home-content/admin` - Update content (Implemented in `AdminHomeEndpoints.cs`)

#### Security & Validation
- [ ] Add [Authorize] attributes for admin endpoints
- [ ] Implement XSS sanitization for rich text content
- [ ] Add file upload validation for images
- [ ] Create content length validation rules

### Frontend Tasks (blazor-developer, ui-design-specialist)

#### Admin Components
- [x] Create `Editor.razor` page (`/admin/home/editor`)
  - Uses shared components with `IsEditor="true"`
  - Calls `AdminHomeService` to save changes
- [x] Implement `AdminHomeService` client-side service
- [ ] Add admin navigation menu item
- [ ] **Implement County Card editing in Editor (Title inputs, Image uploads)**

#### Shared Components (Public & Admin)
- [x] Create `HomeHero.razor` component
  - Support `IsEditor` parameter
  - Render inputs in edit mode, text/image in view mode
  - **Update: Add "Read More" text to scroll icon**
- [ ] **Create `CountyCards.razor` component**
  - Display 3 large cards (Huron, Sanilac, Tuscola)
  - Support `IsEditor` parameter (Edit Title, Upload Image)
- [ ] **Create `GenealogyLinks.razor` component** (Replaces Sidebar?)
  - Quick links to external sites
  - Support `IsEditor` parameter
- [x] Create `AboutSection.razor` component
  - Support `IsEditor` parameter
  - Integrate Rich Text Editor (Quill) for edit mode
- [x] Create `Sidebar.razor` component (May be deprecated or repurposed)
  - Support `IsEditor` parameter
- [x] Create `Home.razor` page for public view (`/`)
  - Uses shared components with `IsEditor="false"`
  - **Update: Remove "About" link from top nav (Global Layout change)**

#### Styling & Theme
- [ ] Apply vintage/historical CSS classes
- [ ] Ensure WCAG 2.1 AA accessibility compliance
- [ ] Test responsive behavior on mobile/tablet
- [ ] Optimize image loading and sizing

### Testing Tasks

#### Unit Tests
- [ ] HomeContent command/query handler tests
- [ ] HomeContent validation tests
- [ ] HomeContent endpoint tests
- [ ] HomeContent entity mapping tests

#### Integration Tests
- [ ] API endpoint integration tests
- [ ] Database CRUD operation tests
- [ ] Authentication/authorization tests
- [ ] File upload integration tests

#### E2E Tests
- [ ] Admin login and access home content editor
- [ ] Create/update home content successfully
- [ ] Preview functionality works correctly
- [ ] Public homepage displays updated content
- [ ] Image upload and display workflow

### Documentation Tasks (documentation-specialist)

- [ ] Update API documentation with new endpoints
- [ ] Create admin user guide for content management
- [ ] Document rich text editor usage and limitations
- [ ] Update architecture diagrams if needed
- [ ] Create troubleshooting guide for common issues

## Technical Specifications

### API Endpoints

```csharp
// GET /api/admin/home-content
public class GetHomeContentQuery : IRequest<HomeContentDto>
{
    // Returns current home content for editing
}

// PUT /api/admin/home-content  
public class UpdateHomeContentCommand : IRequest<UpdateResult>
{
    public string SiteTitle { get; set; }
    public string Tagline { get; set; }
    public string AboutContent { get; set; }
    public string SidebarLinks { get; set; }
    public IFormFile? HeroImage { get; set; }
    public IFormFile? ProfileImage { get; set; }
}

// GET /api/home-content/public
public class GetPublicHomeContentQuery : IRequest<PublicHomeContentDto>
{
    // Returns published content for public display
}
```

### Data Transfer Objects

```csharp
public class HomeContentDto
{
    public string SiteTitle { get; set; }
    public string Tagline { get; set; }
    public string AboutContent { get; set; }
    public string GenealogyLinks { get; set; } // Replaces SidebarLinks
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
    
    // County Cards
    public string HuronTitle { get; set; }
    public string? HuronImagePath { get; set; }
    public string SanilacTitle { get; set; }
    public string? SanilacImagePath { get; set; }
    public string TuscolaTitle { get; set; }
    public string? TuscolaImagePath { get; set; }

    public DateTime LastUpdated { get; set; }
    public string LastUpdatedBy { get; set; }
}
```

### Validation Rules

- SiteTitle: Required, max 100 characters
- Tagline: Optional, max 200 characters  
- AboutContent: Required, max 5000 characters
- GenealogyLinks: Optional, valid JSON format, max 2000 characters
- County Titles: Required, max 50 characters
- Images: JPG/PNG only, max 2MB, max 1920x1080 resolution

### File Storage
- Images stored in `/wwwroot/uploads/home/`
- Filename format: `{timestamp}_{sanitized-original-name}.{ext}`
- Generate thumbnails for display optimization

## Dependencies and Risks

### Dependencies
- ASP.NET Core Identity setup (already complete)
- Entity Framework migrations (already complete)
- Admin authentication system (needs implementation)
- File upload infrastructure

### Risks
- **High**: Admin authentication not yet implemented
  - Mitigation: Implement basic admin auth first
- **Medium**: Rich text editor XSS vulnerabilities
  - Mitigation: Use HtmlSanitizer library
- **Low**: Image upload storage space
  - Mitigation: Implement file size limits

### Assumptions
- Admin users are trusted content editors
- Single admin user for initial implementation
- Images hosted locally (not CDN initially)

## Testing Strategy

### Test Pyramid
- **Unit Tests (70%)**: MediatR handlers, validation logic, entity mapping
- **Integration Tests (20%)**: API endpoints, database operations
- **E2E Tests (10%)**: Critical user workflows

### Test Scenarios
1. **Happy Path**: Admin creates/updates content successfully
2. **Validation**: Required fields, content limits, file types
3. **Security**: Unauthorized access attempts, XSS attempts
4. **Performance**: Large content saves, image uploads
5. **Error Handling**: Network failures, database errors

### Acceptance Criteria
- All tests pass with >80% code coverage
- Manual testing of admin workflow complete
- Accessibility testing with screen reader
- Cross-browser testing (Chrome, Firefox, Edge)

## Deployment Plan

### Pre-Deployment
- [ ] Database migrations applied (none needed)
- [ ] Admin user seeded in database
- [ ] File upload directory created with permissions
- [ ] Configuration values verified

### Deployment Steps
1. Deploy backend API changes
2. Deploy frontend components
3. Verify admin authentication works
4. Test content creation/update workflow
5. Verify public homepage displays correctly

### Rollback Plan
- Keep previous deployment package ready
- Database rollback not needed (no schema changes)
- File system rollback for uploaded images

### Post-Deployment Monitoring
- Monitor application logs for errors
- Check file upload directory disk usage
- Verify homepage load times
- Monitor admin usage patterns

## Success Criteria

### MVP Definition of Done
- [ ] Admin user can log in to content management interface
- [ ] Admin can update all homepage content fields
- [ ] Rich text editor works for about content
- [ ] Image upload works for hero and profile images
- [ ] Public homepage displays updated content correctly
- [ ] Content validation prevents invalid submissions
- [ ] All tests pass with required coverage
- [ ] Code review completed and approved
- [ ] Documentation updated and reviewed

### Quality Gates
- [ ] Security review passed (XSS protection verified)
- [ ] Accessibility testing passed (WCAG 2.1 AA)
- [ ] Performance testing passed (page load <2s)
- [ ] Cross-browser testing completed
- [ ] Admin user acceptance testing completed

### Metrics for Success
- Homepage content can be updated in <5 minutes
- Page load time remains <2 seconds after content updates
- Zero security vulnerabilities in content management
- 100% uptime during content updates
- Admin user satisfaction with editing experience

---

**Next Steps**: Begin implementation with backend data layer and API endpoints, then move to frontend components and styling.