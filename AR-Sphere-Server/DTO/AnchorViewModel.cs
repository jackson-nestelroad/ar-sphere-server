﻿using ARSphere.DTO.Converters;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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

        [JsonConverter(typeof(PointConverter))]
        public Point Location { get; set; }
        
        public UserViewModelPublic CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        // TODO: Change to LikeCount and LikedByUser
        // LikedByUser would be retrieved by the current user's Authentication, since their ID is linked to it with each request
        public ISet<int> LikedBy { get; set; }
    }

    /// <summary>
    /// <para>Public data format to say a model's like field has been updated.</para>
    /// </summary>
    public class AnchorLikedViewModel : BaseViewModel
    {
        public string Id { get; set; }

        [JsonConverter(typeof(PointConverter))]
        public Point Location { get; set; }

        public ISet<int> LikedBy { get; set; }
    }

    /// <summary>
    /// <para>Public data format to say a model has been deleted.</para>
    /// </summary>
    public class AnchorDeletedViewModel : BaseViewModel
    {
        public string Id { get; set; }

        [JsonConverter(typeof(PointConverter))]
        public Point Location { get; set; }
    }
}
