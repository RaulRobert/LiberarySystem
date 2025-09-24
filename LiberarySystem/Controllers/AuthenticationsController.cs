using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Login.Models;

namespace LiberarySystem.Controllers
{
    public class AuthenticationsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthenticationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Authentications


        public IActionResult SignIn()
        {
            return View();
        }

     
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("ID,Username,Password,Name,Surname,Email")] Authentication authentication)
        {
            _context.Add(authentication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


      
    }
}
