using ARSphere.Persistent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models
{
    public class NewAnchorModel : IValidatableObject
    {
        [Required(ErrorMessage = "Cloud Anchor ID required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "X-coordinate required.")]
        public float X { get; set; }

        [Required(ErrorMessage = "Y-coordinate required.")]
        public float Y { get; set; }

        [Required(ErrorMessage = "AR Model ID required.")]
        public int Model { get; set; }

        [Required(ErrorMessage = "User ID required.")]
        public int Creator { get; set; }
    }

    public class UpdateAnchorModel
    {
        [Required(ErrorMessage = "Old Cloud Anchor ID required.")]
        public int OldId { get; set; }

        [Required(ErrorMessage = "New Cloud Anchor ID required.")]
        public int NewId { get; set; }
    }
}
