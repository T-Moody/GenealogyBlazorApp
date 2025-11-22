# Business Requirements - Thumb of Michigan Genealogy

## Project Information
**Project Name**: Thumb of Michigan Genealogy  
**Project Code**: TMG-2025  
**Document Version**: 1.0  
**Document Date**: November 21, 2025  
**Document Owner**: T-Moody  
**Approval Status**: DRAFT - Phase 1 Requirements  

---

## 1. Executive Summary

### 1.1 Project Purpose
A modern, user-friendly web application that empowers everyday people to learn genealogy research skills independently, focusing on Michigan's "Thumb" region (Huron, Tuscola, Sanilac counties). The app teaches genealogy skills rather than providing genealogy services.

### 1.2 Business Objectives
- **Teach, Don't Do**: Guide users to become self-sufficient researchers
- **Regional Focus**: Specialize in Michigan Thumb region genealogy resources
- **Admin Empowerment**: Enable non-technical content management
- **Educational Excellence**: Provide high-quality, structured learning paths

### 1.3 Success Criteria
- Users can independently conduct genealogy research after using tutorials and resources
- 500+ active users within 6 months
- 60%+ tutorial completion rate
- 4.5/5 average user satisfaction rating

---

## 2. Stakeholder Analysis

### 2.1 Primary Stakeholders
| Role | Name | Responsibilities | Contact |
|------|------|------------------|---------|
| Project Owner | T-Moody | Final decision authority, vision oversight | Active |
| Content Admin | Mom (Non-technical user) | Daily content management, tutorial creation | Primary User |
| Genealogy Learners | Regional Users | Learn research skills, access resources | Target Audience |
| Regional Researchers | Thumb Area Residents | Find county-specific genealogy resources | Target Audience |

### 2.2 Stakeholder Needs
**Content Admin Needs:**
- Simple, intuitive content management interface
- Ability to manage tutorials, resources, categories without technical knowledge
- Preview functionality before publishing
- Easy YouTube video embedding

**Genealogy Learner Needs:**
- Clear, step-by-step learning guidance
- Reliable, up-to-date external resources
- Mobile-friendly access
- Search and filtering capabilities

**Regional Researcher Needs:**
- County-specific resources (Huron, Tuscola, Sanilac)
- Local historical context and guidance
- Curated links to relevant archives and databases

---

## 3. Business Context

### 3.1 Current State
- Genealogy resources scattered across multiple websites and databases
- No centralized regional resource for Michigan Thumb area
- Learning genealogy requires navigating complex, overwhelming information
- Non-technical content managers struggle with website updates
- Users often give up due to information overload and lack of guidance

### 3.2 Desired Future State  
- Centralized, curated genealogy learning platform for Michigan Thumb region
- Clear learning paths from beginner to independent researcher
- Easy content management for non-technical administrators
- Mobile-friendly access to tutorials and resources
- Community of learners who can successfully conduct their own research

### 3.3 Business Drivers
- **Educational Mission**: Help preserve family history through education
- **Regional Specialization**: Fill gap in Michigan Thumb genealogy resources
- **Accessibility**: Make genealogy learning approachable for everyday people
- **Sustainability**: Enable non-technical content management for long-term viability

---

## 4. Scope Definition

### 4.1 In Scope
**Core Features (MVP):**
- [x] Public homepage with hero section and about content
- [x] Tutorial management (create, edit, delete, categorize)
- [x] Resource management (external links, descriptions, county assignment)
- [x] Category management for organizing content
- [x] County-specific pages (Huron, Tuscola, Sanilac)
- [x] Admin authentication and content management interface
- [x] YouTube video embedding in tutorials
- [x] Mobile-responsive design using Bootstrap 5
- [x] Search and filtering functionality
- [x] SEO-friendly URL structure

### 4.2 Out of Scope
**Not Included in MVP:**
- User registration/accounts for public users
- Multi-admin user management
- Content approval workflows
- Social sharing features
- Comments or community features
- Genealogy tree building tools
- Document storage/hosting
- Paid premium features
- Multi-language support
- Advanced analytics dashboard

### 4.3 Future Considerations
**Potential Phase 2+ Features:**
- User accounts with learning progress tracking
- Community features (comments, forums)
- Additional Michigan counties
- Content collaboration features
- Advanced search with full-text indexing
- Email newsletter integration
- Mobile app version

---

## 5. Business Rules

### 5.1 Administrative Rules
**Admin Access Control:**
- Single admin user initially (expandable to multiple admins later)
- Admin access via hidden URL (/admin) not linked from public navigation
- No content approval workflow - admin changes are immediately published
- Admin can create, edit, delete all content types (tutorials, resources, categories)

**Content Guidelines:**
- All tutorial content must be educational, not commercial
- External resource links must be genealogy-related and family-friendly
- YouTube videos must be appropriate for general audiences
- Content should focus on research methods, not specific family histories

### 5.2 Public Access Rules
**Content Access:**
- All public content freely accessible without registration
- No user accounts or login required for public users
- Search functionality available to all visitors
- External resource links tracked for analytics but unrestricted

**Usage Guidelines:**
- Educational use encouraged and unrestricted
- No commercial use of aggregated content
- Users responsible for verifying accuracy of external resources
- Mobile and desktop access fully supported

### 5.3 Data Management Rules
**County Organization:**
- Three primary counties: Huron, Tuscola, Sanilac
- "General" category for multi-county or region-wide resources
- Counties can have multiple resource categories (census, vital records, etc.)

**Resource Types:**
- External website links (primary resource type)
- YouTube video tutorials (embedded)
- PDF documents (linked, not hosted)
- Archive and database links
- Historical society and library resources

**Data Quality:**
- Monthly link validation for external resources
- Broken link reporting and cleanup
- Content review quarterly for accuracy and relevance
- User feedback mechanism for reporting issues

---

## 6. Constraints & Assumptions

### 6.1 Technical Constraints
**Technology Stack (Non-negotiable):**
- Must use .NET 10 and Blazor United (Auto render mode)
- Bootstrap 5 for styling (minimal custom CSS)
- Entity Framework Core for data access
- Code-behind files (.razor.cs) for all Blazor components
- Vertical slice architecture implementation

**Infrastructure Constraints:**
- Single-server deployment initially
- SQLite for development, SQL Server for production
- No CDN initially (local asset serving)
- Standard web hosting (no specialized genealogy hosting)

### 6.2 Business Constraints
**Budget Constraints:**
- Minimal hosting costs (shared/basic hosting acceptable)
- No premium third-party services initially
- Open source tools and libraries preferred
- No paid genealogy database subscriptions

**Resource Constraints:**
- Single developer (T-Moody) for technical implementation
- Single content manager (non-technical admin user)
- No dedicated QA or testing resources
- Part-time development schedule

**Timeline Constraints:**
- No hard launch deadline (when ready approach)
- MVP target: 3-4 months for basic functionality
- Content population concurrent with development
- Iterative releases preferred over big-bang launch

### 6.3 Assumptions
**User Assumptions:**
- Admin user has basic computer skills (email, web browsing)
- Public users have internet access and basic web navigation skills
- Target audience primarily English-speaking
- Users interested in learning, not hiring genealogy services

**Technical Assumptions:**
- Modern browsers (Chrome, Firefox, Safari, Edge - last 2 versions)
- Adequate internet bandwidth for video content
- JavaScript enabled in browsers
- Mobile usage will be significant (40%+ of traffic)

**Business Assumptions:**
- Regional focus will attract sufficient user base
- External genealogy resources will remain accessible
- Educational approach will differentiate from commercial genealogy sites
- Word-of-mouth and organic search will drive initial traffic

---

## 7. Risk Analysis

### 7.1 Business Risks

| Risk | Impact | Probability | Mitigation Strategy |
|------|--------|-------------|-------------------|
| **Content Quality Degradation** | High | Medium | Quarterly content reviews, user feedback system, expert validation |
| **Admin User Abandonment** | High | Low | Simple interface design, comprehensive documentation, backup admin training |
| **External Link Rot** | High | High | Automated link checking, alternative resource identification, archive.org fallbacks |
| **Limited User Adoption** | Medium | Medium | SEO optimization, social media presence, partnership with local genealogy groups |
| **Competing Sites** | Medium | Medium | Focus on regional specialization, unique educational approach, community building |
| **Content Volume Insufficient** | Medium | Medium | Phased content development, crowdsourced suggestions, partnership content |
| **Legal/Copyright Issues** | Medium | Low | Clear content guidelines, proper attribution, avoid hosted content |
| **Technical Maintenance Burden** | Medium | Medium | Simple architecture, automated testing, documentation, monitoring |

### 7.2 Technical Risks

| Risk | Impact | Probability | Mitigation Strategy |
|------|--------|-------------|-------------------|
| **Technology Stack Obsolescence** | Medium | Low | .NET LTS versions, standard web technologies, migration planning |
| **Performance Issues** | Medium | Medium | Performance testing, caching strategy, database optimization |
| **Security Vulnerabilities** | High | Medium | Security best practices, regular updates, input validation, HTTPS |
| **Data Loss** | High | Low | Regular backups, version control, database replication |
| **Third-party Integration Failures** | Medium | Medium | Graceful degradation, alternative providers, monitoring |
| **Scalability Limitations** | Low | Medium | Cloud-ready architecture, performance monitoring, scaling plan |
| **Browser Compatibility** | Low | Low | Modern browser focus, progressive enhancement, testing matrix |

---

## 8. Success Metrics

### 8.1 Key Performance Indicators (KPIs)

**User Engagement Metrics:**
- 500+ unique monthly visitors within 6 months
- 60%+ tutorial completion rate (tracked via time spent)
- 40%+ external resource click-through rate
- 3+ pages per session average
- 2+ minutes average session duration

**Content Management Efficiency:**
- Admin can create new tutorial in <5 minutes
- Admin can add new resource in <2 minutes
- Admin updates homepage content monthly
- <5% broken external links at any time
- New content published weekly during active periods

**Educational Impact Metrics:**
- User feedback ratings >4.5/5 for tutorial quality
- User testimonials about successful research outcomes
- Referral traffic from genealogy forums and Facebook groups
- Email inquiries about advanced research topics
- Local genealogy society partnerships established

**Technical Performance:**
- <3 second page load times
- 99%+ uptime (excluding planned maintenance)
- <2% error rate in user interactions
- Mobile traffic >40% of total visits

### 8.2 Acceptance Criteria

**MVP Launch Criteria:**
- [ ] 25+ tutorials published across major genealogy research topics
- [ ] 100+ resources catalogued across all three counties
- [ ] All core admin functions working and tested
- [ ] Site fully responsive on mobile devices
- [ ] Search functionality operational
- [ ] YouTube video embedding functional
- [ ] Basic SEO implementation complete

**User Acceptance Criteria:**
- [ ] Non-technical admin can manage all content independently
- [ ] Public users can find relevant county resources within 2 clicks
- [ ] Tutorial content is clear and actionable for beginners
- [ ] Site loads quickly on mobile and desktop
- [ ] External links are current and accessible

**Technical Acceptance Criteria:**
- [ ] All automated tests passing (>80% code coverage)
- [ ] Security best practices implemented
- [ ] Database backups automated and tested
- [ ] Error logging and monitoring operational
- [ ] Performance targets met under expected load

---

## 9. Timeline & Milestones

### 9.1 Key Milestones

| Milestone | Description | Target Date | Dependencies |
|-----------|-------------|-------------|-------------|
| **Phase 1: Requirements Complete** | All requirement documents finalized | November 21, 2025 | Stakeholder approval |
| **Phase 2: Core Infrastructure** | Database, authentication, basic structure | December 2025 | Development environment setup |
| **Phase 3: Admin Interface** | Content management functionality | January 2026 | Core infrastructure complete |
| **Phase 4: Public Site** | Public-facing pages and features | February 2026 | Admin interface complete |
| **Phase 5: Content Population** | Initial tutorials and resources added | March 2026 | Public site functional |
| **Phase 6: Testing & Polish** | Bug fixes, performance optimization | March 2026 | Content population started |
| **Phase 7: MVP Launch** | Soft launch with initial user feedback | April 2026 | All core features complete |
| **Phase 8: Full Launch** | Public announcement and marketing | May 2026 | MVP validation complete |

### 9.2 Timeline Constraints
**Flexible Constraints:**
- No hard external deadlines (internal project)
- Part-time development schedule (evenings/weekends)
- Content creation concurrent with development
- Iterative approach with regular stakeholder feedback

**Fixed Constraints:**
- Holiday periods may slow development (Dec 2025, major holidays)
- Admin user availability for testing and feedback
- Seasonal genealogy interest patterns (higher in winter months)

---

## 10. Budget & Resources

### 10.1 Budget Requirements
**Development Costs:** $0 (internal development)
**Hosting Costs:** $10-20/month (shared hosting initially)
**Domain Registration:** $15/year
**SSL Certificate:** $0 (Let's Encrypt)
**Development Tools:** $0 (Visual Studio Community, free tools)
**Third-party Services:** $0 initially (may add analytics later)

**Total Monthly Operating Cost:** <$25
**Total First Year Cost:** <$300

### 10.2 Resource Requirements
**Development Team:**
- 1 Full-stack Developer (T-Moody) - part-time
- 1 Content Manager/Admin (non-technical) - as needed

**External Resources:**
- Web hosting provider (shared hosting initially)
- Domain registrar
- Optional: Local genealogy expert for content validation
- Optional: Designer for vintage theme refinement

**Time Allocation:**
- Development: 10-15 hours/week
- Content creation: 3-5 hours/week
- Testing and feedback: 2-3 hours/week
- Total project time: 4-6 months to MVP

---

## ?? COMPLETION STATUS

**TEMPLATE STATUS**: ? INCOMPLETE - User Input Required  
**NEXT ACTION**: User must fill in all [TO BE FILLED] sections  
**WORKFLOW GATE**: Cannot proceed to technical planning until this document is complete  

---

## Instructions for Completion

1. **Fill in all [TO BE FILLED] sections** with your specific requirements
2. **Review and validate** all stakeholder information  
3. **Confirm scope** and ensure all features are clearly defined
4. **Set realistic timelines** based on your constraints
5. **Mark document as APPROVED** when complete

**Once complete, return to the Project Orchestrator for Phase 2: Technical Planning**