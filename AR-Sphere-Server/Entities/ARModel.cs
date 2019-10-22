using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Entities
{
    /// <summary>
    /// <para>Represents one row in the ARModels table in the database.</para>
    /// </summary>
    public class ARModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        [ForeignKey("Promotion")]
        public int Promotion { get; set; }
    }
}
