# Project Idea: Blazor Interactive Auto App

## High-Level Description

An interactive web application built with Blazor (.NET 10) that uses server-side rendering and client-side WebAssembly. Features include:

- Feature-based vertical slice architecture.
- Reusable components and state management with `[PersistentState]`.
- Minimal APIs with EF Core for database access.
- Admin tool for managing content dynamically.
- Data-driven pages: counties, tutorials, links, documents, videos.
- Theme/color scheme abstraction for easy swapping.

## Goals

- Maintainable, testable, and production-quality code.
- Simple, data-driven genealogy resource platform.
- Flexible main page and county resource pages.
- Modular and reusable Blazor components.

## Key Features

1. **Hidden Admin Login & Management**

   - Single admin user with secure login.
   - Admin page not linked publicly; only accessible via known URL or authentication.
   - Admin can create/edit pages, update titles, links, images, and counties.
   - Backend APIs protected with authorization; public users cannot access them.
   - Flexible category model: initially counties, but can be expanded.

2. **Main Page Layout**

   - Left sidebar with counties (or categories), dynamically loaded.
   - Right content area shows description/statement for selected county.
   - Top banner/image, editable by admin.
   - Components modular and reusable, supporting Server + WebAssembly.

3. **County Resource Pages**

   - Header with county name, optional image, and description.
   - Resource sections:
     - **Video Tutorials** (embedded YouTube/Vimeo)
     - **Links / Websites**
     - **Documents / PDFs**
     - **Articles / Guides** (rich text)
   - Filters or tabs by resource type (optional, configurable by admin).
   - Data-driven: admin can add/remove resources and configure display order.
   - Components are small, single-purpose, and use code-behind.

4. **Search & Navigation**

   - Browse counties and search resources by name, link, or description.
   - Sidebar or inline filters for resource types.

5. **Data-Driven & Extensible**

   - All content editable via admin tool.
   - EF Core database stores counties, pages, resources, and content metadata.
   - Supports adding new counties, categories, or resource types in the future.

6. **Theme / Color Scheme**

   - Bootstrap defaults initially.
   - Color scheme and theme abstracted for easy swapping later.

7. **Blazor Architecture & Best Practices**
   - Server + WebAssembly setup for optimal performance.
   - Vertical slice + minimal APIs.
   - Shared services for server/client support.
   - Component state management with `[PersistentState]`.
   - Code-behind files for all components.
   - Single-purpose methods, minimal nesting, descriptive names.
   - Modular, testable, maintainable code.
