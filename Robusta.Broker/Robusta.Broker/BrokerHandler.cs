﻿using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web;

namespace Robusta.Broker
{
    public class BrokerHandler : IHttpHandler
    {
        private const string ISSUER = "Robusta.Broker";
        private const string AUDIENCE = "http://localhost/talentmanager/api";

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;

            string userName = request["username"];
            string password = request["password"];

            bool isAuthentic = !String.IsNullOrEmpty(userName) && userName.Equals(password);

            if (isAuthentic)
            {
                // I use a hard-coded key
                byte[] key = Convert.FromBase64String("qqO5yXcbijtAdYmS2Otyzeze2XQedqy+Tp37wQ3sgTQ=");

                var signingCredentials = new SigningCredentials( new InMemorySymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature,SecurityAlgorithms.Sha256Digest);
                
                var descriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
                {
                    Issuer = ISSUER,
                    Audience = AUDIENCE,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = signingCredentials,
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                })
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(descriptor);

                context.Response.Write(tokenHandler.WriteToken(token));
            }
            else
                context.Response.StatusCode = 401;
        }
    }
}