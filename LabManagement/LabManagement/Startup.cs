using LabManagement.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LabManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var secretKey = _configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "YourIssuer", // Định nghĩa Issuer của bạn
                    ValidAudience = "YourAudience", // Định nghĩa Audience
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes) // Secret key
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ChuyenVien", policy => policy.RequireClaim("NhomQuyen", "Giám đốc trung tâm"));
                options.AddPolicy("QuanLyPTN", policy => policy.RequireClaim("NhomQuyen", "Chuyên viên phòng thí nghiệm"));
                options.AddPolicy("NguoiDung", policy => policy.RequireClaim("NhomQuyen", "Người dùng"));
            });

            services.AddControllers();

        }
    }
}
