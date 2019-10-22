using ARSphere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO.Helpers
{
    /// <summary>
    /// <para>Static class to convert Entities from the database to their View Model equivalent,
    /// which is the representation of the data to the user.</para>
    /// <para>These methods force the transient services to use multiple joins (in one query) to 
    /// retrieve all the data necessary for displaying complete information on an Entity.</para>
    /// </summary>
    public static class EntityToViewModelConverter
    {
        /// <summary>
        /// <para>Converts an Anchor entity to its corresponding View Model.</para>
        /// <para>The following rules should always hold true:</para>
        /// <code>
        ///		anchor.CreatedBy == user.Id
        ///		<br></br>
        ///		anchor.Model == model.Id
        ///		<br></br>
        ///		anchor.Promotion == promotion.Id
        ///		<br></br>
        ///		promotion.Sponsor == sponsor.Id
        /// </code>
        /// </summary>
        /// <param name="anchor"></param>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <param name="promotion"></param>
        /// <param name="sponsor"></param>
        /// <returns></returns>
        public static AnchorViewModel ToViewModel(this Anchor anchor, User user, ARModel model, Promotion promotion, Sponsor sponsor)
        {
            return new AnchorViewModel
            {
                Id = anchor.Id,
                Model = model?.ToViewModel(promotion, sponsor) ?? null,
                CreatedBy = user?.ToViewModel() ?? null,
                CreatedAt = anchor.CreatedAt,
                LikedBy = anchor.LikedBy
            };
        }

        /// <summary>
        /// <para>Converts an ARModel entity to its corresponding View Model.</para>
        /// <para>The following rules should always hold true:</para>
        /// <code>
        ///		model.Promotion == promotion.Id
        ///		<br></br>
        ///		promotion.Sponsor == sponsor.Id
        /// </code>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="promotion"></param>
        /// <param name="sponsor"></param>
        /// <returns></returns>
        public static ARModelViewModel ToViewModel(this ARModel model, Promotion promotion, Sponsor sponsor)
        {
            return new ARModelViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url,
                Promotion = promotion?.ToViewModel(sponsor) ?? null
            };
        }

        /// <summary>
        /// <para>Converts a Promotion entity to its corresponding View Model.</para>
        /// <para>The following rules should always hold true:</para>
        /// <code>
        ///		promotion.Sponsor == sponsor.Id
        /// </code>
        /// </summary>
        /// <param name="promotion"></param>
        /// <param name="sponsor"></param>
        /// <returns></returns>
        public static PromotionViewModel ToViewModel(this Promotion promotion, Sponsor sponsor)
        {
            return new PromotionViewModel
            {
                Id = promotion.Id,
                SponsorName = sponsor?.Name ?? null,
                Title = promotion.Title,
                Url = promotion.Url,
                Description = promotion.Description,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate
            };
        }

        /// <summary>
        /// <para>Converts a User entity to its corresponding View Model.</para>
        /// <para>To be used for displaying public information only.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                RegisteredAt = user.RegisteredAt,
                ProfileImageUrl = user.ProfileImageUrl,
                Level = user.Level
            };
        }
    }
}
