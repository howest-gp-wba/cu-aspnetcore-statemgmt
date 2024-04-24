using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCourse.StateMgmt.Web.ViewModels.Cache
{
    public class ViewPiViewModel
    {
        /// <summary>
        /// holds the result of the PI calculation
        /// </summary>
        public PIResultViewModel Result { get; set; }

        public int CacheDuration { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }
}
