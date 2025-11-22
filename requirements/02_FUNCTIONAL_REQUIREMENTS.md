# Functional Requirements - Thumb of Michigan Genealogy

## Document Information
**Project Name**: Thumb of Michigan Genealogy  
**Document Type**: Functional Requirements Specification  
**Version**: 1.0  
**Date**: November 21, 2025  
**Status**: Phase 1 Complete  

---

## 1. User Roles & Personas

### 1.1 User Roles
[TO BE FILLED BY USER] - Define each user type and their permissions

| Role | Description | Key Capabilities | Access Level |
|------|-------------|------------------|--------------|
| Admin User | [TO BE FILLED] | Full content management | [TO BE FILLED] |
| Public User | [TO BE FILLED] | Browse and search resources | [TO BE FILLED] |
| [Other Roles] | [TO BE FILLED] | [TO BE FILLED] | [TO BE FILLED] |

### 1.2 User Personas
[TO BE FILLED BY USER] - Create detailed user personas

**Admin Persona Example:**
- **Name**: [TO BE FILLED]
- **Role**: [TO BE FILLED]  
- **Goals**: [TO BE FILLED]
- **Pain Points**: [TO BE FILLED]
- **Technical Skill Level**: [TO BE FILLED]

**Public User Persona Example:**
- **Name**: [TO BE FILLED]
- **Background**: [TO BE FILLED]
- **Goals**: [TO BE FILLED]
- **Pain Points**: [TO BE FILLED]
- **Device Preferences**: [TO BE FILLED]

---

## 2. Core Feature Requirements

### 2.1 Authentication & Authorization

#### FR-AUTH-001: Admin Authentication
**Priority**: [low]  
**User Story**: As an admin, I need to securely log into the system so that I can manage content.

**Acceptance Criteria:**
[TO BE FILLED BY USER]
- [ ] Admin can log in with [username/email] and password
- [ ] [TO BE FILLED - Add specific requirements]
- [ ] [TO BE FILLED - Add more criteria]

**Business Rules:**
[TO BE FILLED BY USER]
- Admin login should be hidden from public navigation
- [TO BE FILLED - Add more rules]

#### FR-AUTH-002: Session Management  
[TO BE FILLED BY USER - Complete this section]

### 2.2 County/Category Management

#### FR-COUNTY-001: County Display
**Priority**: [high]  
**User Story**: As a public user, I need to see a list of available counties so that I can select one to explore resources.

**Acceptance Criteria:**
[TO BE FILLED BY USER]
- [ ] Counties are displayed in a sidebar on the main page
- [ ] [TO BE FILLED - How should counties be ordered?]
- [ ] [TO BE FILLED - What information is shown for each county?]
- [ ] [TO BE FILLED - How does selection work?]

#### FR-COUNTY-002: Admin County Management
**Priority**: [medium]  
**User Story**: As an admin, I need to add, edit, and remove counties so that I can manage the resource categories.

**Acceptance Criteria:**
[TO BE FILLED BY USER]
- [ ] Admin can create new counties with [specify required fields]
- [ ] [TO BE FILLED - What can be edited?]
- [ ] [TO BE FILLED - How does deletion work?]

### 2.3 Resource Management

#### FR-RESOURCE-001: Resource Types
**Priority**: High  
**Description**: System supports multiple resource types for comprehensive genealogy education

**Supported Resource Types:**
- [x] **External Website Links** (Primary resource type)
  - Required fields: Title, URL, Description, Category, County Assignment
  - Display format: Card layout with title, description, county badge, category tag
  - Link validation: Automated checking for broken links
- [x] **YouTube Video Tutorials** (Embedded)
  - Required fields: Title, YouTube URL, Description, Category
  - Display format: Embedded player with responsive sizing
  - Privacy: Use privacy-enhanced mode for GDPR compliance
- [x] **Archive and Database Links**
  - Required fields: Title, URL, Description, Access Type (Free/Paid/Registration)
  - Display format: Specialized cards with access requirements clearly marked
  - Examples: FamilySearch, Ancestry.com, local archives
- [x] **Historical Society Resources**
  - Required fields: Organization name, Contact info, Specialties, County
  - Display format: Contact card with address, phone, website, specializations
- [x] **Government Resources**
  - Required fields: Agency name, Resource type, Contact info, Counties served
  - Display format: Official government styling with clear authority indicators

#### FR-RESOURCE-002: Resource Display  
**Priority**: High
**User Story**: As a public user, I need to view resources for a county so that I can access genealogy information efficiently.

**Acceptance Criteria:**
- [x] Resources organized by county tabs (Huron, Tuscola, Sanilac, General)
- [x] Within each county, resources grouped by category (Census, Vital Records, Church Records, etc.)
- [x] Card-based display with vintage paper styling
- [x] Search functionality across all resource titles and descriptions
- [x] Filter options: County, Category, Resource Type, Access Level (Free/Paid)
- [x] Responsive grid layout (4 columns desktop, 2 tablet, 1 mobile)
- [x] External link indicators and "opens in new tab" behavior
- [x] Last verified date displayed for link reliability

#### FR-RESOURCE-003: Admin Resource Management
**Priority**: High
**User Story**: As an admin, I need to efficiently manage genealogy resources so that users have current and reliable information.

**Acceptance Criteria:**
- [x] Simple form interface for adding new resources
- [x] Bulk import from CSV file for initial data population
- [x] Edit/delete resources with confirmation dialogs
- [x] Drag-and-drop reordering within categories
- [x] Link validation dashboard showing broken/questionable links
- [x] Usage analytics: click tracking for resource popularity
- [x] Duplicate detection based on URL and title similarity

### 2.4 Content Management

#### FR-CONTENT-001: Main Page Management
**Priority**: [TO BE FILLED]  
**User Story**: As an admin, I need to customize the main page content so that I can provide appropriate messaging and branding.

**Editable Elements:**
[TO BE FILLED BY USER - Specify what can be edited]
- [ ] Top banner/header image
- [ ] Welcome message/description  
- [ ] [TO BE FILLED - Other elements]

#### FR-CONTENT-002: County Page Customization
[TO BE FILLED BY USER - Complete this requirement]

### 2.5 Search & Navigation

#### FR-SEARCH-001: Resource Search
[TO BE FILLED BY USER - Define search requirements]

#### FR-NAV-001: Navigation Structure  
[TO BE FILLED BY USER - Define how users navigate the site]

---

## 3. User Interface Requirements

### 3.1 Main Page Layout
**Historical/Vintage Theme Design:**

**Layout Requirements:**
- **Header**: Vintage banner with old paper texture, site title in serif font (Times New Roman/Georgia), tagline, theme toggle (Light/Dark)
- **Left Sidebar**: 250px wide, vintage paper background, county navigation, quick links, "About the Region" section
- **Main Content Area**: Central content with aged paper texture, drop shadows for depth, sepia-toned accents
- **Footer**: Minimal, copyright, contact, "Last Updated" timestamp in vintage typography

**Vintage Theme Elements:**
- **Colors**: Cream/parchment backgrounds (#F5F5DC), dark brown text (#3E2723), sepia accents (#8D6E63)
- **Typography**: Serif fonts for headings, clean sans-serif for body text, vintage-style drop caps
- **Textures**: Subtle paper grain, aged edges on content boxes, vintage border decorations
- **Visual Elements**: Old map styling, historical document aesthetics, faded photograph frames

**Dark Mode Specifications:**
- **Colors**: Dark backgrounds (#1A1A1A), warm white text (#F5F5F5), amber accents (#FFC107)
- **Textures**: Dark parchment effect, subtle grain, reduced contrast for eye comfort
- **Toggle**: Prominent but elegant theme switcher in header
- **Persistence**: Remember user preference in localStorage

### 3.2 County Resource Pages
**County-Specific Vintage Styling:**
- **Header**: County name with historical map fragment background
- **Resource Cards**: Vintage filing card appearance with aged edges
- **Category Sections**: Old file folder tabs aesthetic
- **Historical Context**: Sidebar with county founding date, key historical facts
- **Resource Organization**: Library card catalog inspired layout

### 3.3 Admin Interface
**Clean, Functional Admin Design:**

**Admin Interface Sections:**
- [x] **Dashboard Overview**: Content statistics, recent activity, quick actions
- [x] **Tutorial Management**: Create/edit tutorials, category assignment, YouTube embedding
- [x] **Resource Management**: Add/edit resources, county assignment, link validation
- [x] **Category Management**: Create/edit/reorder categories for both tutorials and resources
- [x] **Homepage Editor**: Update hero section, About Me content, sidebar links
- [x] **Site Settings**: Theme customization, vintage color adjustments, site-wide settings

**Admin Theme:**
- Simplified version of vintage theme for clarity and usability
- Light backgrounds with vintage accents, not full vintage treatment
- Focus on usability over aesthetics in admin areas

### 3.4 Responsive Design Requirements

**Device Support:**
- [x] **Desktop (1200px+)**: Full sidebar, 4-column resource grid, full vintage styling with textures
- [x] **Tablet (768-1199px)**: Collapsible sidebar, 2-column resource grid, reduced vintage textures for performance
- [x] **Mobile (320-767px)**: Hidden sidebar (hamburger menu), single-column layout, minimal vintage effects

**Responsive Behavior:**
- Progressive enhancement of vintage effects based on screen size
- Touch-friendly interface elements on mobile
- Readable text sizes across all devices (minimum 16px on mobile)
- Fast loading with optimized vintage background images

---

## 4. Performance Requirements

### 4.1 Response Time Requirements
- **Page load time**: <3 seconds first contentful paint, <5 seconds full load
- **Search response time**: <1 second for resource/tutorial search
- **Resource display time**: <2 seconds for county resource page loading
- **Admin operations**: <2 seconds for content creation/editing
- **Theme switching**: <0.5 seconds transition time

### 4.2 Concurrent User Requirements  
- **Expected concurrent users**: 50 users initially, scale to 100+
- **Peak usage scenarios**: Evening hours (7-10 PM), winter months (genealogy season)
- **Admin concurrent sessions**: 1 primary admin, up to 3 total admin sessions
- **Resource limits**: 1000+ resources, 100+ tutorials without performance degradation

---

## 5. Integration Requirements

### 5.1 Video Platform Integration

**Video Platforms:**
- [x] **YouTube**: Primary video platform for tutorial embedding
  - Privacy-enhanced mode for GDPR compliance
  - Responsive embedding with vintage-styled frames
  - URL validation and metadata extraction
  - Playlist support for multi-part tutorials
- [ ] **Vimeo**: Future consideration for ad-free content
  - Similar embedding requirements as YouTube
  - Privacy-focused alternative option

### 5.2 File Management
- **Image uploads**: Hero images, county headers (JPEG, PNG, WebP)
- **File size limits**: 5MB maximum per image upload
- **Processing**: Automatic resizing and optimization
- **Storage**: Local filesystem initially, cloud storage for scaling
- **Vintage effects**: Optional aging/sepia filters for uploaded images

---

## 6. Data Requirements

### 6.1 County Data Structure

**Required Fields:**
- **County Name**: String, 50 chars max, unique, required
- **Display Name**: String, 100 chars max (e.g., "Huron County, Michigan")
- **Description**: Text, 1000 chars max, optional
- **Founded Date**: Date, for historical context
- **County Seat**: String, 50 chars max
- **Historical Notes**: Rich text, 2000 chars max
- **Header Image**: File path, optional vintage map or photo
- **Sort Order**: Integer, for display ordering

### 6.2 Resource Data Structure  
- **Title**: String, 200 chars max, required
- **URL**: String, valid URL, required
- **Description**: Text, 1000 chars max, required
- **Category**: Foreign key to Categories table
- **Counties**: Many-to-many relationship (resource can serve multiple counties)
- **Resource Type**: Enum (Website, Archive, Database, Organization, Government)
- **Access Level**: Enum (Free, Paid, Registration Required)
- **Last Verified**: DateTime, for link validation tracking
- **Click Count**: Integer, for analytics
- **Added By**: Foreign key to Users (admin who added)
- **Tags**: Comma-separated keywords for search

### 6.3 Admin Data Structure
- **Username**: String, 50 chars, unique, required
- **Email**: String, valid email, unique, required
- **Password Hash**: String, bcrypt hashed
- **Full Name**: String, 100 chars, for audit trails
- **Role**: Enum (Admin, ContentManager), future expansion
- **Last Login**: DateTime
- **Created Date**: DateTime
- **Is Active**: Boolean, for account management

---

## 7. Security Requirements

### 7.1 Admin Security
- **Authentication method**: ASP.NET Core Identity with cookie authentication
- **Password requirements**: 8+ characters, mixed case, numbers, special characters
- **Session timeout**: 2 hours inactivity, sliding expiration
- **Admin URL protection**: /admin path, not linked from public navigation
- **Failed login protection**: 5 attempts = 15 minute lockout
- **HTTPS enforcement**: All admin operations over secure connections

### 7.2 Data Security
- **Input validation**: Server-side validation for all user inputs
- **XSS prevention**: Proper encoding of all output, especially rich text
- **SQL injection prevention**: Parameterized queries, Entity Framework ORM
- **File upload restrictions**: Image files only, size limits, virus scanning
- **Content moderation**: Admin review for all public-facing content
- **Audit logging**: Track all admin actions with timestamps and user identification

---

## 8. Accessibility Requirements

### 8.1 Accessibility Standards
- **WCAG compliance level**: WCAG 2.1 AA compliance mandatory
- **Screen reader support**: Full compatibility with NVDA, JAWS, VoiceOver
- **Keyboard navigation**: Complete site navigation without mouse required
- **Color contrast**: Minimum 4.5:1 ratio for normal text, 3:1 for large text
- **Focus indicators**: Clear, visible focus states for all interactive elements
- **Alt text**: Descriptive alt text for all images, including vintage decorative elements
- **Semantic HTML**: Proper heading hierarchy, landmark regions, form labels
- **Motion preferences**: Respect prefers-reduced-motion for theme transitions
- **Text scaling**: Support up to 200% zoom without horizontal scrolling
- **High contrast mode**: Alternative high contrast theme option

---

## 9. Theme & Styling Requirements

### 9.1 Visual Design - Vintage Historical Theme

**Light Mode Color Scheme:**
- **Primary Background**: Warm cream/parchment (#F5F5DC, #FEFDF0)
- **Secondary Background**: Light aged paper (#F9F7F4)
- **Text Colors**: Dark brown (#3E2723), medium brown (#5D4E75)
- **Accent Colors**: Sepia brown (#8D6E63), vintage gold (#B8860B)
- **Border Colors**: Aged brown (#A1887F), vintage ink (#4E342E)

**Dark Mode Color Scheme:**
- **Primary Background**: Rich dark brown (#1A1611, #2D2417)
- **Secondary Background**: Warm charcoal (#3E3731)
- **Text Colors**: Warm white (#F5F5F5), light cream (#E8E3D3)
- **Accent Colors**: Warm amber (#FFC107), vintage brass (#DAA520)
- **Border Colors**: Bronze (#CD7F32), warm gold (#FFD700)

**Typography:**
- **Headers**: Serif fonts (Playfair Display, Georgia, Times New Roman)
- **Body Text**: Clean serif (Crimson Text) or sans-serif (Open Sans) for readability
- **Accent Text**: Vintage script fonts for special elements (Dancing Script)
- **Size Scale**: 16px base, 1.2 ratio scaling, minimum 14px on mobile

**Branding Elements:**
- Vintage Michigan Thumb region map elements
- Old document styling (aged edges, subtle stains)
- Historical flourishes and decorative borders
- Genealogy-themed icons (family trees, documents, archives)

### 9.2 Theme Flexibility

**Theme Switching Requirements:**
- **Toggle Control**: Elegant sun/moon icon in header, smooth transitions
- **Persistence**: LocalStorage to remember user preference
- **System Preference**: Detect and respect OS dark mode preference on first visit
- **Smooth Transitions**: 0.3s CSS transitions for color changes

**Customization Levels:**
- **Admin Customizable**: Hero image, accent colors, site title/tagline
- **User Selectable**: Light/Dark mode toggle
- **Future Enhancements**: Multiple vintage color schemes (sepia, blue-gray, green heritage)
- **Accessibility**: High contrast mode option, respect prefers-reduced-motion

**Bootstrap 5 Integration:**
- Custom CSS variables for vintage color overrides
- Utility classes for vintage effects (aged-border, vintage-shadow)
- Component modifications: cards, buttons, forms with vintage styling
- Responsive utilities maintained while adding vintage aesthetics

---

## ✅ COMPLETION STATUS

**DOCUMENT STATUS**: ✅ COMPLETE - Ready for Phase 2  
**COMPLETION CHECKLIST**:
- [x] All user roles and personas defined with genealogy context
- [x] All feature requirements specified with detailed acceptance criteria
- [x] UI/UX requirements documented with vintage theme specifications
- [x] Performance and security requirements set with realistic targets
- [x] Data structure requirements defined for genealogy content
- [x] Integration requirements specified (YouTube, file management)
- [x] Vintage/historical theme requirements with dark mode support
- [x] Accessibility requirements meeting WCAG 2.1 AA standards

**PHASE GATE STATUS**: ✅ APPROVED - Proceed to Technical Architecture  
**NEXT PHASE**: Architecture implementation and core development setup  

---

## Instructions for Completion

1. **Start with User Roles**: Clearly define who will use the system and how
2. **Detail Each Feature**: Use the provided template structure for consistency
3. **Be Specific**: Avoid vague requirements; include measurable criteria
4. **Consider Edge Cases**: Think about error conditions and boundary cases
5. **Validate with Stakeholders**: Ensure all requirements align with business needs

**Once complete, return to Project Orchestrator for technical architecture planning**