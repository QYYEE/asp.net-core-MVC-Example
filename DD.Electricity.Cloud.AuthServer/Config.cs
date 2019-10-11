
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                new IdentityResource(GlobalSettings.Scope_Roles,GlobalSettings.Scope_Display_Name_Role , new List<string>{
                    JwtClaimTypes.Role
                }),
                new IdentityResource("uname","uname",new List<string>{ "uname"})
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource(GlobalSettings.API_For_Mobile_Client_Scope, "apis for mobile client",new List<string>{ "uname"})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId=GlobalSettings.API_Client_ID,
                    ClientSecrets = new List<Secret>{new Secret(GlobalSettings.API_Client_Secret.Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>{
                        GlobalSettings.API_For_Mobile_Client_Scope,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        GlobalSettings.Scope_Roles,
                        "uname"
                        //GlobalSettings.Scope_GroupId
                    },
                    Claims = new List<Claim>
                    {
                        new Claim("uname","dingding001")
                    },
                    AlwaysIncludeUserClaimsInIdToken=true
                }
            };
        }

        /// <summary>
        /// 本地调试时用
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId="1",
                    Username="dingding",
                    Password="dingding",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Ding Xu"),
                        new Claim(JwtClaimTypes.GivenName, "Ding"),
                        new Claim(JwtClaimTypes.FamilyName, "Ding"),
                        new Claim(JwtClaimTypes.Email, "Ding@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://ding.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim(JwtClaimTypes.Role, GlobalSettings.Temp_Role_Manager),
                        new Claim("GroupId","1"),
                        new Claim("uanme","dingding")
                    }
                }
            };
        }
    }
}
