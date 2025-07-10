# 📂 SmartBookingSystem -  ASP.NET Core Web API

SmartBookingSystem is a modern, robust, and extensible platform designed to streamline appointment-based service booking. Built with ASP.NET Core and Entity Framework Core, it offers an end-to-end solution that empowers service providers to manage their profiles, services, schedules, and customer interactions — while delivering a smooth and intuitive booking experience for customers. 
Users can browse categorized services, book appointments, react to provider posts, and leave feedback — all within a scalable and future-ready architecture.

---

### 🔧 Technologies Used

- **ASP.NET Core Web API** – for building RESTful endpoints
- **Entity Framework Core** – ORM for database operations
- **ASP.NET Identity** – authentication and role management
- **SQL Server** – primary relational database
- **C#** – backend language using object-oriented principles
- **Domain-Driven Design (DDD)** – architecture pattern

---

### 🧩 Features Implemented

- ✅ Provider & Customer roles via ASP.NET Identity
- ✅ Appointment booking with status tracking (Pending, Done, Canceled)
- ✅ Weekly schedule management per provider
- ✅ Provider profile, image, and geolocation (lat/lng)
- ✅ Service categorization and pricing
- ✅ Provider posts with images and customer emoji reactions
- ✅ Customer feedback & ratings on appointments
- ✅ Clean separation of concerns across layers

---

## 🧾 Domain Entities

| Entity                 | Description |
|------------------------|-------------|
| **ApplicationUser**    | Inherits from `IdentityUser<Guid>` – base for all users |
| **Provider**           | Service provider with profile, services, schedule, posts |
| **Customer**           | End-user who books services and reacts to posts |
| **Appointment**        | Booking entry with customer, provider, service, and feedback |
| **ServiceCategory**    | Grouping mechanism for services |
| **Service**            | Individual service with price and description |
| **WeeklySchedule**     | Defines available time slots per day |
| **ProviderPost**       | Posts created by providers for offers/updates |
| **ProviderPostImage**  | Images attached to provider posts |
| **ProviderPostReaction** | Emoji reaction by a customer to a post |

---

### 🛡️ Role-Based Authorization

- The system supports multiple roles using **ASP.NET Identity**:
  - `Customer`
  - `Provider`
  - `Admin`

Each role has access only to its specific operations and endpoints.  
Users are connected to exactly one role at a time via the `ApplicationUser` entity.

---

### ⚙️ Clean Code Practices

- **Domain-Driven Design (DDD)** structure
-  DTOs for requests and responses.
- AutoMapper for model mapping.
- Repository + Unit of Work for data access abstraction.
- Domain entities separated from the presentation layer.
- Entities separated from infrastructure and services
- Use of `Guid` as primary keys for consistency
- Nullable navigation properties and collection initializations
- Async operations for scalability
- `IsDeleted` support for soft delete
- Auditing fields like `CreatedAt`, `UpdatedAt`, etc.

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) – required to run the project
- SQL Server or LocalDB – for database storage
- Visual Studio 2022+ or Visual Studio Code – for development

### Installation & Run

```bash
git clone https://github.com/sohaibessa797/SmartBookingSystem.API.git
cd SmartBookingSystem.API

# Restore packages
dotnet restore

# Apply migrations
dotnet ef database update

# Run the application
dotnet run
```

---

### 📌 Final Notes

* No frontend included – backend API only.
* Easily integrable with any frontend (Web, Mobile, etc.)
* For production: secure using HTTPS, environment variables, and strict role-based policies.
---
