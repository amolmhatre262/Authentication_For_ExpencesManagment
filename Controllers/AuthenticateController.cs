using ExpenceManagment_AuthenticationSerivices.Data;
using ExpenceManagment_AuthenticationSerivices.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenceManagment_AuthenticationSerivices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<AuthenticateController> _logger; // Fix for CS0246: Correct type name

        // Fix for CS8618: Ensure all non-nullable fields are initialized in the constructor
        public AuthenticateController(ApplicationDBContext context, ILogger<AuthenticateController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.PasswordHash == request.PasswordHash);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { message = "Login successful", user.UserID, user.UserName });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            try
            {
                throw new InvalidOperationException("Simulated exception to test Serilog logging.");
                //List<Users> users = await _context.Users.ToListAsync(); // Fix for potential NullReferenceException
                //return Ok(users);
            }
            catch (Exception ex)
            {
                var requestPath = HttpContext?.Request?.Path;
                var methodName = nameof(GetAllUsers);

                // 🔥 Full structured log for developer
                _logger.LogError(
                    "Exception in {Method} at {Path}. Message: {Message}. StackTrace: {StackTrace}",
                    methodName,
                    requestPath,
                    ex.Message,
                    ex.StackTrace);

                // 🧼 Clean response to client
                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "Something went wrong. Please contact support."
                };

                var correlationId = HttpContext?.Items["CorrelationId"];
                _logger.LogError(ex, "CorrelationId: {CorrelationId}, Exception: {Message}", correlationId, ex.Message);

                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser([FromBody] Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
