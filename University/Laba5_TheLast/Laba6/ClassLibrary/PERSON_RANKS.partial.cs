using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ClassLibrary
{
    [MetadataType(typeof(PERSON_RANKSMD))]
    public partial class PERSON_RANKS
    {
        public class PERSON_RANKSMD
        {
            public int PRS_PR { get; set; }
            public int PRS_DRK { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int PRS_ID { get; set; }

            
            public virtual DIC_RANKS DIC_RANKS { get; set; }
            public virtual PERSON PERSON { get; set; }
             
        }
    }
}
