# Restaurant Wholesale Management System (RWMS)

A web-based application designed to help restaurants efficiently manage wholesale operations. RWMS provides a centralized platform where restaurant owners and managers can manage wholesale products, track client orders, organize supply requirements, and monitor basic financial information — replacing disconnected manual processes with a single, structured system.

**Course:** COMP2154 – Systems Development Project
**Team:** Full Stack Kitchen
**Members:** Andrea Salswach Lopez (101580260), Renan Gutierrez (101573073)
**Institution:** George Brown Polytechnic

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [User Roles](#user-roles)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Development Schedule](#development-schedule)

---

## Overview

Many restaurants rely on a mix of phone calls, text messages, spreadsheets, and handwritten notes to manage wholesale orders and supplies. RWMS addresses this by providing one centralized web application where restaurant staff and clients can manage the entire ordering process — from product listings and order placement to supply tracking and financial reporting.

---

## Features

### Restaurant Owners & Managers
- Create, edit, and manage wholesale product listings
- View and manage all client orders in a centralized dashboard
- Accept, reject, and update order statuses
- Create and maintain supply lists based on stock levels and order demand
- Export supply or product lists as PDF documents

### Restaurant Owners (only)
- View basic financial reports summarizing spending on supplies and revenue from orders

### Clients
- Browse available wholesale products
- Place orders with preferred delivery or pickup dates
- View current and past orders along with their status

### System
- User authentication with role-based access control
- Email notifications for order confirmations and account actions

---

## Technology Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET MVC (C#) |
| Database | SQL Server Express |
| ORM | Entity Framework (EF Core) |
| Frontend | Bootstrap |
| Charts | Chart.js |
| Email | Gmail SMTP |
| Version Control | Git / GitHub |
| Project Management | Jira |
| Deployment (optional) | Microsoft Azure |

---

## Architecture

The system follows the **Model–View–Controller (MVC)** pattern, maintaining a clear separation between data, business logic, and the user interface. All modules interact with a single centralized SQL Server Express database.

**Core Modules:**
- **Authentication & Role Management** — User registration, login, and role-based access control
- **Product Management** — Create, edit, and manage wholesale product listings
- **Order Management** — Client order placement and restaurant-side order processing
- **Supply Management** — Supply list creation and maintenance based on stock and order demand
- **Financial Tracking** — Basic spending and revenue reports for restaurant owners
- **Notification Module** — Email confirmations for orders and account actions

---

## User Roles

| Role | Responsibilities |
|---|---|
| Restaurant Owner | Financial oversight, product management, order approval, supply tracking |
| Restaurant Manager | Product management, order processing, supply list maintenance |
| Client | Browse products, place orders, track order status |

---

## Getting Started

> Prerequisites: .NET SDK, SQL Server Express, Git

1. Clone the repository:
   ```bash
   git clone https://github.com/RenanGutierrezz/rwms.git
   cd rwms
   ```

2. Configure the database connection string in `appsettings.json`.

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001`.

---

## Project Structure

```
rwms/
├── Controllers/        # MVC controllers for each module
├── Models/             # Entity models and view models
├── Views/              # Razor views (UI templates)
├── Data/               # DbContext and migrations
├── Services/           # Business logic and email services
├── wwwroot/            # Static assets (CSS, JS, images)
└── appsettings.json    # Application configuration
```

---

## Development Schedule

| Phase | Weeks | Focus |
|---|---|---|
| Planning & Design | 1–2 | Requirements, ER diagrams, wireframes, project setup |
| Core Development | 3–5 | Authentication, product management, order creation |
| Feature Expansion | 6–7 | Supply management, financial tracking, email notifications |
| Testing & Refinement | 8 | Usability testing, bug fixes, performance |
| Finalization | 9 | Documentation, optional deployment, final demo |

---

## Scope

**In scope:** Web app, role-based auth, product management, order management, supply lists, basic financial reporting, PDF export.

**Out of scope:** Mobile apps, online payment processing, advanced analytics, real-time shipment tracking, third-party accounting integrations.
