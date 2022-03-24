using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.Users;
using Service.Identity.Domain.UsersRol;
using System;

namespace Service.Identity.Context
{
    public class IndentityContext : IdentityDbContext<UsersModel,UserRol,Guid>
    {
        public IndentityContext(DbContextOptions<IndentityContext> options) : base(options)
        {
           
        }
    }
}

