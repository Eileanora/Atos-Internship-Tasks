# Pokemon Review System

This project is done as a part of the Atos Internship program, focusing on building a system using ASP.NET Core.

Pokemon Review System is a web API project that:
1. Allows admin to manage Pokemons, and their categories.
2. Allows regular users to register, own, and review Pokemons.

It showcases a clean architecture approach with a multi-layered design. Following SOLID and best practices as much as possible.

## Solution Structure

- **Domain/**: Contains core business models and interfaces.
- **Infrastructure/**: Implements data access using Entity Framework Core, configurations, and repositories.
- **Service/**: Contains business logic, DTOs, validators, and service interfaces.
- **Shared/**: Provides shared helpers, error/result handling, and resource parameter classes.
- **WebApi/**: The API layer exposing RESTful endpoints, handling dependency injection, middleware, and API documentation.

## Features

- CRUD operations for Pokémon, categories, owners, reviews
- Auditing support
- Clean separation of layers (Domain, Infrastructure, Service, API)
- Input validation using **Fluent validation**
- Error handling following the **Result pattern**
- **Repository** and **Unit of Work** patterns for data access
- **JWT authentication** (token and refresh token support) and **Role-based authorization** (Admin, User)
- API documentation via **Postman** collection

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or update connection string for your DB)

### Setup
1. **Clone the repository:**
   ```sh
   git clone https://github.com/YasmeenHany14/Atos-Internship-Tasks/tree/pokemon-review-system
   cd pokemon-review-system
   ```
2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
3. **Apply migrations and seed the database:**
   ```sh
   dotnet run --project WebApi -- seeddata
   ```
4. **Run the API:**
   ```sh
   dotnet run --project WebApi
   ```

## API Documentation

API documentation is provided as a Postman collection. You can find the collection [here](https://documenter.getpostman.com/view/36220371/2sB2x9kBXh).

## API Endpoints

### Pokémon
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Pokemon            | Get all Pokémon            | No           |
| GET    | /api/Pokemon/{id}       | Get Pokémon by ID          | Admin        |
| GET    | /api/Pokemon/{id}/rating| Get Pokémon rating         | No           |
| POST   | /api/Pokemon            | Add new Pokémon            | Admin        |
| PATCH  | /api/Pokemon/{id}       | Update Pokémon (patch)     | Admin        |
| DELETE | /api/Pokemon/{id}       | Delete Pokémon             | Admin        |

### Categories
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Category           | Get all categories         | No           |
| GET    | /api/Category/{id}      | Get category by ID         | No           |

### Owners
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Owners             | Get all owners             | Admin        |
| GET    | /api/Owners/{id}        | Get owner by ID            | User         |

### Countries
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Countries          | Get all countries          | No           |

### Reviews
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Reviews            | Get all reviews            | No           |
| GET    | /api/Reviews/{id}       | Get review by ID           | No           |
| POST   | /api/Reviews            | Add new review             | No           |

### Reviewers
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| GET    | /api/Reviewers          | Get all reviewers          | No           |
| GET    | /api/Reviewers/{id}     | Get reviewer by ID         | No           |
| POST   | /api/Reviewers          | Add new reviewer           | No           |

### Account
| Method | Endpoint                | Description                | Auth Required |
|--------|-------------------------|----------------------------|--------------|
| POST   | /api/account/login      | Login                      | No           |
| POST   | /api/account/logout     | Logout                     | User         |
| POST   | /api/account/refresh-token | Refresh token           | No           |
| POST   | /api/account/add-role   | Add role to user           | No           |

## License
This project is for educational purposes.
