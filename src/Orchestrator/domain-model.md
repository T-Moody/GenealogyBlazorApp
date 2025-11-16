# Domain Model

## Main Domain Concepts

### Category
Represents a geographic or organizational grouping for resources (initially counties, but extensible).
- **Core Purpose**: Organize resources into logical groups for browsing and navigation
- **Identity**: Unique identifier and human-readable name
- **Content**: Name, description, optional image, display order

### Resource
Represents any genealogy-related material accessible through the platform.
- **Core Purpose**: Provide access to genealogy research materials
- **Identity**: Unique identifier within a category
- **Content**: Title, description, resource type, access information, metadata

### Site Configuration
Represents platform-wide settings and content managed by administrators.
- **Core Purpose**: Control overall site appearance and behavior
- **Content**: Banner images, main page content, theme settings, navigation structure

### Administrative User
Represents the authorized content manager for the platform.
- **Core Purpose**: Secure access control for content management operations
- **Identity**: Login credentials and session management
- **Scope**: Full access to all content and configuration

## Entities and Relationships

### Category EntityCategory:
- ID (unique identifier)
- Name (display name, required, unique)
- Description (rich text, optional)
- ImageUrl (optional banner/header image)
- DisplayOrder (integer for sorting)
- IsVisible (boolean for admin control)
- CreatedDate (audit timestamp)
- ModifiedDate (audit timestamp)
**Relationships:**
- Category ? Resources (one-to-many): A category contains multiple resources
- Category is independent (no parent categories in initial model)

### Resource EntityResource:
- ID (unique identifier)
- CategoryID (foreign reference to parent category)
- Title (display name, required)
- Description (detailed text, optional)
- ResourceType (enum: Video, Link, Document, Article)
- AccessUrl (external link or internal path)
- DisplayOrder (integer for sorting within category)
- IsVisible (boolean for admin control)
- Metadata (flexible additional properties)
- CreatedDate (audit timestamp)
- ModifiedDate (audit timestamp)
**Relationships:**
- Resource ? Category (many-to-one): Each resource belongs to one category
- Resources are independent of each other (no hierarchies or cross-references initially)

### Resource Type Specializations
Different resource types have specific additional properties:

**Video Resource:**
- EmbedCode (for YouTube/Vimeo integration)
- Duration (optional metadata)
- ThumbnailUrl (optional)

**Link Resource:**
- TargetUrl (external website)
- LinkType (external site, document, tool, etc.)

**Document Resource:**
- FileUrl (path to uploaded file)
- FileType (PDF, image, etc.)
- FileSizeBytes (for display)

**Article Resource:**
- Content (rich text body)
- Author (optional attribution)
- PublishDate (optional)

### Site Configuration EntitySiteConfiguration:
- ID (singleton identifier)
- SiteName (platform title)
- MainPageTitle (homepage header)
- MainPageContent (introductory text)
- BannerImageUrl (main page header image)
- ThemeSettings (color scheme and styling options)
- ContactInfo (optional admin contact)
- ModifiedDate (audit timestamp)
**Relationships:**
- Independent singleton entity (no relationships to other entities)

## Important Invariants and Rules

### Data Integrity Rules
- **Rule 1**: Category names must be unique across the entire platform
- **Rule 2**: Resources must always belong to exactly one valid category
- **Rule 3**: Display orders within a category should be unique (system can auto-resolve conflicts)
- **Rule 4**: Resource access URLs must be valid and accessible
- **Rule 5**: Visible resources must belong to visible categories

### Business Logic Rules
- **Rule 6**: Only authenticated administrators can modify content
- **Rule 7**: Public users can only access resources marked as visible in visible categories
- **Rule 8**: Resource types determine required and optional metadata fields
- **Rule 9**: Categories and resources maintain audit trails for administrative tracking
- **Rule 10**: Site configuration changes take effect immediately without requiring restart

### Validation Rules
- **Rule 11**: Category names cannot be empty and must be reasonable length (1-100 characters)
- **Rule 12**: Resource titles cannot be empty (1-200 characters)
- **Rule 13**: URLs must be well-formed when provided
- **Rule 14**: Rich text content must be sanitized for security
- **Rule 15**: Display orders must be non-negative integers

### Extensibility Rules
- **Rule 16**: New resource types can be added without affecting existing data
- **Rule 17**: Category model can be extended to support hierarchies in future versions
- **Rule 18**: Metadata fields support flexible expansion for resource specialization
- **Rule 19**: Site configuration supports addition of new settings without breaking changes
- **Rule 20**: Administrative access model can accommodate multiple administrators in future versions

## Domain Services

### Content Management Service
- Handles creation, modification, and deletion of categories and resources
- Enforces business rules and validation
- Manages display order conflicts and updates

### Search Service
- Provides text-based search across resource titles and descriptions
- Handles filtering by resource type and category
- Maintains search index for performance

### Security Service
- Manages administrative authentication and session state
- Enforces access control for content modification operations
- Validates administrative permissions for each operation

### Configuration Service
- Manages site-wide settings and appearance
- Handles theme and layout configuration
- Provides cached access to frequently-used settings
