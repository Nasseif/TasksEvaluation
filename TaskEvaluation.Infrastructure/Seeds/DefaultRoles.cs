using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Consts;

namespace TaskEvaluation.Infrastructure.Seeds
{
		public static class DefaultRoles
		{
			public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
			{
				if (!roleManager.Roles.Any())
				{
					await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
				}
			}
		}

}
