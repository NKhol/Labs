using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace ClassLibrary
{
    [MetadataType(typeof(DIC_AUDIENCEMD))]
    public partial class DIC_AUDIENCE
    {
        public class DIC_AUDIENCEMD
        {
            
            public DIC_AUDIENCEMD()
        {
            this.PERSON_AUDIENCE = new HashSet<PERSON_AUDIENCE>();
        }
             

        [HiddenInput(DisplayValue = false)]
        public int DAU_ID { get; set; }

        [Display(Name = "Audience")]
        public string DAU_NAME { get; set; }

            
        [HiddenInput(DisplayValue = false)]
        public virtual ICollection<PERSON_AUDIENCE> PERSON_AUDIENCE { get; set; }
             
        }
             
    }
}
