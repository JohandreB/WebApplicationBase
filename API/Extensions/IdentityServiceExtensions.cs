using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //AddidentityCore<> Used for SPA, ..AddIdentity<> used for standard MVC with session based authorisation
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<AppRole>().AddRoleManager<RoleManager<AppRole>>().AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>().AddEntityFrameworkStores<DataContext>();
            
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                });

            services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
                    opt.AddPolicy("ModerateRole", policy => policy.RequireRole("Admin", "Moderator"));
                });

            return services;
        }
    }
}