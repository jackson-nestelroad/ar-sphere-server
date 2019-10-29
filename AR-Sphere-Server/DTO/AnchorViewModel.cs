using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
    /// <summary>
    /// <para>Public data format for presenting an Anchor entity.</para>
    /// </summary>
    public class AnchorViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public ARModelViewModel Model { get; set; }
        public UserViewModelPublic CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int> LikedBy { get; set; }
    }
}
