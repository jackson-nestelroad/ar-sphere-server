using ARSphere.DTO;
using ARSphere.DTO.Helpers;
using ARSphere.Entities;
using ARSphere.Middleware.Validation;
using ARSphere.Models;
using ARSphere.Models.Helpers;
using ARSphere.Persistent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DAL
{
    /// <summary>
    /// <para>API service to work with the Users table.</para>
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        public UserService(DatabaseContext _context, IValidationService _validation) : base(_context, _validation) { }

        public UserViewModelPublic GetById(int id)
        {
            var query = from user in _context.Users
                            where user.Id == id
                            select user;
            var selection = query.ToList();

            return selection.Any() ? selection.First().ToViewModelPublic() : null;
        }

        public User RegisterUser(RegisterModel model)
        {
            _validation.Validate(model);
            return model.ToEntity();
        }
    }
}
