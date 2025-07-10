![SmartBookingSystem](https://github.com/sohaibessa797/SmartBookingSystem.API/blob/46602ab099119cf771503af8b6bbcfba621c1d74/banner.png)
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

### 🚀 Installation & Run
1. Clone the repository:
```bash
git clone https://github.com/sohaibessa797/SmartBookingSystem.API.git
 ```

2. Update your `appsettings.json` with your database connection string.

3. Apply EF Core migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

5. Use Postman or Swagger to test the endpoints.

---

### 📌 Final Notes

* No frontend included – backend API only.
* Easily integrable with any frontend (Web, Mobile, etc.)
* For production: secure using HTTPS, environment variables, and strict role-based policies.

---

## 🤝 Contributing

Contributions are welcome! Feel free to fork, open issues, or submit pull requests.

---

## 📫 Contact

Created by [@sohaibessa797](https://github.com/sohaibessa797) – feel free to connect!

