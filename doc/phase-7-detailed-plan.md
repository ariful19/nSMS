# Phase 7 Detailed Plan — Testing & QA

## Objectives
- Deliver a comprehensive testing strategy covering all implemented modules (Auth, Students, Teachers, Academics, Fees, Library, Communications)
- Implement automated tests for critical user flows and business logic
- Validate end-to-end functionality through integration tests
- Ensure the application is production-ready with proper error handling and validation
- Verify security measures (authentication, authorization, CSRF protection)
- Document testing procedures and standards for future development

## Scope & Deliverables

### 1. Automated Testing Infrastructure
- **Unit Tests**: Test individual functions, utilities, and service methods
- **Integration Tests**: Test controller endpoints with database interactions
- **End-to-End Tests**: Test complete user workflows across multiple pages
- **Security Tests**: Validate authentication, authorization, and CSRF protection
- **Data Validation Tests**: Ensure form inputs are properly validated and sanitized

### 2. Test Coverage Goals
- **Authentication & Authorization**: 
  - Login/logout flows
  - Session management
  - Role-based access control (Admin, Staff, Teacher, Student)
  - CSRF token generation and validation
  - Password hashing and verification
  
- **Student Management**:
  - List students with filtering
  - Create new student records
  - Update student information
  - View student details
  - Delete student records
  - Validate required fields and data integrity
  
- **Teacher/Staff Management**:
  - List teachers/staff with filtering
  - Create new teacher/staff records
  - Update employment information
  - View teacher/staff details
  - Delete teacher/staff records
  
- **Academic Management**:
  - Create and manage classes/classrooms
  - Assign students to classes (enrollment)
  - Create subjects and assign to classes
  - Enter and update grades/assessments
  - Generate academic reports
  
- **Fees Management**:
  - Record fee charges
  - Process fee payments
  - View student fee ledger
  - Calculate balances
  - Generate fee reports/receipts
  
- **Library Management**:
  - Manage book catalog (CRUD operations)
  - Manage book copies and inventory
  - Issue books to students
  - Return books and update status
  - Track overdue loans
  - Search and filter books
  
- **Communications (Notices & Events)**:
  - Create, edit, and delete notices
  - Publish and schedule notices
  - Manage notice audiences by role
  - Create, edit, and delete events
  - RSVP to events
  - Filter events by date and visibility

### 3. Testing Types and Approach

#### A. Unit Tests (Service Layer)
Focus on business logic without HTTP concerns:
- Student service: enrollment logic, data transformations
- Teacher service: employment status calculations
- Academic service: grade calculations, GPA computations
- Fee service: balance calculations, payment processing
- Library service: loan duration, overdue calculations
- Notice/Event service: scheduling logic, audience filtering

#### B. Integration Tests (Controller + Database)
Test HTTP endpoints with actual database operations:
- Request/response validation
- Database transactions
- Error handling
- Flash messages
- Redirects and status codes
- Session state management

#### C. Authentication & Authorization Tests
- Unauthenticated access attempts
- Role-based route protection
- Session expiration
- CSRF token validation
- Login with invalid credentials
- Logout and session cleanup

#### D. End-to-End Workflow Tests
Complete user journeys across multiple pages:
1. **Student Enrollment Workflow**:
   - Admin logs in → Navigates to students → Creates new student → Assigns to class → Enters grades → Views report
   
2. **Fee Payment Workflow**:
   - Admin logs in → Navigates to fees → Selects student → Records charge → Records payment → Views ledger
   
3. **Library Loan Workflow**:
   - Librarian logs in → Adds book → Adds copy → Issues to student → Returns book
   
4. **Communication Workflow**:
   - Admin logs in → Creates notice → Publishes to specific roles → Users view notice
   - Admin creates event → Teacher RSVPs to event

#### E. Data Validation Tests
- Required field validation
- Data type validation (dates, numbers, emails)
- Unique constraints (student numbers, employee numbers, ISBNs)
- Foreign key constraints
- Business rule validation (e.g., end date after start date)

#### F. Error Handling Tests
- 404 Not Found pages
- 500 Internal Server Error handling
- Database connection errors
- Invalid form submissions
- Unauthorized access attempts
- CSRF token mismatches

### 4. Test Implementation Plan

#### Phase 7.1: Expand Unit Test Coverage
**Goal**: Achieve comprehensive coverage of service layer business logic

Tasks:
1. Create additional service tests:
   - `tests/student.service.spec.js` - Student business logic
   - `tests/teacher.service.spec.js` - Teacher/staff business logic
   - `tests/fee.service.spec.js` - Fee calculation logic
   - `tests/library.service.spec.js` - Library loan logic
   - `tests/notice.service.spec.js` - Notice scheduling logic
   - `tests/event.service.spec.js` - Event RSVP logic

2. Test utility functions:
   - `tests/utils/slug.spec.js` - Slug generation
   - `tests/utils/viewHelpers.spec.js` - View helper functions

#### Phase 7.2: Enhance Integration Tests
**Goal**: Complete controller test coverage with database interactions

Tasks:
1. Expand existing controller tests:
   - Add more test cases to `tests/students.controller.spec.js`
   - Add more test cases to `tests/teachers.controller.spec.js`
   - Add more test cases to `tests/fees.controller.spec.js`
   - Add more test cases to `tests/library.controller.spec.js`
   - Add more test cases to `tests/notices.controller.spec.js`
   - Add more test cases to `tests/events.controller.spec.js`

2. Create new controller tests:
   - `tests/auth.controller.spec.js` - Authentication flows
   - `tests/class.controller.spec.js` - Classroom management
   - `tests/report.controller.spec.js` - Report generation

3. Test scenarios to cover:
   - Happy path (successful operations)
   - Validation errors
   - Not found errors
   - Unauthorized access
   - Database constraint violations
   - Concurrent operations

#### Phase 7.3: Authentication & Security Tests
**Goal**: Ensure robust security across all endpoints

Tasks:
1. Create comprehensive auth tests:
   - `tests/auth/login.spec.js` - Login scenarios
   - `tests/auth/logout.spec.js` - Logout scenarios
   - `tests/auth/session.spec.js` - Session management
   - `tests/auth/csrf.spec.js` - CSRF protection
   - `tests/auth/authorization.spec.js` - Role-based access

2. Test middleware:
   - `tests/middleware/ensureAuth.spec.js`
   - `tests/middleware/ensureRole.spec.js`
   - `tests/middleware/ensureAnyRole.spec.js`
   - `tests/middleware/attachUser.spec.js`

#### Phase 7.4: End-to-End Workflow Tests
**Goal**: Validate complete user journeys using Playwright or Supertest sequences

Tasks:
1. Set up E2E testing framework (optional Playwright integration)
2. Create workflow test suites:
   - `tests/e2e/student-enrollment.spec.js`
   - `tests/e2e/fee-payment.spec.js`
   - `tests/e2e/library-loan.spec.js`
   - `tests/e2e/communication-flow.spec.js`

3. If using Supertest only (simpler approach):
   - Create multi-step test sequences
   - Maintain session state across requests
   - Verify data persistence

#### Phase 7.5: Data Validation & Edge Cases
**Goal**: Ensure robust input validation and error handling

Tasks:
1. Test form validations:
   - Empty required fields
   - Invalid data formats
   - Out-of-range values
   - SQL injection attempts
   - XSS attempts

2. Test edge cases:
   - Deleting records with dependencies
   - Concurrent updates to same record
   - Large dataset handling
   - Date boundary conditions
   - Decimal precision in fees

#### Phase 7.6: Performance & Load Testing
**Goal**: Ensure acceptable performance under load (optional)

Tasks:
1. Identify performance-critical endpoints
2. Create basic load tests (if needed)
3. Test with larger datasets
4. Verify pagination works correctly
5. Check query performance

### 5. Manual Testing & QA

#### Update Manual QA Checklist
- Review and enhance existing `doc/manual-qa-checklist.md`
- Add test cases for missed scenarios
- Include regression testing guidelines
- Document known limitations or issues

#### Exploratory Testing
- Test browser compatibility
- Test responsive design on different screen sizes
- Test with JavaScript disabled (progressive enhancement)
- Test accessibility features
- Test with different user roles simultaneously

### 6. Test Execution & CI/CD Integration

#### Local Development
- Run tests before committing: `npm test`
- Run specific test suites: `npm test -- <pattern>`
- Watch mode for TDD: `npm test -- --watch`

#### Continuous Integration
- Configure automated test runs on push/PR
- Set up test coverage reporting
- Define minimum coverage thresholds
- Configure test databases for CI environment

### 7. Documentation

#### Test Documentation
- Document testing philosophy and approach
- Create test writing guidelines
- Document test data setup and teardown
- Provide examples of good test patterns

#### Test Reports
- Generate coverage reports
- Document test results
- Track test metrics over time
- Identify areas needing more coverage

### 8. Quality Assurance Standards

#### Code Quality
- Follow consistent test naming conventions
- Use descriptive test names that explain the scenario
- Arrange-Act-Assert pattern for tests
- Avoid test interdependencies
- Clean up test data after each test

#### Test Data Management
- Use factories or fixtures for test data
- Seed consistent test data
- Isolate test database from development
- Reset database between test suites

#### Error Messages
- Validate error message content
- Ensure user-friendly error messages
- Test error recovery mechanisms

## Workstreams & Sequence

### Week 1: Foundation
1. Review existing tests and identify gaps
2. Create Phase 7 test plan document (this document)
3. Set up additional test utilities and helpers
4. Implement core service unit tests

### Week 2: Integration Testing
1. Expand controller integration tests
2. Add authentication and security tests
3. Test all CRUD operations comprehensively
4. Validate error handling

### Week 3: Workflow & Validation
1. Implement end-to-end workflow tests
2. Add comprehensive data validation tests
3. Test edge cases and error scenarios
4. Performance testing (if applicable)

### Week 4: QA & Documentation
1. Execute manual QA checklist
2. Fix identified bugs and issues
3. Update documentation
4. Generate final test reports
5. Prepare for Phase 8 (deployment)

## Dependencies & Inputs
- Completed Phases 1-6 (all modules implemented)
- Test database (SQLite for testing)
- Jest testing framework
- Supertest for HTTP testing
- Optional: Playwright for browser-based E2E tests

## Definition of Done
- ✅ Minimum 80% code coverage for service layer
- ✅ All controller endpoints have integration tests
- ✅ Authentication and authorization fully tested
- ✅ All CRUD operations validated
- ✅ Critical user workflows have E2E tests
- ✅ Manual QA checklist passed for all modules
- ✅ No critical or high-priority bugs remaining
- ✅ Test documentation complete
- ✅ CI/CD pipeline configured (if applicable)
- ✅ Application ready for production deployment

## Risks & Mitigations

### Risk: Insufficient Test Coverage
**Mitigation**: 
- Define minimum coverage thresholds
- Review coverage reports regularly
- Prioritize testing critical business logic

### Risk: Test Database State Issues
**Mitigation**: 
- Implement proper test data cleanup
- Use transactions for test isolation
- Create dedicated test database

### Risk: Flaky Tests
**Mitigation**: 
- Avoid timing-dependent tests
- Mock external dependencies
- Use deterministic test data

### Risk: Testing Takes Too Long
**Mitigation**: 
- Parallelize test execution
- Use test database in memory where possible
- Focus on critical path testing first

## Success Metrics
- Test suite execution time < 5 minutes
- Zero critical bugs in production
- All modules verified working end-to-end
- Positive user acceptance testing feedback
- Clean security audit results

## Next Steps (Phase 8)
After Phase 7 completion, proceed to Phase 8:
- Performance optimization
- Production deployment preparation
- Documentation finalization
- User training materials
- Handover and support planning
