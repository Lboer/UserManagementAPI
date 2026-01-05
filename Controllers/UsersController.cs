
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = [];

    [HttpGet]
    public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            if (users == null || !users.Any())
            {
                return NotFound(new { message = "No users found." });
            }

            // Basic pagination
            var pagedUsers = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                totalCount = users.Count,
                pageNumber,
                pageSize,
                data = pagedUsers
            });
        }
        catch (Exception ex)
        {
            // Log the exception (in real-world scenarios use ILogger)
            return StatusCode(500, new { message = "An error occurred while retrieving users.", details = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            // Validate ID
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID. ID must be greater than zero." });
            }

            if (users == null || !users.Any())
            {
                return NotFound(new { message = "No users available in the system." });
            }

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} was not found." });
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            // Log exception (in production, use ILogger)
            return StatusCode(500, new { message = "An error occurred while retrieving the user.", details = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest(new { message = "User data is required." });
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return BadRequest(new { message = "First name and last name are required." });
            }

            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return BadRequest(new { message = "A valid email address is required." });
            }

            if (string.IsNullOrWhiteSpace(user.Department))
            {
                return BadRequest(new { message = "Department is required." });
            }

            // Check for duplicate email
            if (users.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(new { message = "A user with this email already exists." });
            }

            // Assign ID and add user
            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the user.", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User updatedUser)
    {
        try
        {
            // Validate ID
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID. ID must be greater than zero." });
            }

            if (updatedUser == null)
            {
                return BadRequest(new { message = "User data is required." });
            }

            // Validate fields
            if (string.IsNullOrWhiteSpace(updatedUser.FirstName) || string.IsNullOrWhiteSpace(updatedUser.LastName))
            {
                return BadRequest(new { message = "First name and last name are required." });
            }

            if (string.IsNullOrWhiteSpace(updatedUser.Email) || !updatedUser.Email.Contains("@"))
            {
                return BadRequest(new { message = "A valid email address is required." });
            }

            if (string.IsNullOrWhiteSpace(updatedUser.Department))
            {
                return BadRequest(new { message = "Department is required." });
            }

            // Find existing user
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} was not found." });
            }

            // Check for duplicate email (excluding current user)
            if (users.Any(u => u.Email.Equals(updatedUser.Email, StringComparison.OrdinalIgnoreCase) && u.Id != id))
            {
                return Conflict(new { message = "Another user with this email already exists." });
            }

            // Update fields
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;

            return Ok(new { message = "User updated successfully.", user });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the user.", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            // Validate ID
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID. ID must be greater than zero." });
            }

            if (users == null || !users.Any())
            {
                return NotFound(new { message = "No users available in the system." });
            }

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} was not found." });
            }

            users.Remove(user);

            return Ok(new { message = $"User with ID {id} has been deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the user.", details = ex.Message });
        }
    }
}