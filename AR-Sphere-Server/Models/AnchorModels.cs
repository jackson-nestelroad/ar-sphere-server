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
        public double X { get; set; }

        [Required(ErrorMessage = "Y-coordinate required.")]
        public double Y { get; set; }

        [Required(ErrorMessage = "AR Model ID required.")]
        public int Model { get; set; }

        [Required(ErrorMessage = "User ID required.")]
        public int Creator { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (X <= -90 || X >= 90)
            {
                yield return new ValidationResult("X-coordinate out of range.", new[] { "X" });
            }
            if (Y <= -180 || Y >= 180)
            {
                yield return new ValidationResult("Y-coordinate out of range.", new[] { "Y" });
            }
        }
    }

    public class UpdateAnchorModel
    {
        [Required(ErrorMessage = "Old Cloud Anchor ID required.")]
        public int OldId { get; set; }

        [Required(ErrorMessage = "New Cloud Anchor ID required.")]
        public int NewId { get; set; }
    }
}
