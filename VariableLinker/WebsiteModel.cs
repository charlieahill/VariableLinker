using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariableLinker
{
    [Serializable]
    public class WebsiteModel
    {
        public string SiteName { get; set; } = string.Empty;
        public string SiteUrlPrecursor { get; set; } = string.Empty;
        public string SiteUrlPostcursor { get; set; } = string.Empty;
        public string OverrideDefaultArgumennts { get; set; } = string.Empty;

        [field: NonSerialized]
        public string UserInput { get; set; } = string.Empty;
    }
}
