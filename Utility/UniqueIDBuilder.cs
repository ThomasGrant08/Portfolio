using Microsoft.AspNetCore.Mvc;

namespace ThomasGrant.Utility
{
	public static class UniqueIDBuilder
	{
		public static string Generate()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var pass = string.Empty;
			Random rnd = new Random();

			for (int i = 0; i < 10; i++)
			{
				var c = chars[rnd.Next(chars.Length)];
				pass = pass + c;
			}

			return pass;

		}
	}
}
