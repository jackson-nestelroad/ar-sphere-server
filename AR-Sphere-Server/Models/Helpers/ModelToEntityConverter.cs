using ARSphere.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models.Helpers
{
    /// <summary>
    /// <para>Static class to convert Models (fed directly to the API) to Entities for storage in the <c>DatabaseContext</c>.</para>
    /// </summary>
    public static class ModelToEntityConverter
    {
        /// <summary>
        /// <para>Converts model data for a User into an entity for the database.</para>
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static User ToEntity(this RegisterModel userModel)
        {
            return new User
            {
                Username = userModel.Username,
                Email = userModel.Password,
                Password = "HASHED",
                RegisteredAt = DateTime.UtcNow
            };
        }

        public static Anchor ToEntity(this NewAnchorModel anchorModel, int creatorId)
        {
            return new Anchor
            {
                Id = anchorModel.Id,
                Model = anchorModel.Model,
                Location = new Point(anchorModel.Longitude, anchorModel.Latitude)
                {
                    SRID = 4326
                },
                CreatedBy = creatorId,
                CreatedAt = DateTime.UtcNow,
                LikedBy = null
            };
        }
    }
}
