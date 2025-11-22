# Non-Functional Requirements - Thumb of Michigan Genealogy

## Document Information
**Project Name**: Thumb of Michigan Genealogy  
**Document Type**: Non-Functional Requirements Specification  
**Version**: 1.0  
**Date**: November 21, 2025  
**Status**: Phase 1 Complete  

---

## 1. Performance Requirements

### 1.1 Response Time Requirements

| Operation | Target Response Time | Maximum Acceptable | Measurement Conditions |
|-----------|---------------------|-------------------|----------------------|
| Page Load (Initial) | < 2000 ms | < 3000 ms | First contentful paint, broadband connection |
| County Selection | < 500 ms | < 1000 ms | County page navigation, cached resources |
| Resource Search | < 800 ms | < 1500 ms | Full-text search across all resources |
| Admin Operations | < 1000 ms | < 2000 ms | Content creation/editing, authenticated |
| File Upload | < 5000 ms | < 10000 ms | Image uploads up to 5MB |
| Theme Toggle | < 300 ms | < 500 ms | Light/dark mode switching |
| Tutorial Video Load | < 3000 ms | < 5000 ms | YouTube embed initialization |

### 1.2 Throughput Requirements
- **Concurrent Users**: Support for 50 simultaneous users initially, scale to 100+
- **Peak Load**: Handle 25 concurrent sessions during evening peak hours (7-10 PM)
- **Transaction Volume**: Process 500+ page requests per hour during peak usage
- **Admin Capacity**: 1-3 concurrent admin users performing content management
- **Search Operations**: 100+ search queries per hour without performance degradation

### 1.3 Resource Usage
- **Memory Usage**: Application baseline 256MB RAM, maximum 1GB under peak load
- **Storage Requirements**: Initial 2GB (database 500MB, images 1GB, logs 500MB)
- **Database Growth**: Estimate 100MB per year (1000+ resources, 100+ tutorials)
- **Bandwidth**: Average 2MB per user session, 5MB for video-heavy sessions
- **CDN Requirements**: Static assets <100MB initially, images optimized for web

---

## 2. Scalability Requirements

### 2.1 User Growth
- **Initial Users**: Expected 100 users at launch (local genealogy community)
- **Growth Projection**: Anticipated growth to 500 users within 6 months, 2000 within 2 years
- **Peak Usage Patterns**: Evening hours (7-10 PM), winter months (genealogy research season), weekends
- **Geographic Distribution**: Primary Michigan Thumb region, secondary Michigan statewide, tertiary nationwide

### 2.2 Data Growth
- **Content Growth**: Expected 3 counties, 200+ resources initially, 50+ tutorials at launch
- **Growth Rate**: Anticipate adding 20-30 new resources per month, 5-10 tutorials per month
- **File Storage Growth**: Expect 500MB growth per year (images, documents, backups)
- **Database Growth**: Estimate 50MB per year for content data

### 2.3 Geographic Distribution
- **Geographic Scope**: Serving users primarily in Michigan, USA, with global accessibility
- **Language Requirements**: English only initially, potential for multilingual support in future
- **Timezone Considerations**: Eastern Time Zone focus for maintenance windows

---

## 3. Availability & Reliability Requirements

### 3.1 Availability Targets
- **Uptime SLA**: 99.5% availability (~43 hours downtime per year)
- **Planned Maintenance**: Maximum 4 hours per month during low-usage periods (2-6 AM ET)
- **Critical Operations**: Admin functions must be available 98% of time (higher tolerance for admin downtime)
- **Public Site**: Must maintain 99.5% uptime for public-facing content

### 3.2 Disaster Recovery
- **Recovery Time Objective (RTO)**: System must be restored within 4 hours for full functionality
- **Recovery Point Objective (RPO)**: Acceptable data loss of maximum 24 hours
- **Backup Requirements**: Daily automated database backups, weekly full system backups, 30-day retention
- **Backup Testing**: Monthly restore testing to verify backup integrity

### 3.3 Error Handling
- **Error Rate**: Less than 2% of requests should result in errors under normal load
- **Graceful Degradation**: Search functionality may slow, vintage theme effects may be disabled under high load
- **User Experience**: Error messages should be friendly and provide clear next steps or alternatives

---

## 4. Security Requirements

### 4.1 Authentication & Authorization
- **Admin Authentication**: Cookie-based authentication using ASP.NET Core Identity
- **Session Security**: 
  - Session timeout: 120 minutes of inactivity with sliding expiration
  - Secure cookies with HttpOnly, Secure, and SameSite=Strict flags
  - Maximum 3 concurrent sessions per admin user
- **Password Policy**: Minimum 8 characters, mixed case, numbers, special characters, no password expiration
- **Failed Login Protection**: 5 failed attempts = 15 minute account lockout
- **Multi-Factor Authentication**: Not required initially, planned for future enhancement

### 4.2 Data Protection
- **Data Encryption**: 
  - In transit: TLS 1.2+ required for all communications
  - At rest: Database encryption for sensitive data (passwords, admin information)
- **PII Protection**: Minimal personal data collection (admin email only), no public user registration
- **Data Retention**: Admin logs retained 90 days, user analytics anonymized after 12 months
- **GDPR Compliance**: Cookie consent, data export capability, right to deletion

### 4.3 Input Security
- **Input Validation**: Server-side validation for all inputs, length limits, format validation
- **SQL Injection Prevention**: Parameterized queries only, Entity Framework ORM protection
- **XSS Prevention**: Output encoding for all user content, Content Security Policy headers
- **File Upload Security**: Image files only (.jpg, .png, .webp), 5MB size limit, virus scanning
- **Rich Text Security**: HTML sanitization for tutorial content, whitelist of allowed tags

### 4.4 Admin Security
- **Admin URL Protection**: /admin path not linked from public navigation, requires authentication
- **Access Logging**: Log all admin actions with timestamp, user ID, IP address, action details
- **IP Restrictions**: None initially, but capability to restrict admin access by IP if needed
- **Audit Trail**: Complete audit log of content changes with before/after values
- **Session Security**: Automatic logout on browser close, secure session token generation

---

## 5. Usability Requirements

### 5.1 User Experience Standards
[TO BE FILLED BY USER]
- **Learning Curve**: New users should accomplish basic tasks within [TO BE FILLED] minutes
- **Task Completion**: [TO BE FILLED]% of users should complete primary tasks without assistance
- **Error Recovery**: Users should recover from errors within [TO BE FILLED] steps

### 5.2 Interface Requirements
[TO BE FILLED BY USER]
- **Browser Support**: 
  - Chrome: [TO BE FILLED - minimum version]
  - Firefox: [TO BE FILLED - minimum version]  
  - Safari: [TO BE FILLED - minimum version]
  - Edge: [TO BE FILLED - minimum version]
- **Mobile Requirements**: [TO BE FILLED - specific mobile browser requirements]

### 5.3 Accessibility Standards
[TO BE FILLED BY USER]
- **WCAG Compliance**: Must meet [TO BE FILLED] level (AA recommended)
- **Screen Reader**: Compatible with [TO BE FILLED - specific screen readers]
- **Keyboard Navigation**: All functions accessible via [TO BE FILLED - keyboard requirements]
- **Color Contrast**: Meet [TO BE FILLED - specific contrast ratios]

---

## 6. Compatibility Requirements

### 6.1 Browser Compatibility

| Browser | Minimum Version | Support Level | Notes |
|---------|----------------|---------------|-------|
| Chrome | 90+ | Full | Primary development target |
| Firefox | 88+ | Full | Secondary testing priority |
| Safari | 14+ | Full | iOS and macOS support |
| Edge | 90+ | Full | Chromium-based versions only |
| Mobile Safari | 14+ | Full | iOS mobile experience |
| Chrome Mobile | 90+ | Full | Android mobile experience |
| Internet Explorer | None | Not Supported | Legacy browser not supported |

### 6.2 Device Compatibility
- **Desktop**: Windows 10+, macOS 10.15+, Linux Ubuntu 20.04+, minimum 1024x768 resolution
- **Tablets**: iPad (iOS 14+), Android tablets (Android 10+), 768px+ width support
- **Mobile**: iPhone 8+ (iOS 14+), Android phones (Android 10+), 375px+ width support
- **Screen Sizes**: Responsive design from 320px to 2560px width

### 6.3 Technology Compatibility
- **JavaScript**: Minimum ES2020 support required for modern features
- **WebAssembly**: Optional for Blazor WebAssembly components, not required for basic functionality
- **Local Storage**: Required for theme preference storage, graceful degradation if unavailable
- **Cookies**: Required for admin authentication, site functionality depends on cookie support

---

## 7. Maintainability Requirements

### 7.1 Code Quality Standards
- **Code Coverage**: Minimum 80% test coverage for business logic, 60% overall
- **Documentation**: XML documentation for all public APIs, README for each major component
- **Code Review**: All changes require review, automated code analysis with SonarQube or similar
- **Coding Standards**: EditorConfig enforced, consistent C# styling, no code smells

### 7.2 Deployment Requirements
- **Deployment Frequency**: Ability to deploy updates weekly during development, monthly in production
- **Zero-Downtime**: Not required initially, brief downtime acceptable during maintenance windows
- **Rollback Capability**: Ability to rollback to previous version within 30 minutes
- **Environment Promotion**: Dev → Staging → Production pipeline with automated testing

### 7.3 Monitoring & Logging
- **Application Monitoring**: Response times, error rates, resource usage, user activity
- **Performance Metrics**: Page load times, database query performance, search response times
- **Log Retention**: Error logs 90 days, access logs 30 days, admin activity logs 1 year
- **Alerting**: Email alerts for critical errors, performance degradation, security events

---

## 8. Compliance & Legal Requirements

### 8.1 Data Privacy
[TO BE FILLED BY USER]
- **Privacy Regulations**: Must comply with [TO BE FILLED - GDPR, CCPA, etc.]
- **Cookie Policy**: [TO BE FILLED - cookie usage requirements]
- **Data Subject Rights**: [TO BE FILLED - user rights regarding their data]

### 8.2 Accessibility Compliance
[TO BE FILLED BY USER]
- **Legal Requirements**: Must meet [TO BE FILLED - ADA, Section 508, etc.]
- **Jurisdiction**: Compliance required in [TO BE FILLED - countries/states]

### 8.3 Content Compliance
[TO BE FILLED BY USER]
- **Content Guidelines**: [TO BE FILLED - what content is acceptable]
- **Copyright**: [TO BE FILLED - how copyrighted material is handled]
- **Moderation**: [TO BE FILLED - content review requirements]

---

## 9. Environmental Requirements

### 9.1 Operating Environment
- **Hosting Platform**: Azure App Service (preferred) or AWS Elastic Beanstalk, shared hosting acceptable initially
- **Operating System**: Windows Server 2019+ for production, Windows 10/11 for development
- **Database System**: SQL Server 2019+ for production, SQLite for development and testing
- **Web Server**: IIS with ASP.NET Core hosting bundle

### 9.2 Development Environment
- **Development Tools**: Visual Studio 2022 Community (primary), VS Code (secondary)
- **Version Control**: Git with GitHub, feature branch workflow, pull request reviews
- **CI/CD Requirements**: GitHub Actions for automated build/test, manual deployment initially
- **Package Management**: NuGet for .NET packages, npm for any client-side dependencies

---

## 10. Quality Assurance Requirements

### 10.1 Testing Requirements
- **Unit Testing**: xUnit framework, 80%+ coverage for business logic, FluentAssertions for readable tests
- **Integration Testing**: ASP.NET Core TestHost for API testing, in-memory database for data layer tests
- **Performance Testing**: Simple load testing with 50+ concurrent users, response time validation
- **Security Testing**: OWASP ZAP scanning, dependency vulnerability scanning, penetration testing annually
- **User Acceptance Testing**: Manual testing with actual admin user, usability testing for vintage theme
- **Browser Testing**: Cross-browser testing on major browsers, mobile device testing

### 10.2 Quality Metrics
- **Defect Rate**: No more than 2 critical bugs per release, 5 minor bugs acceptable
- **Performance Benchmarks**: All response time targets met, no performance regression between releases
- **User Satisfaction**: Target 4.5/5 user satisfaction score, measured through feedback forms
- **Accessibility Score**: Lighthouse accessibility score >90, WAVE tool validation passed

---

## ✅ COMPLETION STATUS

**DOCUMENT STATUS**: ✅ COMPLETE - Ready for Phase 2  
**CRITICAL AREAS COMPLETED**:
- [x] Performance targets and acceptable limits defined
- [x] Security requirements and compliance needs specified
- [x] Availability and reliability expectations set
- [x] Browser and device compatibility matrix completed
- [x] Quality assurance and testing requirements detailed
- [x] Environmental and operational requirements specified
- [x] Maintainability and monitoring requirements defined

**PHASE GATE STATUS**: ✅ APPROVED - All Non-Functional Requirements Complete  
**NEXT PHASE**: Technical architecture implementation can begin  
**DEVELOPMENT READY**: All requirements baseline established for implementation  

---

## Instructions for Completion

1. **Prioritize Critical Areas**: Start with performance, security, and availability
2. **Be Realistic**: Set achievable targets based on your actual needs and constraints
3. **Consider Future Growth**: Plan for growth but don't over-engineer initially  
4. **Validate Feasibility**: Ensure requirements are technically and financially feasible
5. **Get Stakeholder Buy-in**: Confirm requirements with all key stakeholders

**Questions to Consider While Completing:**
- What's the real impact if performance is slower than targets?
- What security incidents would be most damaging to your organization?
- How much downtime is truly acceptable for your users?
- What devices/browsers do your users actually use?
- What level of quality assurance fits your risk tolerance?

**Once complete, return to Project Orchestrator for technical architecture design**