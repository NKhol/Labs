using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ClassLibrary
{
    [MetadataType(typeof(DIC_RANKSMD))]
    public partial class DIC_RANKS
    {
        public class DIC_RANKSMD
        {
            
            public DIC_RANKSMD()
        {
            this.PERSON_RANKS = new HashSet<PERSON_RANKS>();
        }
             

            [HiddenInput(DisplayValue = false)]
            public int DRK_ID { get; set; }

            [Display(Name = "Name of rank")]
            public string DRK_NAME { get; set; }

            
            [HiddenInput(DisplayValue = false)]
            public virtual ICollection<PERSON_RANKS> PERSON_RANKS { get; set; }
             
        }
    }
}
