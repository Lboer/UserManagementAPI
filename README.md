#Activity 1:
1. Project Setup
•	Created a boilerplate for Program.cs in your ASP.NET Core Web API project (UserManagementAPI): 
o	Added Controllers, Swagger for API documentation.
o	Configured CORS for flexibility.
o	Set up middleware for HTTPS, Swagger UI, and routing.
2. Core API Structure
•	Defined a User model with properties: Id, FirstName, LastName, Email, Department.
•	Created a UsersController with full CRUD endpoints: 
o	GET /api/users → Retrieve all users.
o	GET /api/users/{id} → Retrieve a user by ID.
o	POST /api/users → Create a new user.
o	PUT /api/users/{id} → Update an existing user.
o	DELETE /api/users/{id} → Delete a user.
(Currently using an in-memory list for simplicity.)
3. Testing Setup
•	Provided a UserManagementAPI.http file for easy endpoint testing: 
o	Includes sample requests for GET, POST, PUT, and DELETE.
o	Ready to use with VS Code REST Client or similar tools.
#Activity 2:
1. Enhanced UsersController Methods
GET /users
•	Added pagination (pageNumber, pageSize) to prevent performance bottlenecks.
•	Implemented error handling for empty datasets and unexpected exceptions.
•	Returned structured responses with metadata.
GET /users/{id}
•	Added ID validation (rejects <= 0).
•	Provided contextual error messages for missing users.
•	Wrapped logic in try-catch for safety.
POST /users
•	Added input validation for names, email format, and department.
•	Checked for duplicate emails.
•	Returned 400 Bad Request for invalid input and 409 Conflict for duplicates.
•	Safe ID assignment using Max().
PUT /users/{id}
•	Validated all fields and ID.
•	Checked for duplicate emails (excluding current user).
•	Returned structured success and error responses.
•	Returned updated user object for confirmation.
DELETE /users/{id}
•	Validated ID and handled empty dataset.
•	Returned confirmation message instead of silent 204 No Content.
•	Added exception handling for stability.
2. Edge Case Testing
•	Created a comprehensive .http file (UserManagementAPI_EdgeCases.http) to test: 
o	Invalid IDs (negative, zero, non-existent).
o	Missing fields and invalid email formats.
o	Duplicate email scenarios.
o	Empty dataset handling.
o	Proper error codes (400, 404, 409, 500).
3. Improvements Across the Board
•	Validation for all input fields.
•	Structured JSON responses for success and failure.
•	Exception handling to prevent crashes.
•	Performance optimization for large datasets.
•	Clear feedback for clients on why requests fail.