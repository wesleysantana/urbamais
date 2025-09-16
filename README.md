# Urbamais (DDD Example · .NET)

> **Heads-up:** This repository is a **demonstration** of how I structure projects using **Domain-Driven Design (DDD)**.  
> It focuses on **architecture and patterns**, not delivering a fully finished product.  
> Some features are **incomplete** or **stubbed**; parts of the app **may not work end-to-end**.

## ✨ Overview

- **DDD architecture** with clear boundaries: **Domain**, **Application**, **Infrastructure**, **WebApi**, and **CrossCutting**.
- **JWT** authentication (HMAC) with **ASP.NET Identity**.
- **Entity Framework Core** with **PostgreSQL** (Npgsql).
- **Swagger/OpenAPI** with API versioning.
- Consistent error responses via **ProblemDetails**.
- **AutoMapper** for DTO ↔ domain mappings.
- Domain validations with **FluentValidation-style** results and helpers to compose errors from VOs/child entities.

> Some subdomains (e.g., **City**) are intentionally **not 100% complete** to keep the repo focused on structure.

---

## 🧱 Solution Layout

```
src/
  Core/
    Constants/
    Domain/
      Interfaces/           # IAggregateRoot, IEntity, etc.
    Enums/
    SeedWork/               # BaseEntity, BaseValidate
    ValueObjects/           # CpfVO, CnpjVO, NomeVO, DescricaoVO
  Urbamais.Domain/
    Entities/
      EntitiesOfCore/       # City, Address, Phone, Email, ...
      Planejamento/         # Unidade, Insumo, ...
      Obra/ Suprimento/ Fornecedor/ ...
    InterfacesRepositories/
      Core/                 # ICidadeRepository, ...
      Planejamento/         # IUnidadeRepository, ...
      Generic/              # legacy interfaces (being phased out)
    Services/               # domain services (when needed)
  Urbamais.Application/
    Interfaces/             # IUnidadeAppService, ICidadeAppService, ...
    Services/               # UnidadeAppService, CidadeAppService, ...
    ViewModels/             # Request/Response DTOs
    App/                    # (optional) app-layer helpers
  Urbamais.Infra/
    Config/                 # ContextEf, EF configurations
      ConfigModels/         # EntityTypeConfiguration per entity
    Repositories/           # UnidadeRepository, CidadeRepository, ...
  Urbamais.Identity/
    Config/                 # ContextIdentity, Identity configs
    Services/               # IdentityService, roles/permissions
  Urbamais.CrossCutting/
    AutoMapper/             # MappingProfile
    IOC/                    # ModuloIOC (DI registrations)
  Urbamais.WebApi/
    Controllers/
    Swagger/
    Shared/                 # Custom ProblemDetails
    Program.cs, Bootstrap.cs, AuthenticationSetup.cs, ...
```

---

## 🧭 Key Architectural Decisions

- **Domain stays clean**: domain project references **no** EF/Infra/Web packages. It exposes **interfaces** (e.g., `IUnidadeRepository`); implementations live in Infrastructure.
- **Repositories per aggregate**: public contracts are **business-oriented** (`ObterPorSiglaAsync`, `BuscarPorDescricaoAsync`, etc.). Internal EF helpers may exist inside Infra, but do not leak to Domain.
- **Unit of Work**: the Application layer coordinates persistence via `IUnitOfWork` (single commit per use case).
- **Validation in Domain**: entities/VOs expose `ValidationResult`. Helpers in `BaseValidate` aggregate VO/child errors without repetitive boilerplate.
- **Soft delete** where appropriate via domain methods (`Delete()`/`Restore()`), persisted by Infra; **hard delete** only for exceptional cases.
- **JWT + Identity**: token issuance with issuer/audience/secret from configuration; refresh token flow available.
- **API Versioning + Swagger**: grouped docs and consistent ProblemDetails for errors.

---

## 🛠️ Stack

- **.NET** (ASP.NET Core Web API)
- **EF Core** + **Npgsql** (PostgreSQL)
- **ASP.NET Identity**
- **JWT** (HMAC)
- **AutoMapper**
- **Swashbuckle** (Swagger)
- **Hellang ProblemDetails**

---

## ▶️ Run Locally

> This is a **demo**. Some endpoints might be partial or not fully wired to the DB.

1) **Prereqs**
- .NET SDK (7/8)
- PostgreSQL (or adjust provider/connection)
- (Optional) EF Core CLI: `dotnet tool install --global dotnet-ef`

2) **Configure `appsettings.Development.json`** (example)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=urbamais;Username=postgres;Password=postgres"
  },
  "JwtOptions": {
    "Issuer": "Urbamais",
    "Audience": "Urbamais.Api",
    "SecurityKey": "YOUR_LONG_RANDOM_SECRET_KEY_HERE",
    "AccessTokenExpiration": 60,
    "RefreshTokenExpiration": 1440
  }
}
```

3) **Restore & (optionally) apply migrations**
```bash
dotnet restore
# dotnet ef database update -p src/Urbamais.Infra -s src/Urbamais.WebApi
```

4) **Run the API**
```bash
dotnet run --project src/Urbamais.WebApi
```
Swagger will be available (e.g., `https://localhost:xxxx/swagger`).

---

## 🔐 Quick JWT Check

Typical auth endpoints (names may vary slightly in this demo):

- `POST /api/usuario/login` → returns access/refresh tokens
- `POST /api/usuario/refresh-login` → refresh flow (requires bearer token)

Use header:
```
Authorization: Bearer <your_access_token>
```

> You may need to seed a test user or tweak `IdentityService` for a quick demo login.

---

## 📚 Endpoints (current state)

- **Unidade** (`/api/unidade`): main CRUD/search endpoints are implemented (Application + Infra).
- **Cidade** (`/api/cidade`): **in progress**; Domain/Infra are ahead of Application/Controller in some parts.

Some calls may return **404/500** until the full chain (Controller → App → Repo → EF mappings → DB) is finalized for every aggregate.

---

## 🧪 Tests

- **Domain.Tests** — entities and value objects (e.g., `UnidadeTests`, `CpfVOTest`, etc.).
- **Application.Tests** — app services with **fakes/mocks** for repositories/UoW.
- **WebApi.Tests** — controller tests (light integration).

> Some tests are scaffolds or not yet updated to the latest refactors (e.g., “single `Update` method” on `Unidade`). The intent is to showcase the **testing approach**.

---

## 🧩 Patterns You’ll Find

- Business-oriented repository interfaces in **Domain**; EF implementations in **Infra**.
- `IUnitOfWork.SaveChangesAsync` at the end of application use cases.
- Domain **normalization** (trim/uppercase when relevant) and **error aggregation** helpers in `BaseValidate`.
- **Soft delete** methods on aggregates, with global query filters at the DbContext (when used).
- **Swagger** + **ProblemDetails** for consistent API docs and errors.
- **API versioning** (header based, e.g., `v1`).

---

## 🗺️ Roadmap / Ideas

- Finish **City** flow (Controller + App + tests).
- Standardize **paging/take** across all list endpoints.
- Add indices for frequent queries (e.g., `IX_Cidade(Uf, Nome)`).
- Ensure **UTC** timestamps end-to-end.
- Seed & migrations for a **one-click demo**.
- **Docker Compose** for API + Postgres.
- Publish a **Postman collection** with sample JWT calls.

---

## ⚠️ Disclaimer

This is a **portfolio/demo** project.  
It is **not production-ready**.  
Some parts are **work-in-progress** or deliberately simplified to highlight architecture.

---

## 📄 License

Suggested: **MIT License** (simple and friendly for portfolio use).

---

## 🤝 Contributing / Feedback

PRs and suggestions are welcome — especially around:
- Aggregate boundaries and DDD refinements
- Integration tests and migration/seed setup
- API documentation and examples
- Docker/CI improvements

---

*Thanks for checking out this DDD example!*

