# Resource Management Feature

## Feature Description

Administrative capability to create, organize, and maintain genealogy resources of various types (videos, links, documents, articles) within categories. Provides type-specific editing interfaces and flexible metadata management for different resource formats.

## User Stories

### Resource Creation Stories
- **RM-001**: As an admin, I can add video resources with embedded YouTube/Vimeo links to provide tutorial content
- **RM-002**: As an admin, I can add external website links with descriptions to guide users to helpful genealogy sites
- **RM-003**: As an admin, I can add document resources (PDFs, images) to share reference materials
- **RM-004**: As an admin, I can create article resources with rich text content for in-depth guides
- **RM-005**: As an admin, I can organize resources within categories with display order control

### Resource Management Stories
- **RM-006**: As an admin, I can view all resources within a category including hidden ones for comprehensive management
- **RM-007**: As an admin, I can edit existing resources to update content, links, and metadata
- **RM-008**: As an admin, I can move resources between categories when organizational needs change
- **RM-009**: As an admin, I can toggle resource visibility to control public access without deletion
- **RM-010**: As an admin, I can bulk manage resources for efficient content organization

### Resource Access Stories
- **RM-011**: As a public user, I can view resources organized by type within each category for easy browsing
- **RM-012**: As a public user, I can access different resource types through appropriate interfaces (embedded videos, external links, downloads, readable articles)
- **RM-013**: As a public user, I can see resource descriptions and metadata to understand content before accessing

## Acceptance Criteria

### Resource Type Handling
- **Video Resources**: Support YouTube/Vimeo embed codes, optional duration and thumbnail metadata
- **Link Resources**: Validate external URLs, support link categorization (website, tool, database)
- **Document Resources**: Handle file uploads or external file URLs, display file type and size
- **Article Resources**: Provide rich text editor for content creation, support basic formatting

### Resource Creation Process
- Resource creation form adapts based on selected type
- Required fields validated before save (title, type, access information)
- Optional metadata fields appear based on resource type
- Resources default to hidden status until explicitly made visible
- Created resources automatically assigned to selected category

### Resource Organization
- Resources display in configured order within category sections
- Admin interface shows all resources with type indicators and visibility status
- Public interface groups resources by type with clear section headers
- Display order can be adjusted within each category

### Resource Editing
- Edit form pre-populates with current values including type-specific metadata
- Resource type changes update available fields dynamically
- Changes save with immediate feedback and validation
- Editing preserves resource history for audit purposes

## Main User Flows

### Add Video Resource Flow
1. Admin selects category for new resource
2. Admin chooses "Video" resource type
3. System displays video-specific form fields
4. Admin enters title, description, YouTube/Vimeo URL or embed code
5. Admin optionally adds duration and thumbnail URL
6. Admin sets display order and visibility
7. System validates video URL/embed code
8. Admin saves resource and system confirms creation

### Add Link Resource Flow
1. Admin selects category for new resource
2. Admin chooses "Link" resource type
3. System displays link-specific form fields
4. Admin enters title, description, and target URL
5. Admin selects link type (website, database, tool, etc.)
6. System validates URL format and accessibility
7. Admin saves resource and system confirms creation

### Add Document Resource Flow
1. Admin selects category for new resource
2. Admin chooses "Document" resource type
3. System displays document-specific form fields
4. Admin enters title, description, and either uploads file or provides URL
5. System detects file type and size for display
6. Admin sets access permissions and display order
7. System validates file accessibility
8. Admin saves resource and system confirms creation

### Add Article Resource Flow
1. Admin selects category for new resource
2. Admin chooses "Article" resource type
3. System displays article-specific form with rich text editor
4. Admin enters title and creates content using editor
5. Admin optionally adds author attribution and publish date
6. Admin previews formatted article content
7. Admin saves article and system confirms creation

### Bulk Resource Management Flow
1. Admin accesses category resource management
2. Admin selects multiple resources using checkboxes
3. Admin chooses bulk action (change visibility, reorder, move category)
4. System applies changes to selected resources
5. Admin confirms bulk operation results
6. System updates resource status and provides feedback

## Constraints and Assumptions

### Resource Type Constraints
- Video resources limited to supported embed formats (YouTube, Vimeo initially)
- Document resources support common formats (PDF, DOC, images)
- Article content uses secure HTML subset to prevent security issues
- Link resources validated for accessibility but external content not controlled

### Technical Assumptions
- File uploads handled through separate file management system
- External URLs validated for format but not necessarily accessibility
- Rich text editor provides standard formatting options
- Resource metadata extensible for future resource types

## Validations and Edge Cases

### Input Validation
- Resource title: 1-200 characters, required
- URLs: Valid format validation, HTTPS preferred
- File uploads: Size limits, allowed file type restrictions
- Rich text: HTML sanitization for security
- Display order: Non-negative integers with conflict resolution

### Edge Cases
- **Broken Links**: System cannot validate external link accessibility in real-time
- **Large Files**: File size limits enforced with clear user feedback
- **Rich Text Security**: HTML content sanitized to prevent XSS attacks
- **Duplicate URLs**: Same external resource can exist in multiple categories
- **Resource Migration**: Moving resources between categories preserves all metadata

### Error Handling
- File upload failures provide clear error messages and retry options
- Invalid URLs show specific validation feedback
- Rich text editor handles formatting errors gracefully
- Save failures preserve form state for user retry
- Bulk operation failures show detailed success/failure status per item

## Implementation Checklist

### Domain Types to Add/Modify
- [ ] Base resource entity with polymorphic type handling
- [ ] Video resource specialization with embed metadata
- [ ] Link resource specialization with URL validation
- [ ] Document resource specialization with file metadata
- [ ] Article resource specialization with content management
- [ ] Resource validation rules per type

### Shared Contracts (DTOs) to Add
- [ ] Resource creation requests per type
- [ ] Resource update requests with optional fields
- [ ] Resource list responses with type-specific metadata
- [ ] Bulk operation requests for multiple resources
- [ ] Resource search/filter requests

### Backend Operations/Endpoints to Implement
- [ ] Create resource endpoint with type-specific validation
- [ ] Update resource endpoint with metadata handling
- [ ] Delete resource endpoint with confirmation
- [ ] List resources by category endpoint (admin and public versions)
- [ ] Move resources between categories endpoint
- [ ] Bulk resource operations endpoint
- [ ] Resource file upload/management endpoints

### UI Components/Views to Build
- [ ] Resource type selection interface
- [ ] Type-specific resource creation/edit forms
- [ ] Resource list management interface with type indicators
- [ ] Rich text editor component for articles
- [ ] File upload component with progress and validation
- [ ] Bulk selection and operation interface
- [ ] Public resource display components per type

### Tests to Write
- [ ] Unit tests for resource validation logic per type
- [ ] Integration tests for CRUD operations
- [ ] Tests for file upload and management
- [ ] Tests for rich text content security
- [ ] End-to-end tests for resource management workflows
- [ ] Performance tests for resource listing and search