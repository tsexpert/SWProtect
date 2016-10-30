﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;
using WpfApplication1.Domain;

namespace WpfApplication1.Controller
{
    class AuthController
    {
        private Context db = new Context();       

        public bool firstLogin { get; set; }

        public AuthController()
        {
            db.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));
            firstLogin = (db.Users
                .Where(u => u.Login == "Admin")
                .SingleOrDefault()) == null;

        }

        public void SaveDto(User entity)
        {
            
            db.Users.Add(entity);
            db.SaveChanges();
        }
        
        public string Autorization(string login, string pass)
        {
            string validation = null;
            var user = db.Users
                .Where(u => u.Login == login)
                .SingleOrDefault();
            if (user != null)
            {
                if (user.Password != pass)
                {
                    return validation = "Неправильный пароль";
                }
            }
            else return validation = "Пользователь не найден";
            return validation;
        }
    }
}