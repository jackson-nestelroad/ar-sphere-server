﻿using ARSphere.DTO;
using ARSphere.Entities;
using ARSphere.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
    public interface IUserService
    {
        public UserViewModelPublic GetPublicById(int id);
        public UserViewModelPrivate GetPrivateById(int id);
        public User RegisterUser(RegisterModel model);
    }
}
