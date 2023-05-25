using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using EReaderNow.Data.Service;
using ERederNow_android.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace EReaderNow.Data.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAPIController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private static string? token;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly AddDB db;
        public AccountAPIController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr, IDbContextFactory dbContextFactor)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            _dbContextFactory = dbContextFactor;

            Console.WriteLine("AccountController");
        }
        public async Task SaveToken(string tok)
        {
            using (AddDB db = _dbContextFactory.Create())
            {
               if(tok == null) Console.WriteLine("SaveToken" + "Fasle tok is null");
               
                Token token = new Token() { tokenValue = new string( tok) };

                if (!FindToken(tok).Result)
                {
                    if (tok != "")
                    {
                        await db.Tokens.AddAsync(token);
                        await db.SaveChangesAsync();
                        Console.WriteLine("SaveToken");
                    }
                }

        
            

            }
        }
        private async Task<string> GenerateJwtTokenAsync(Account account)
        {
            Console.WriteLine("GenerateJwtTokenAsync");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cf7f86b3-6f3b-46f7-b519-b091003a5f56"));
            var claims = new List<Claim>
            {
          new Claim(ClaimTypes.Name, account.UserName),
          new Claim(ClaimTypes.Role, "Admin")
            };

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account is null");
            }
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(6),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            

            // Serialize the token to a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> FindToken(string token)
        {
            using (AddDB db = _dbContextFactory.Create())
            {
                List<Token> matchingToken12 = await db.Tokens.ToListAsync();
                foreach(var a in matchingToken12)
                {
                    Console.WriteLine(a.ID + a.tokenValue);
                }
                var matchingToken = db.Tokens.AsEnumerable().FirstOrDefault(t => t.tokenValue == token);
                if (matchingToken != null)
                {
                    Console.WriteLine("FindToken");
                    return true;
                }
            }
            return false;
        }
        [HttpGet("restricted")]
        public IActionResult GetRestricted()
        {
            Console.WriteLine("restricted");
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            Console.WriteLine("1" + token);
            Console.WriteLine("2" + AccountAPIController.token);
          
            if (FindToken(token).Result)
            {
                Console.WriteLine("ok");
                return Ok();
            }
            Console.WriteLine("false");
            return BadRequest();
        }
        [HttpPost("Login")]
        [Obsolete]
        public async Task<IActionResult> Login(object model)
        {
            Console.WriteLine("Login");
    
            Account account = JsonSerializer.Deserialize<Account>(model.ToString());

            if (account == null) { return BadRequest(new { message = "Model is null" }); }
            var result = await signInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, false);
            
       /*     if (!result.Succeeded)
            {
                return BadRequest(new { message = "PasswordSignInAsync failed" });
            }*/

            if (result.Succeeded)
            {
                 token = new string(await GenerateJwtTokenAsync(account));
                Console.WriteLine(token);
                 await SaveToken(AccountAPIController.token);

                return Ok(new { message = "Success", AccountAPIController.token });
            
            }
            else
            {
                return BadRequest(new { message = "Invalid login or password" });
            }
        }
       /* [HttpPost("Login")]
        public async Task<IActionResult> Login(Account account)
        {   
            Console.WriteLine("OnLogin");
            Console.WriteLine(account.ToString());
            if (account == null) { return BadRequest(new { message = "Model is null" }); }
            var result = await signInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok(new { message = "Success" });
            }
            else
            {
                return BadRequest(new { message = "Invalid login or password" });
            }
        }*/

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Success" });
        }
      /*  // POST api/BooksItems
        [HttpGet("GetLogin/")]
        public async Task<IActionResult> GetLogin(Account log)
        {
            Console.WriteLine("GetLogin");
            using (AddDB db = _dbContextFactory.Create())
            {
                if (log == null)
                {
                    return BadRequest();
                }

            }
            return Ok(log);

        }*/
  

        [HttpGet]
    
        public IActionResult Register()
        {
            return Ok(new { message = "Register" });
        }

        [HttpPost]
      
        public async Task<IActionResult> Register(object model)
        {
            Account account = JsonSerializer.Deserialize<Account>(model.ToString());
            if (account == null) { return BadRequest(new { message = "Model is null" }); }
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { Email = account.Email, UserName = account.UserName };
                var result = await userManager.CreateAsync(user, account.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Ok(new { message = "Success" });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { errors = errors });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors = errors });
            }
        }
    }
}

