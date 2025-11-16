---
feature: "authentication"
agent: build-test
iteration: 1
status: issues
timestamp: 2024-01-15T00:46:00Z
---

issues:
  - id: package:mediatr_version_conflict
    severity: critical
    message: "Package version conflict: MediatR.Extensions.Microsoft.DependencyInjection 11.1.0 requires MediatR (>= 11.0.0 && < 12.0.0) but version MediatR 12.4.1 was resolved."
    file: "src/GenealogyBlazorApp/GenealogyBlazorApp.csproj"
    
  - id: package:automapper_version_conflict  
    severity: critical
    message: "Package version conflict: AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1 requires AutoMapper (= 12.0.1) but version AutoMapper 13.0.1 was resolved."
    file: "src/GenealogyBlazorApp/GenealogyBlazorApp.csproj"
    
  - id: package:auth_cookies_not_found
    severity: critical
    message: "Unable to find package Microsoft.AspNetCore.Authentication.Cookies with version (>= 10.0.0). Nearest version: 2.3.0"
    file: "src/GenealogyBlazorApp/GenealogyBlazorApp.csproj"
    
  - id: package:authorization_unnecessary
    severity: minor
    message: "PackageReference Microsoft.AspNetCore.Authorization will not be pruned. Consider removing this package as it is likely unnecessary."
    file: "src/GenealogyBlazorApp/GenealogyBlazorApp.csproj"

  - id: missing:program_configuration
    severity: major
    message: "Program.cs not configured with authentication services, endpoints, and database setup"
    file: "src/GenealogyBlazorApp/Program.cs"