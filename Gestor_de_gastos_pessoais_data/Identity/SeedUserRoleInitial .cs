﻿using Gestor_de_gastos_pessoais_data.Domain.Interfaces.InterfacesIdentity;
using Gestor_de_gastos_pessoais_data.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestor_de_gastos_pessoais_data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<UsuarioSistema> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<UsuarioSistema> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUser()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                UsuarioSistema user = new UsuarioSistema();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2021").Result;
                if (result.Succeeded)
                {
                    try
                    {
                        _userManager.AddToRoleAsync(user, "User").Wait();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                 
                }
            }
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                UsuarioSistema user = new UsuarioSistema();
                user.UserName = "admin@localhost";
                user.Email = "admino@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2021").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public void SeedRole()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
