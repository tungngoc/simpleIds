using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelloIdentity
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "123"
                },
                 new TestUser{SubjectId = "818727", Username = "alice", Password = "alice",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "ClienID_1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("Client_Secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
                 new Client
                {
                    ClientId = "ro.test",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("Client_Secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
                 //OpenId Connect implicit flow client 
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"https://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.WebSite
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
