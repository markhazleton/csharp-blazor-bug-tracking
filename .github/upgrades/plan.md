# .NET 10 Upgrade Plan

## Table of Contents
- [Executive Summary](#executive-summary)
- [Implementation Timeline](#implementation-timeline)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Package Update Reference](#package-update-reference)
- [Project-by-Project Plans](#project-by-project-plans)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Risk Management](#risk-management)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

## Executive Summary
### Scenario Overview
Upgrade all .NET 9 projects in the CarvedRock solution to `.NET 10.0 (LTS)` while aligning NuGet dependencies with supported versions and removing deprecated packages.

### Current State Snapshot
| Metric | Value |
| --- | --- |
| Projects | 5 (ASP.NET Core, Blazor Server, Razor Pages, unit tests) |
| Lines of Code | 7,635 |
| Project Dependencies | Depth ? 2, single dependency chain rooted at `CarvedRock.Admin` |
| Packages Requiring Action | 13 updates, 2 deprecated packages, 2 framework-included references |
| API Issues | 4 source incompatible, 3 behavioral changes (Identity + exception handler patterns) |

### Target State
- Every project targets `net10.0` with synchronized SDK-style configurations.
- Deprecated/preview packages (e.g., `Azure.Identity`, `FluentValidation.AspNetCore`) are replaced or removed.
- Shared packages (`Microsoft.Extensions.Caching.Memory`, `System.Text.Json`, EF Core) are unified at 10.0.1.
- Tests and tooling run against .NET 10 SDK with no manual framework references.

### Complexity Classification
**Simple (All-At-Once Candidate)** — only five SDK-style projects, shallow dependency graph, no security advisories, and limited LOC impact (<0.2%).

### Key Findings
1. `CarvedRock.Admin` drives most package churn (EF Core, Identity UI, scaffolding, deprecated Azure/FluentValidation packages).
2. UI stacks (Razor Pages + Blazor Server) depend on updated Identity patterns and new `UseExceptionHandler` behavior in .NET 10.
3. Test project contains framework-in-box assemblies that must be removed during the upgrade.
4. No security vulnerabilities were reported, simplifying concurrent package alignment.

### Recommended Next Step
Proceed with the planning stage using the **All-At-Once strategy** to perform a single atomic upgrade covering target frameworks, package baselines, and shared infrastructure, then validate via coordinated testing.

## Selected Strategy & Iteration Plan
**Strategy:** All-At-Once upgrade of every project and shared dependency in a single atomic operation, followed by holistic validation.

**Justification:**
- Small solution (5 SDK-style projects) with well-understood dependencies.
- Minimal API-breaking surface (4 source, 3 behavioral issues) and no security blockers.
- Coordinated package matrix (EF Core, Identity, System.Text.Json, caching) benefits from simultaneous updates.
- Testing surface is compact (one UI automation suite + API Swagger validation) and can be executed in a single pass.

**Planned Planning Iterations Remaining:**
1. Dependency analysis + migration sequencing details.
2. Project-level specifications and package matrix.
3. Testing, risk, complexity, and success criteria finalization.

## Implementation Timeline
### Phase 0: Preparation
- Verify .NET 10 SDK installation and update `global.json` if present.
- Confirm branch `upgrade-to-NET10` stays up to date with `master` before executing upgrades.
- Inventory tooling dependencies (EF Core tools, `dotnet-ef`, code generation CLI) for .NET 10 compatibility.

### Phase 1: Atomic Upgrade Execution
_Performed as a single coordinated batch across all projects._
1. Update every project `TargetFramework` to `net10.0` (BugTracker UI + tests, CarvedRock Admin/API/Blazor).
2. Align shared package versions to 10.0.1 (EF Core, Identity, System.Text.Json, Microsoft.Extensions libraries).
3. Remove deprecated or in-box packages (`Azure.Identity`, `FluentValidation.AspNetCore`, `System.Net.Http`, `System.Text.RegularExpressions`).
4. Run `dotnet restore` and address any transitive dependency binding redirects.
5. Build entire solution, resolve compilation errors/API changes (Identity builder, `UseExceptionHandler`).
6. Rebuild ensuring 0 errors and no warnings introduced by suppressions.

### Phase 2: Unified Validation
1. Execute `BugTrackerUI.Tests` (xUnit) under .NET 10.
2. Run manual/API validations: Swagger for `CarvedRock.Api`, UI smoke tests for Razor Pages + Blazor Server.
3. Capture telemetry/log review focusing on new ASP.NET Core middleware behavior.
4. Prepare release notes summarizing framework, package, and tooling updates.

## Migration Strategy
### Approach
Adopt an **All-At-Once strategy**—update every project, shared MSBuild import, and tooling dependency to .NET 10 in a single atomic change set.

### Execution Principles
1. **Target Framework Synchronization**: Change `TargetFramework` to `net10.0` in all projects plus any `Directory.Build.props/targets` and shared props files.
2. **Package Matrix Alignment**: Apply the package versions defined in §Package Update Reference simultaneously to avoid transitive mismatches.
3. **Coordinated Build Cycle**:
   - Run `dotnet restore` once repositories reference net10.0.
   - Build entire solution; triage failures in dependency order (Admin ? API/Blazor ? Razor Pages ? Tests).
   - Apply source fixes for Identity builder changes and updated middleware signatures.
   - Rebuild until solution compiles cleanly with zero warnings introduced by the upgrade.
4. **Single Validation Pass**: After build success, execute all automated tests and smoke tests before making any other code changes.

### Dependency-Based Focus Areas
| Order | Projects | Purpose |
| --- | --- | --- |
| 1 | `CarvedRock.Admin` | Update EF Core/Identity stack; ensures downstream compilations succeed. |
| 2 | `CarvedRock.Api`, `CarvedRock.BlazorServerSide` | Consume Admin updates; verify serialization, DI registrations, caching setup. |
| 3 | `BugTrackerUI`, `BugTrackerUI.Tests` | Apply Razor Pages adjustments and unblock tests using updated framework APIs. |

### Parallelism & Coordination
- Perform file edits project-by-project but commit them as one atomic upgrade to ensure no intermediate mismatched states exist.
- Shared assets (e.g., generated JS/CSS bundles) should be rebuilt only after backend upgrades complete, ensuring consistent runtime assemblies.

### Post-Upgrade Hardening
- Validate Identity UI flows end-to-end (registration, login, password reset) because framework upgrades often adjust cookie and antiforgery defaults.
- Review middleware ordering for `UseExceptionHandler` due to behavioral notes in the assessment; adjust startup logic if custom endpoints were used.

## Project-by-Project Plans
### CarvedRock.Admin
- **Current State:** ASP.NET Core admin portal on `net9.0`, heavy EF Core + Identity usage, 132 files, dependants: API & Blazor.
- **Target State:** `net10.0` with EF Core/Identity packages at 10.0.1, deprecated Azure/FluentValidation packages removed.
- **Details:** [To be filled]

### CarvedRock.Api
- **Current State:** ASP.NET Core Web API on `net9.0`, depends on Admin for services; uses Swashbuckle 6.9.0.
- **Target State:** `net10.0` with shared caching and System.Text.Json 10.0.1.
- **Details:** [To be filled]

### CarvedRock.BlazorServerSide
- **Current State:** Blazor Server app on `net9.0`, references Admin for data models, 26 files.
- **Target State:** `net10.0` with Microsoft.Extensions.* packages aligned to 10.0.1 and updated ExceptionHandler behavior.
- **Details:** [To be filled]

### BugTrackerUI
- **Current State:** Razor Pages UI on `net9.0`, 30 files, one dependant test project.
- **Target State:** `net10.0` with ASP.NET Core runtime assemblies (no redundant packages) and updated middleware configuration.
- **Details:** [To be filled]

### BugTrackerUI.Tests
- **Current State:** xUnit test project targeting `net9.0`, references Microsoft.AspNetCore.Components prerelease packages and redundant BCL assemblies.
- **Target State:** `net10.0` with frameworks-in-box references removed and AspNetCore packages at 10.0.1.
- **Details:** [To be filled]

## Risk Management
### High-Level Risk Posture
| Project | Risk | Drivers | Mitigation |
| --- | --- | --- | --- |
| CarvedRock.Admin | Medium | Largest LOC, Identity + EF Core changes, deprecated packages | Prioritize Admin fixes first, run EF migrations/tests immediately after upgrade |
| CarvedRock.Api | Low | Thin API layer, few packages | Validate Swagger + caching behavior post-upgrade |
| CarvedRock.BlazorServerSide | Medium | Depends on Admin, Blazor server lifecycle changes | Regression test navigation/auth flows |
| BugTrackerUI | Low | Standalone Razor Pages | Validate middleware pipeline changes |
| BugTrackerUI.Tests | Low | Package cleanup, test SDK compatibility | Update packages and run entire suite |

### General Mitigations
- Lock package versions using `Directory.Packages.props` (if applicable) or explicit `PackageReference` versions to prevent accidental drift.
- Track Identity API changes noted in assessment; ensure `AddDefaultIdentity` and `AddEntityFrameworkStores` signatures match .NET 10 expectations.
- Remove deprecated packages (`Azure.Identity`, `FluentValidation.AspNetCore`) and replace with supported alternatives or framework features.

## Complexity & Effort Assessment
| Phase | Projects | Relative Complexity | Notes |
| --- | --- | --- | --- |
| Foundation | CarvedRock.Admin | High | Most package churn + Identity/EF updates |
| Service Layer | CarvedRock.Api, CarvedRock.BlazorServerSide | Medium | Dependent on Admin updates; minor code tweaks |
| UI & Tests | BugTrackerUI, BugTrackerUI.Tests | Low | Framework retarget + package cleanup |

Overall effort remains manageable due to consistent project structure, but Admin changes demand focused review/testing.
