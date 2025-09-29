using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace library_system.Business
{
    public class AuthenticationBO
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Authentication> _passwordHasher;

        public AuthenticationBO(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Authentication>();
        }

        // ✅ SignUp with hashing
        public async Task<bool> SignUpAsync(Authentication auth)
        {
            // Check if username or email already exists
            var exists = await _context.Authentication
                .AnyAsync(a => a.Username == auth.Username || a.Email == auth.Email);

            if (exists) return false;

            // Hash the password before saving
            auth.Password = _passwordHasher.HashPassword(auth, auth.Password);

            _context.Authentication.Add(auth);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ SignIn with hash verification
        public async Task<Authentication?> SignInAsync(string username, string password)
        {
            var user = await _context.Authentication
                .FirstOrDefaultAsync(a => a.Username == username);

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
