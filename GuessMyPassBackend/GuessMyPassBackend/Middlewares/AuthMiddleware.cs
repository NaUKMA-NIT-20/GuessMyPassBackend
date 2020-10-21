using GuessMyPassBackend.Controllers;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GuessMyPassBackend.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Settings _appSettings;

        public AuthMiddleware(RequestDelegate next, IOptions<Settings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        { 

            if(context.Request.Path.Equals("/user/login") || context.Request.Path.Equals("/user/register"))
            {
                await _next(context);
            } else
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                bool isValid = false;

                if (token != null)
                {
                    isValid = ValidateJwt(context, token);
                }



                if (!isValid)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("No Authorization");
                    return;
                }


                context.Items.Add("token", token);

                await _next(context);
            }
        }

        private bool ValidateJwt(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JWT_SECRET);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken != null) return true;

            }
            catch
            {

                return false;
            }

            return false;
        }
    }

}
