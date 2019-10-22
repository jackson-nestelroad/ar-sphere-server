using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.DTO
{
    /// <summary>
    /// <para>Public data format for presenting a Promotion entity.</para>
    /// </summary>
    public class PromotionViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string SponsorName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
