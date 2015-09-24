using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ClassLibrary
{
    [MetadataType(typeof(DIC_DEGREEMD))]
    public partial class DIC_DEGREE
    {
        public class DIC_DEGREEMD
        {
            [HiddenInput(DisplayValue = false)]
            public int DDG_ID { get; set; }

            [Display(Name = "Degree")]
            public string DDG_NAME { get; set; }
        }
    }
}
