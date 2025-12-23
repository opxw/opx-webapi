using LinqToDB.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Opx.WebApi.Common;
using Opx.WebApi.Jwt;
using System.Text;

namespace Opx.WebApi
{
	public static class SetupExtension
	{
		public static IServiceCollection UseOpxWebApi(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(o =>
			{
				o.SuppressModelStateInvalidFilter = true;
			});

			return services;
		}

		public static IServiceCollection UseOpxWebApiLinq2DbRepository(this IServiceCollection services, string provider, string connectionString)
		{
			services.UseRepositoryPattern(provider, connectionString);

			return services;
		}

		public static void UseOpxWebApiHandler(this WebApplication webApplication)
		{
			webApplication.Use(async (context, next) =>
			{
				await next();
				await webApplication.HandleUncatchedStatusCodeAsync(context);
			});
		}

		public static IServiceCollection UseOpxJwtAuth(this IServiceCollection services, JwtTokenValidationSetting validationSetting)
		{
			services.AddSingleton<IJwtTokenValidationSetting, JwtTokenValidationSetting>(_ => validationSetting);

			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(validationSetting.SecretKey));
			var validationParameters = new TokenValidationParameters()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,
				ValidateIssuer = true,
				ValidIssuer = validationSetting.Issuer,
				ValidateAudience = true,
				ValidAudience = validationSetting.Audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
				RequireExpirationTime = false,
			};

			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.TokenValidationParameters = validationParameters;
			});

			return services;
		}
	}
}