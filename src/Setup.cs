using LinqToDB.Repository;
using Microsoft.AspNetCore.Mvc;
using Opx.WebApi.Common;

namespace Opx.WebApi
{
	public static class SetupExtension
	{
		public static IServiceCollection UseWebApi(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(o =>
			{
				o.SuppressModelStateInvalidFilter = true;
			});

			return services;
		}

		public static IServiceCollection UseWebApiLinq2DbRepository(this IServiceCollection services, string provider, string connectionString)
		{
			services.UseRepositoryPattern(provider, connectionString);

			return services;
		}

		public static void UseWebApiHandler(this WebApplication webApplication)
		{
			webApplication.Use(async (context, next) =>
			{
				await next();
				await webApplication.HandleUncatchedStatusCodeAsync(context);
			});
		}
	}
}