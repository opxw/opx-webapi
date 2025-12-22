using Mapster;

namespace Opx.WebApi.Extensions
{
	public static class MapExtension
	{
		public static T? MapTo<T>(this object source) where T : class
		{
			return source.Adapt<T>();
		}

		public static async Task<T> MapToAsync<T>(this object source) where T : class
		{
			return await source.BuildAdapter().AdaptToTypeAsync<T>();
		}
	}
}
