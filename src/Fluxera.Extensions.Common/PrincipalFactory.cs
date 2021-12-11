//namespace Fluxera.Extensions.Common
//{
//	using System;
//	using System.IdentityModel.Tokens.Jwt;
//	using System.Security.Claims;
//	using System.Text;
//	using System.Threading;
//	using Fluxera.Common;
//	using JetBrains.Annotations;
//	using Microsoft.Extensions.Logging;
//	using Microsoft.Extensions.Options;
//	using Microsoft.IdentityModel.Tokens;

//	[UsedImplicitly]
//	internal sealed class PrincipalFactory : IPrincipalFactory
//	{
//		private readonly ILogger<PrincipalFactory> logger;
//		private readonly PrincipalFactoryOptions options;

//		public PrincipalFactory(
//			ILogger<PrincipalFactory> logger,
//			IOptions<PrincipalFactoryOptions> options)
//		{
//			this.logger = logger;
//			this.options = options.Value;
//		}

//		/// <inheritdoc />
//		public ClaimsPrincipal CreatePrincipal(string jwt)
//		{
//			ClaimsPrincipal? principal = null;

//			try
//			{
//				JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
//				handler.InboundClaimTypeMap.Clear();

//				TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
//				{
//					ValidateAudience = false,
//					ValidateIssuer = false,
//					ValidateIssuerSigningKey = true,
//					ValidateActor = false,
//					ValidateLifetime = false,
//					ValidateTokenReplay = false,
//					NameClaimType = JwtClaimTypes.Name,
//					RoleClaimType = JwtClaimTypes.Role,
//					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.options.SigningKey))
//				};

//				principal = handler.ValidateToken(jwt, tokenValidationParameters, out _);
//			}
//			catch(Exception ex)
//			{
//				this.logger.LogWarning(ex, ex.Message);
//			}

//			principal ??= new ClaimsPrincipal(new ClaimsIdentity());
//			return principal;
//		}
//	}
//}


