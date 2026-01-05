
# UserManagementAPI

## Overview
The **UserManagementAPI** is an ASP.NET Core Web API designed for managing user records efficiently. It provides full CRUD functionality and includes robust validation, error handling, and middleware enhancements.

---

## Activity 1: Initial Setup

### 1. Project Setup
- Created a boilerplate for `Program.cs`:
  - Added **Controllers** and **Swagger** for API documentation.
  - Configured **CORS** for flexibility.
  - Set up middleware for HTTPS, Swagger UI, and routing.

### 2. Core API Structure
- Defined a `User` model with properties:
  - `Id`, `FirstName`, `LastName`, `Email`, `Department`.
- Implemented `UsersController` with full CRUD endpoints:
  - **GET** `/api/users` → Retrieve all users.
  - **GET** `/api/users/{id}` → Retrieve a user by ID.
  - **POST** `/api/users` → Create a new user.
  - **PUT** `/api/users/{id}` → Update an existing user.
  - **DELETE** `/api/users/{id}` → Delete a user.
- Currently uses an **in-memory list** for simplicity.

### 3. Testing Setup
- Provided `UserManagementAPI.http` for easy endpoint testing:
  - Includes sample requests for **GET**, **POST**, **PUT**, and **DELETE**.
  - Compatible with VS Code REST Client or similar tools.

---

## Activity 2: Enhancements

### 1. Improved UsersController Methods
- **GET /users**
  - Added **pagination** (`pageNumber`, `pageSize`) for performance.
  - Implemented error handling for empty datasets and exceptions.
  - Returned structured responses with metadata.
- **GET /users/{id}**
  - Added **ID validation** and contextual error messages.
  - Wrapped logic in `try-catch` for safety.
- **POST /users**
  - Added **input validation** for names, email format, and department.
  - Checked for **duplicate emails**.
  - Returned `400 Bad Request` for invalid input and `409 Conflict` for duplicates.
  - Safe ID assignment using `Max()`.
- **PUT /users/{id}**
  - Validated all fields and ID.
  - Checked for duplicate emails (excluding current user).
  - Returned structured success and error responses.
- **DELETE /users/{id}**
  - Validated ID and handled empty dataset.
  - Returned confirmation message instead of silent `204 No Content`.
  - Added exception handling for stability.

### 2. Edge Case Testing
- Created `UserManagementAPI_EdgeCases.http` to test:
  - Invalid IDs (negative, zero, non-existent).
  - Missing fields and invalid email formats.
  - Duplicate email scenarios.
  - Empty dataset handling.
  - Proper error codes (`400`, `404`, `409`, `500`).

### 3. Improvements Across the Board
- Comprehensive **validation** for all input fields.
- Consistent **JSON responses** for success and failure.
- Global **exception handling** to prevent crashes.
- Performance optimization for large datasets.
- Clear feedback for clients on why requests fail.