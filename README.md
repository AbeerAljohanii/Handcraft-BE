# 🎨 Artify Backend Project

## 🌟 Project Overview

This is a backend solution for an e-commerce platform dedicated to selling paintings and showcasing art galleries, built with **.NET 8**. The project includes core functionalities such as **user authentication**, **product management** (artworks), **category management** (types of artwork ), and **order processing**. The system provides a seamless experience for artists and buyers to exchange artistic works with ease.

## Features

- **👤 User Management**:
  - Register new User (Customer/Artist)
  - User authentication with JWT token
  - Role-based access control (Artist, Customer)
- **🖼️ Product Management (Artworks)**:
  - Create new artwork listing (title, description, price)
  - Update artwork information
  - Delete artwork listings
  - Search and filter artworks by ArtistId and artist
- **🏷️ Category Management**:
  - Create new categories (types of artwork)
  - Retrieve category details
  - Update category information
  - Delete categories
  - Associate artworks with specific categories
- **🛠️ Workshop Management**:
  - Create new workshope
  - Retrieve workshop details
  - Update workshop information
  - Delete workshop
- **📅 Booking Management**:
  - Create new bookings for workshops
  - Retrieve bookings by user or workshop
  - Update booking status (confirmed, canceled)
  - Delete bookings
  - Handle bookings with pagination and filtering
- **📦 Order Management**:
  - Create new orders for purchased artworks
  - Retrieve order details by user or order ID
  - Update order status (pending, shipped, completed)
  - Delete orders
  - View order history for users
- **💳 Payment Management**:

## ⚙️ Technologies Used

- **.Net 8**: Web API Framework
- **Entity Framework Core**: ORM for database interactions
- **PostgreSQl **: Relational database for storing data
- **JWT**: For user authentication and authorization
- **AutoMapper**: For object mapping
- **Swagger**: API documentation

## 📋Prerequisites

- .Net 8 SDK
- SQL Server
- VSCode

## 🛠️ Getting Started

### 1. Clone the repository:

```bash
git clone https://github.com/AbeerAljohanii/sda-3-online-Backend_Teamwork
```

### 2.🛠️ Setup database

- Make sure PostgreSQL Server is running
- Create `appsettings.json` file
- Update the connection string in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Local": "Server=localhost;Database=ECommerceDb;User Id=your_username;Password=your_password;"
  }
}
```

- Run migrations to create database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- Run the application

```bash
dotnet watch
```

The API will be available at: `http://localhost:5125`

### 🐍 Swagger

- Navigate to `http://localhost:5125/swagger/index.html` to explore the API endpoints.

## 📂 Project structure

```bash
|-- Controllers: API controllers with request and response
|-- Database # DbContext and Database Configurations
|-- DTOs # Data Transfer Objects
|-- Entities # Database Entities (User, ArtWorks, Category, Order)
|-- Middleware # Logging request, response and Error Handler
|-- Repositories # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Utils # Common logics
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
```

## 📡 API Endpoints

### User

- **POST** `/api/users` – Register a new user.
- **POST** `/api/users/signin` – Login and get JWT token.
- **GET** `/api/users/search-by-name/{name}` - Search user by name.
- **GET** `/api/users/search-by-phone/{phoneNumber}` - Search user by phone number.
- **GET** `/api/users/page` - Pagination for users.

### Artwork

- **POST** `/api/artworks` – Create a new artwork (requires Artist role).
- **GET** `/api/artworks` – Get all artworks with pagination.
- **GET** `/api/artworks/{id}` – Get artwork by ID.
- **GET** `/api/artworks/artist/{artistId}` – Get artworks by artist ID.
- **PUT** `/api/artworks/{id}` – Update an artwork (requires Admin or Artist role).
- **DELETE** `/api/artworks/{id}` – Delete an artwork (requires Admin or Artist role).

### Category

- **GET** `/api/categories` – Get all categories.
- **GET** `/api/categories/{id}` – Get category by ID.
- **GET** `/api/categories/search/{name}` – Get category by name.
- **GET** `/api/categories/page` – Get categories with pagination.
- **POST** `/api/categories` – Create a new category.
- **PUT** `/api/categories/{id}` – Update a category.
- **DELETE** `/api/categories/{id}` – Delete a category.


### Order

- **GET** `/api/orders` – Get all orders.
- **GET** `/api/orders/sort-by-date` – Sort orders by date.
- **GET** `/api/orders/{id}` – Get order by ID.
- **POST** `/api/orders` – Create a new order.
- **PUT** `/api/orders/{id}` – Update an order.
- **DELETE** `/api/orders/{id}` – Delete an order.

## 🌐 Deployment

The application is deployed and can be accessed at: [https://your-deploy-link.com](https://your-deploy-link.com)

## 👩‍💻 Team Members

- **Lead** : Abeer Aljohani (@AbeerAljohanii) 👩‍💻
- Bashaer Alhuthali (bashaer310) 👩‍💻
- Danah Almalki (DanaAlmalki) 👩‍💻
- Manar Almalawi (mal-manar) 👩‍💻
- Shuaa Almarwani (Shuaa-99) 👩‍💻

## License

This project is licensed under the MIT License.
