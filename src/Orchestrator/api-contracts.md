# API Contracts

## Operations Overview

The platform provides two main categories of operations:
- **Public Operations**: Available to all users for browsing and searching content
- **Administrative Operations**: Secured operations for content management

## Public Operations (Queries)

### Get Categories List
**Purpose**: Retrieve all visible categories for main navigation
**Input**: None
**Output**: CategorySummary[] {
  id: string
  name: string
  description: string (optional)
  imageUrl: string (optional)
  resourceCount: number
}**Error Cases**: None (empty array if no categories)

### Get Category Details
**Purpose**: Retrieve detailed information about a specific category
**Input**: {
  categoryId: string
}**Output**:
CategoryDetail {
  id: string
  name: string
  description: string (optional)
  imageUrl: string (optional)
  resources: ResourceSummary[] {
    id: string
    title: string
    description: string (optional)
    resourceType: string (Video|Link|Document|Article)
    accessUrl: string
    metadata: object (type-specific properties)
  }
}**Error Cases**: 
- Category not found (404)
- Category not visible (404)

### Search Resources
**Purpose**: Find resources matching search criteria
**Input**:{
  searchText: string (optional)
  categoryId: string (optional)
  resourceType: string (optional)
  limit: number (optional, default 50)
  offset: number (optional, default 0)
}**Output**:
SearchResults {
  results: ResourceSearchResult[] {
    id: string
    title: string
    description: string (optional)
    resourceType: string
    categoryName: string
    accessUrl: string
  }
  totalCount: number
  hasMore: boolean
}**Error Cases**:
- Invalid search parameters (400)

### Get Site Configuration
**Purpose**: Retrieve public site configuration for layout and branding
**Input**: None
**Output**:PublicSiteConfig {
  siteName: string
  mainPageTitle: string
  mainPageContent: string (optional)
  bannerImageUrl: string (optional)
  themeSettings: object
}**Error Cases**: None (returns defaults if not configured)

## Administrative Operations (Commands and Queries)

### Authentication Operations

#### Admin Login
**Purpose**: Authenticate administrator and establish session
**Input**:{
  username: string
  password: string
}**Output**:
AuthResult {
  success: boolean
  sessionToken: string (if successful)
  expiresAt: datetime (if successful)
}**Error Cases**:
- Invalid credentials (401)
- Account locked (423)

#### Admin Logout
**Purpose**: Terminate administrative session
**Input**:{
  sessionToken: string
}**Output**:
{
  success: boolean
}**Error Cases**: None (idempotent operation)

### Category Management Operations

#### Create Category
**Purpose**: Add a new category to the system
**Input**:{
  name: string
  description: string (optional)
  imageUrl: string (optional)
  displayOrder: number (optional)
}**Output**:
{
  categoryId: string
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Duplicate name (409)
- Invalid input data (400)

#### Update Category
**Purpose**: Modify existing category properties
**Input**:{
  categoryId: string
  name: string (optional)
  description: string (optional)
  imageUrl: string (optional)
  displayOrder: number (optional)
  isVisible: boolean (optional)
}**Output**:
{
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Category not found (404)
- Duplicate name (409)
- Invalid input data (400)

#### Delete Category
**Purpose**: Remove category and all associated resources
**Input**:{
  categoryId: string
}**Output**:
{
  success: boolean
  resourcesDeleted: number
}**Error Cases**:
- Unauthorized (401)
- Category not found (404)
- Category has resources (409) - unless force delete is implemented

#### Get All Categories (Admin)
**Purpose**: Retrieve all categories including hidden ones for management
**Input**: None
**Output**:AdminCategoryList[] {
  id: string
  name: string
  description: string (optional)
  imageUrl: string (optional)
  displayOrder: number
  isVisible: boolean
  resourceCount: number
  createdDate: datetime
  modifiedDate: datetime
}**Error Cases**:
- Unauthorized (401)

### Resource Management Operations

#### Create Resource
**Purpose**: Add a new resource to a category
**Input**:{
  categoryId: string
  title: string
  description: string (optional)
  resourceType: string (Video|Link|Document|Article)
  accessUrl: string
  displayOrder: number (optional)
  metadata: object (type-specific properties)
}**Output**:
{
  resourceId: string
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Category not found (404)
- Invalid resource type (400)
- Invalid input data (400)

#### Update Resource
**Purpose**: Modify existing resource properties
**Input**:{
  resourceId: string
  title: string (optional)
  description: string (optional)
  accessUrl: string (optional)
  displayOrder: number (optional)
  isVisible: boolean (optional)
  metadata: object (optional, type-specific properties)
}**Output**:
{
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Resource not found (404)
- Invalid input data (400)

#### Delete Resource
**Purpose**: Remove a resource from the system
**Input**:{
  resourceId: string
}**Output**:
{
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Resource not found (404)

#### Get Category Resources (Admin)
**Purpose**: Retrieve all resources in a category including hidden ones
**Input**:{
  categoryId: string
}**Output**:
AdminResourceList[] {
  id: string
  title: string
  description: string (optional)
  resourceType: string
  accessUrl: string
  displayOrder: number
  isVisible: boolean
  metadata: object
  createdDate: datetime
  modifiedDate: datetime
}**Error Cases**:
- Unauthorized (401)
- Category not found (404)

### Site Configuration Operations

#### Update Site Configuration
**Purpose**: Modify platform-wide settings and appearance
**Input**:{
  siteName: string (optional)
  mainPageTitle: string (optional)
  mainPageContent: string (optional)
  bannerImageUrl: string (optional)
  themeSettings: object (optional)
  contactInfo: string (optional)
}**Output**:
{
  success: boolean
}**Error Cases**:
- Unauthorized (401)
- Invalid input data (400)

#### Get Site Configuration (Admin)
**Purpose**: Retrieve full site configuration for management
**Input**: None
**Output**:AdminSiteConfig {
  siteName: string
  mainPageTitle: string
  mainPageContent: string (optional)
  bannerImageUrl: string (optional)
  themeSettings: object
  contactInfo: string (optional)
  modifiedDate: datetime
}**Error Cases**:
- Unauthorized (401)

## Common Error Response Format

All operations that can fail return errors in a consistent format:ErrorResponse {
  success: false
  errorCode: string
  errorMessage: string
  details: object (optional, for validation errors)
}
## Authentication Requirements

- All administrative operations require valid session token
- Session tokens expire after configurable time period (default: 8 hours)
- Failed authentication attempts are logged for security monitoring
- Public operations require no authentication

## Rate Limiting and Performance

- Search operations limited to prevent abuse (10 requests per minute per IP)
- Administrative operations limited to prevent brute force (5 failures locks account temporarily)
- Large result sets use pagination with configurable limits
- Responses include caching headers where appropriate
