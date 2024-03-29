﻿using ARSphere.DTO.Converters;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ARSphere.Entities
{
    /// <summary>
    /// <para>Represents one row in the Anchors table in the database.</para>
    /// </summary>
    public class Anchor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [ForeignKey("ARModel")]
        public int Model { get; set; }

        [JsonConverter(typeof(PointConverter))]
        public Point Location { get; set; }

        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        // MSSQL cannot store arrays or lists
        // Likes will be stored as a comma-separated list of User IDs
        // Use LikedBy to get and set the comma-separated list as a List<int>

        [ForeignKey("User")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Likes { get; private set; } = "";

        [NotMapped]
        public ISet<int> LikedBy
        {
            get
            {
                return Likes.Split(',').Select(str =>
                {
                    bool success = int.TryParse(str, out int value);
                    return new { success, value };
                }).Where(pair => pair.success).Select(pair => pair.value).ToHashSet();
            }
            set
            {
                Likes = string.Join(',', LikedBy);
            }
        }
    }
}
