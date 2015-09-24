using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClassLibrary
{
    [MetadataType(typeof(PERSON_CHAIRMD))]
    public partial class PERSON_CHAIR
    {
        public class PERSON_CHAIRMD
        {
            public int PC_PR { get; set; }
            public int PC_CH { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int PC_ID { get; set; }

            
            public virtual PERSON PERSON { get; set; }
             
        }
    }
}
