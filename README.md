# Story Platform – Clean Architecture + CQRS (Beta)

This is an API-driven storytelling platform built using **Clean Architecture**, **CQRS**, **MediatR**, and **JWT Authentication**, with a **Blazor Server-Side** frontend.

The platform allows users to create, manage, and publish interactive stories, while also providing a full moderation workflow, reporting system, notifications, and admin review tools.

---

## Purpose of the Project
The original version of this system was much larger, but I needed a smaller and more manageable project to strengthen my understanding of architecture, domain separation, and practical backend structure.

This project became that foundation — a fully functional, realistic platform that demonstrates real-world patterns:  
- Command/Query separation (CQRS)  
- API-first backend  
- Token-based authorization  
- Clear layering and boundaries  
- Practical moderation workflow

---

## Core Features

### Backend Architecture
- Clean Architecture with strict layer separation  
- CQRS pattern implemented using **MediatR**  
- API-driven structure with clear boundaries  
- JWT authentication for all secure endpoints  
- Custom middleware for injecting tokens into requests  

### Story System
- Users can create, edit, and manage stories  
- Stories are divided into narration-based sections  
- Authors can publish or unpublish their stories anytime  
- Readers can rate and comment on published stories  

### Moderation & Reporting
- Users can report any story or comment  
- Admins can review all reports  
- Accepted comment reports → comment is removed  
- Accepted story reports → story gets a **strike** and becomes hidden  
- Admins can view strikes and warnings for each user  
- Authors can request to restore corrected stories  

### Notification System
- Users are notified when their reports are accepted or rejected  
- Notifications currently load without pagination (improvement planned)

---

## Current Limitations (Because This Is a Beta)

### No Formal Validation Layer Yet
- DTO validation is not implemented  
- Only basic null-checks exist  
- Full validation (likely FluentValidation) will be added in v0.2  

### No Pagination
- All queries return full datasets  
- This affects story lists, comments, and notifications  
- Proper pagination will be a major focus of the next update  

### Blazor Code Needs Refactor
- Working, but not structurally ideal  
- Needs cleanup, state restructuring, and logic separation  

### Media Features Not Yet Implemented
- No image support for story sections  
- No background music system (designed but not implemented)  

---

## Planned for Next Update (v0.2)
- Add full pagination to all major endpoints  
- Implement a complete validation layer for all DTOs  
- Refactor Blazor project structure  
- Improve request handling and state management  
- Begin work on media features (images & audio)

---

## Frontend – Blazor Server-Side
- JWT stored in cookies  
- Custom middleware adds the token to every outgoing request  
- Server-side rendering with clean API communication  

---

## Status
Developed by MahdiDevKo
