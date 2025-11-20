using Domain.Value_Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Agreggates
{
    public class DescriptionPage
    {
        public Nomenclatura Nomenclatura {  get; set; }
        public string PageReference { get; set; }

        public DescriptionPage(string nomenclatura, string pageReference)
        {
            Nomenclatura = new Nomenclatura(nomenclatura);
            PageReference = pageReference;
        }
    }
}
