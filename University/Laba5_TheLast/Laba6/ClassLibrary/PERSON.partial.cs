using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClassLibrary
{
    [MetadataType(typeof(PERSONMD))]
    public partial class PERSON
    {
        public class PERSONMD
        {
            
            public PERSONMD()
        {
            this.PERSON_AUDIENCE = new HashSet<PERSON_AUDIENCE>();
            this.PERSON_CHAIR = new HashSet<PERSON_CHAIR>();
            this.PERSON_RANKS = new HashSet<PERSON_RANKS>();
            this.LOGIN = new HashSet<LOGIN>();
        }
             

            [HiddenInput(DisplayValue=false)]
            public int PR_ID { get; set; }

            [Display(Name="Name")]
            public string PR_NAME { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int PR_DCH { get; set; }

            [Display(Name = "Position")]
            public string PR_POS { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int PR_DDG { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public string PR_PH { get; set; }

            [Required]
            [Display(Name = "Interests")]
            public string PR_INT { get; set; }


            
            [HiddenInput(DisplayValue = false)]
            public virtual DIC_CHAIRS DIC_CHAIRS { get; set; }

            [HiddenInput(DisplayValue = false)]
            public virtual ICollection<PERSON_AUDIENCE> PERSON_AUDIENCE { get; set; }

            [HiddenInput(DisplayValue = false)]
            public virtual ICollection<PERSON_CHAIR> PERSON_CHAIR { get; set; }

            [HiddenInput(DisplayValue = false)]
            public virtual ICollection<PERSON_RANKS> PERSON_RANKS { get; set; }

            [HiddenInput(DisplayValue = false)]
            public virtual ICollection<LOGIN> LOGIN { get; set; }
             
        }
    }
}
