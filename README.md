# Thumb of Michigan Genealogy

A modern Blazor (.NET 10) web application that helps everyday people learn genealogy research skills, focusing on Michigan's "Thumb" region (Huron, Tuscola, and Sanilac counties).

## 🎯 Project Vision

**Teach, Don't Do** - This platform empowers users to become independent genealogy researchers through structured tutorials and curated regional resources, rather than providing genealogy services.

## 🌟 Key Features

### Public Site
- **Homepage**: Hero section with site mission and About Me content
- **Tutorial Library**: Step-by-step genealogy learning guides with embedded YouTube videos
- **Resource Directory**: Curated external links organized by county and category
- **County Pages**: Dedicated sections for Huron, Tuscola, and Sanilac counties
- **Search & Filter**: Find tutorials and resources quickly

### Admin Interface
- **Content Management**: Simple, non-technical interface for managing all content
- **Tutorial Editor**: Create and edit tutorials with YouTube video embedding
- **Resource Manager**: Organize external genealogy resources by county and category
- **Category System**: Flexible categorization for tutorials and resources
- **Homepage Editor**: Update hero section, About Me, and sidebar content

## 🏗️ Architecture

- **Framework**: .NET 10 with Blazor United (Auto render mode)
- **Architecture**: Vertical Slice Architecture with clean separation
- **Styling**: Bootstrap 5 (minimal custom CSS)
- **Database**: Entity Framework Core with SQL Server/SQLite
- **Authentication**: ASP.NET Core Identity for admin access
- **Testing**: xUnit and bUnit with TDD approach

## 📁 Project Structure

```
GenealogyBlazorApp/
├── src/
│   ├── GenealogyBlazorApp/           # Server/Host project
│   │   ├── Components/               # Layouts and server components
│   │   ├── Features/                 # Vertical slices
│   │   │   ├── Home/                # Homepage feature
│   │   │   ├── Tutorials/           # Tutorial management
│   │   │   ├── Resources/           # Resource management
│   │   │   ├── Categories/          # Category management
│   │   │   └── Counties/            # County-specific content
│   │   └── Infrastructure/          # Data access and services
│   ├── GenealogyBlazorApp.Client/   # Interactive components
│   │   └── Features/                # Feature-based organization
│   └── GenealogyBlazorApp.Shared/   # DTOs and contracts
├── tests/                           # Unit and integration tests
├── requirements/                    # Project requirements documents
└── DEVELOPMENT_LEDGER.md           # Project progress tracking
```

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- Visual Studio 2022 or VS Code
- SQL Server LocalDB or SQLite

### Development Environment Setup
```bash
# Clone the repository
git clone https://github.com/T-Moody/GenealogyBlazorApp.git
cd GenealogyBlazorApp

# Restore dependencies
dotnet restore

# Run the application
dotnet run --project src/GenealogyBlazorApp
```

### Database Setup
```bash
# Add initial migration
dotnet ef migrations add InitialCreate --project src/GenealogyBlazorApp

# Update database
dotnet ef database update --project src/GenealogyBlazorApp
```

## 📋 Development Status

### Phase 1: Requirements Gathering ✅ COMPLETE
- [x] Business Requirements Document
- [x] Functional Requirements Document  
- [x] Technical Requirements Document
- [x] Non-Functional Requirements Document
- [x] Development Ledger established

### Phase 2: Architecture & Design ✅ COMPLETE
- [x] System architecture implementation
- [x] Database schema creation
- [x] Core infrastructure setup
- [x] Authentication system setup

### Phase 3: Core Development 🔄 IN PROGRESS
- [ ] Feature implementation (vertical slices)
  - [ ] Home Feature (In Progress)
- [ ] Admin interface development
- [ ] Public site development
- [ ] Integration testing

## 📖 Documentation

### Requirements Documentation
- [Business Requirements](requirements/01_BUSINESS_REQUIREMENTS.md)
- [Functional Requirements](requirements/02_FUNCTIONAL_REQUIREMENTS.md)  
- [Technical Requirements](requirements/03_TECHNICAL_REQUIREMENTS.md)
- [Non-Functional Requirements](requirements/03_NON_FUNCTIONAL_REQUIREMENTS.md)

### Development Resources
- [Development Ledger](docs/DEVELOPMENT_LEDGER.md) - Project progress tracking
- [GitHub Copilot Instructions](.github/copilot-instructions.md) - AI coding assistant context

## 🎨 Design Principles

- **User-Centric**: Designed for non-technical admin users and genealogy beginners
- **Regional Focus**: Specialized content for Michigan Thumb region
- **Educational**: Teach skills, don't provide services
- **Clean Architecture**: Maintainable, testable, scalable codebase
- **Mobile-First**: Responsive design with Bootstrap 5
- **Accessibility**: WCAG 2.1 AA compliance

## 🔧 Technology Stack

| Component | Technology | Version |
|-----------|------------|---------|
| Framework | .NET | 10 |
| Web Framework | Blazor United | Auto |
| Database | Entity Framework Core | 10 |
| UI Framework | Bootstrap | 5.3+ |
| Testing | xUnit + bUnit | Latest |
| Authentication | ASP.NET Core Identity | 10 |
| Database | SQL Server / SQLite | Latest |

## 🤝 Contributing

This project follows a structured development workflow:

1. **Requirements-Driven**: All features start with documented requirements
2. **Test-Driven Development**: Write tests before implementation
3. **Vertical Slice Architecture**: Organize by feature, not layer
4. **Code Reviews**: All changes require review before merging
5. **Clean Code**: Follow established C# coding standards

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Team

- **Project Owner**: T-Moody
- **Target Admin User**: Non-technical content manager
- **Target Audience**: Genealogy learners in Michigan's Thumb region

## 📊 Project Metrics

- **Target Users**: 500+ within 6 months
- **Content Goal**: 50+ tutorials, 200+ resources
- **Performance**: <3 second page loads, 99.5% uptime
- **Accessibility**: WCAG 2.1 AA compliance

---

**Status**: Phase 3 Core Development - Feature Implementation  
**Last Updated**: November 22, 2025  
**Next Milestone**: Home Feature Completion