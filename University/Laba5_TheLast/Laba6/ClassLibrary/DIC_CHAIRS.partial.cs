using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ClassLibrary
{
    [MetadataType(typeof(DIC_CHAIRSMD))]
    public partial class DIC_CHAIRS
    {
        public class DIC_CHAIRSMD
        {
            
             public DIC_CHAIRSMD()
        {
            this.PERSON = new HashSet<PERSON>();
        }
    
        [HiddenInput(DisplayValue = false)]
        public int DCH_ID { get; set; }

        [Display(Name = "Chair")]
        public string DCH_NAME { get; set; }
            
        [HiddenInput(DisplayValue = false)]
        public virtual ICollection<PERSON> PERSON { get; set; }
             

        }
    }
}
