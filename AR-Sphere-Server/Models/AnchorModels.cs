using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models
{
    /// <summary>
    /// <para>User input for creating a new anchor.</para>
    /// </summary>
    public class NewAnchorModel : IValidatableObject
    {
        [Required(ErrorMessage = "Cloud Anchor ID required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "X-coordinate required.")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "Y-coordinate required.")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "AR Model ID required.")]
        public int Model { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Latitude <= -90 || Latitude >= 90)
            {
                yield return new ValidationResult("X-coordinate out of range.", new[] { "X" });
            }
            if (Longitude <= -180 || Longitude >= 180)
            {
                yield return new ValidationResult("Y-coordinate out of range.", new[] { "Y" });
            }
        }
    }

    /// <summary>
    /// <para>User input for updating an old anchor to a new one.</para>
    /// </summary>
    public class UpdateAnchorModel
    {
        [Required(ErrorMessage = "Old Cloud Anchor ID required.")]
        public int OldId { get; set; }

        [Required(ErrorMessage = "New Cloud Anchor ID required.")]
        public int NewId { get; set; }
    }
}
