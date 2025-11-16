# Category Management Feature

## Feature Description

Administrative capability to create, organize, and maintain geographic categories (initially counties) that serve as the primary organizational structure for genealogy resources. Provides full CRUD operations with display ordering and visibility controls.

## User Stories

### Category Creation Stories
- **CM-001**: As an admin, I can create new categories with descriptive names and descriptions so that resources can be properly organized
- **CM-002**: As an admin, I can add banner images to categories to enhance visual presentation and user experience
- **CM-003**: As an admin, I can set display order for categories to control how they appear in navigation
- **CM-004**: As an admin, I can save category drafts as hidden while preparing content before making them public

### Category Management Stories
- **CM-005**: As an admin, I can view a list of all categories including hidden ones to manage the full content structure
- **CM-006**: As an admin, I can edit existing categories to update names, descriptions, images, and settings
- **CM-007**: As an admin, I can reorder categories to adjust navigation presentation as content grows
- **CM-008**: As an admin, I can toggle category visibility to control public access without deletion

### Category Organization Stories
- **CM-009**: As an admin, I can delete empty categories that are no longer needed to maintain clean organization
- **CM-010**: As an admin, I can see resource counts for each category to understand content distribution
- **CM-011**: As a public user, I can see categories in a logical order that makes geographic browsing intuitive

## Acceptance Criteria

### Category Creation
- New category form requires unique name (case-insensitive validation)
- Description supports rich text formatting for enhanced presentation
- Image URL field validates format and provides preview when possible
- Display order auto-assigns next available number but allows manual override
- New categories default to hidden status until explicitly made visible

### Category Listing and Navigation
- Admin view shows all categories with visibility status indicators
- Public view shows only visible categories in display order
- Category list includes resource count for administrative context
- Drag-and-drop or manual reordering updates display sequence immediately

### Category Editing
- Edit form pre-populates with current values for modification
- Name uniqueness validation excludes current category from conflict check
- Changes save immediately with confirmation feedback
- Visibility changes take effect immediately on public interface

### Category Deletion
- Categories with resources cannot be deleted without confirmation and cascade handling
- Empty categories can be deleted immediately
- Deletion confirmation shows impact (number of resources that will be affected)
- Deleted categories are permanently removed (no soft delete in initial version)

## Main User Flows

### Create New Category Flow
1. Admin accesses category management interface
2. Admin clicks "Add Category" button
3. System displays category creation form
4. Admin enters name, description, and optional image URL
5. Admin sets display order (or accepts default)
6. Admin saves category (initially hidden)
7. System confirms creation and returns to category list
8. Admin optionally makes category visible immediately

### Edit Existing Category Flow
1. Admin views category list in management interface
2. Admin clicks edit button for specific category
3. System displays edit form with current values
4. Admin modifies fields as needed
5. Admin previews changes if applicable (image, description)
6. Admin saves changes
7. System confirms update and shows updated list
8. Changes reflect immediately in public interface if category is visible

### Organize Categories Flow
1. Admin reviews current category order in management interface
2. Admin identifies categories that need reordering
3. Admin uses drag-and-drop or manual order controls
4. System updates display order in real-time
5. Admin verifies new order in public interface preview
6. New order takes effect immediately for public users

## Constraints and Assumptions

### Business Constraints
- Category names must be unique across the entire platform
- Geographic focus initially (counties) but model supports other categorizations
- Single-level category hierarchy (no parent/child relationships initially)
- Categories serve as primary navigation mechanism for public users

### Technical Assumptions
- Category images hosted externally or via separate file management
- Rich text descriptions use standard HTML formatting
- Display order conflicts resolved automatically by system
- Category changes propagate immediately without caching delays

## Validations and Edge Cases

### Input Validation
- Category name: 1-100 characters, required, unique (case-insensitive)
- Description: Optional, maximum reasonable length for display
- Image URL: Valid URL format when provided, optional
- Display order: Non-negative integer, duplicates auto-resolved

### Edge Cases
- **Duplicate Names**: System prevents creation/update with existing names
- **Long Descriptions**: Text truncation in list view with full view in edit form
- **Missing Images**: Default placeholder or no image display gracefully
- **Order Conflicts**: System auto-adjusts when multiple categories have same order
- **Visibility Impact**: Making category visible immediately shows all visible resources

### Error Handling
- Validation errors show inline with specific field feedback
- Save failures preserve form state for retry
- Network errors during image preview fail gracefully
- Deletion errors (e.g., category has resources) show clear explanation and options

## Implementation Checklist

### Domain Types to Add/Modify
- [ ] Category entity with all required properties
- [ ] Category validation rules and constraints
- [ ] Display order management logic
- [ ] Category audit and change tracking

### Shared Contracts (DTOs) to Add
- [ ] Category creation request with validation
- [ ] Category update request with optional fields
- [ ] Category list response with admin metadata
- [ ] Category summary for public API
- [ ] Category deletion request with confirmation

### Backend Operations/Endpoints to Implement
- [ ] Create category endpoint with validation
- [ ] Update category endpoint with conflict checking
- [ ] Delete category endpoint with cascade handling
- [ ] List categories endpoint (admin version with all details)
- [ ] Reorder categories endpoint for display sequence
- [ ] Category visibility toggle endpoint

### UI Components/Views to Build
- [ ] Category management list view with admin controls
- [ ] Category creation/edit form with validation
- [ ] Category reordering interface (drag-and-drop or manual)
- [ ] Category deletion confirmation dialog
- [ ] Category visibility toggle controls
- [ ] Public category navigation component

### Tests to Write
- [ ] Unit tests for category validation logic
- [ ] Integration tests for CRUD operations
- [ ] Tests for display order management
- [ ] Tests for visibility state changes
- [ ] End-to-end tests for category management workflows
- [ ] Tests for public category navigation