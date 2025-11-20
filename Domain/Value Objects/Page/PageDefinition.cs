using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class PageDefinition
    {
        public PageOrientation Orientation { get; set; }
        public PageDefinition()
        {
            Orientation = PageOrientation.LandScape;
        }

        public PageDefinition(PageOrientation orinentation)
        {
            Orientation = orinentation;
        }
    }
}
