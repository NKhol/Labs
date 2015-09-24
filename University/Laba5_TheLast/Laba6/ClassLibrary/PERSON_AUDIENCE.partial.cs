using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ClassLibrary
{
    [MetadataType(typeof(PERSON_AUDIENCEMD))]
    public partial class PERSON_AUDIENCE
    {
        public class PERSON_AUDIENCEMD
        {
            public int PAU_PR { get; set; }
            public int PAU_DAU { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int PAU_ID { get; set; }
            

            [HiddenInput(DisplayValue = false)]
            public virtual DIC_AUDIENCE DIC_AUDIENCE { get; set; }
            [HiddenInput(DisplayValue = false)]
            public virtual PERSON PERSON { get; set; }
             
        }
    }

}
