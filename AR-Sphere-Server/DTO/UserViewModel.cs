using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
    /// <summary>
    /// <para>Public data format for presenting a User entity's public data.</para>
    /// </summary>
    public class UserViewModelPublic : BaseViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Level { get; set; }
    }

    public class UserViewModelPrivate : BaseViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        // TODO: Image class with binary and headers members
        public string ProfileImageUrl { get; set; }
        public int Currency { get; set; }
        public int Level { get; set; }
    }
}
