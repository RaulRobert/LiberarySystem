using LiberarySystem.Models;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public int GetUserIdByUsername(string username)
        {
            var user = _context.Authentication.FirstOrDefault(u => u.Username == username);
            return user?.ID ?? 0;
        }


        public async Task<string> GenerateTokenAsync(int userId)
        {
            var token = Guid.NewGuid().ToString(); // random token

            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashedTokenBytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
            var hashedToken = Convert.ToBase64String(hashedTokenBytes);

            var user = await _context.Authentication.FindAsync(userId);
            if (user != null)
            {
                user.Token = hashedToken; // save hashed token in DB
                await _context.SaveChangesAsync();
            }

            return token; // return plain token for session
        }
        public void UpdateUserToken(int userId, string token)
        {
            var user = _context.Authentication.Find(userId);
            if (user != null)
            {
                user.Token = token;
                _context.SaveChanges();
            }
        }


        // Optional: validate token
        public async Task<Authentication?> ValidateTokenAsync(int userId, string token)
        {
            var user = await _context.Authentication.FindAsync(userId);
            if (user == null) return null;

            using var sha = SHA256.Create();
            var hashedTokenBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            var hashedToken = Convert.ToBase64String(hashedTokenBytes);

            return user.Token == hashedToken ? user : null;
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
                .Include(p => p.Role)
                .FirstOrDefaultAsync(a => a.Username == username);

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }


        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<SelectList> getRolesDropDown()
        {
            return new SelectList(await _context.Role.ToListAsync(), "Id", "Name");
        }

        public async Task AddAsync(Authentication auth)
        {
          _context.Authentication.Add(auth);  
            await _context.SaveChangesAsync();  
        }


        public Authentication ValidateUser(string username, string password)
        {
            var user = _context.Authentication
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);

            if (user == null) return null;

            var hasher = new PasswordHasher<Authentication>();
            var result = hasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
