using Installation.Domain.Entities;
using MHPlatform.Service.Model.OrderForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Model.Installation
{
    public class FileFlowDto: OrderFormDto
    {
        public int ID { get; set; }

        public string OrderID { get; set; } =string.Empty;

        public int FileFlowNo { get; set; }

        public string? Datecreated { get; set; }

        public string? OTfactor { get; set; }

        public string? OTchkbx { get; set; }

        public string? Priority { get; set; }

        public DateTime? ApprxDelDate { get; set; }

        public string? ProjectList { get; set; }

        public string? ApprDelDateTxt { get; set; }

        public DateTime? PaymentReceived { get; set; }

        public DateTime? FileMadeUp { get; set; }

        public string? DesignConsultant { get; set; }

        public string? TehcnicalRep { get; set; }

        public DateTime? FileIn { get; set; }

        public DateTime? CMDate { get; set; }

        public DateTime? TargetDate { get; set; }

        public DateTime? FileOut { get; set; }

        public DateTime? OverTargetDate { get; set; }

        public string? Reasons { get; set; }

        public DateTime? LeadStart { get; set; }

        public DateTime? LeadClosed { get; set; }

        public string? Revision { get; set; }

        public DateTime? ChckddocsDate { get; set; }

        public DateTime? ChckdforTechnclDate { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? Leftshwroom { get; set; }

        public DateTime? Recvinplant { get; set; }

        public string? Shwrm { get; set; }

        public string? Deleted { get; set; }

        public string? Deletedby { get; set; }

        public string? GrpngSysGen { get; set; }

        public string? GrpngCtgry { get; set; }

        public string? GrpngMat { get; set; }

        public string? ManualGrpngCtgry { get; set; }

        public string? ManualGrpngMat { get; set; }

        public DateTime? ChckbySalesDesigner { get; set; }

        public string? Variation { get; set; }

        public string? WorkingDaysOver { get; set; }

        public string? WorkingDaysUnder { get; set; }

        public string? FFsrc { get; set; }

        public string? FGrouping { get; set; }

        public string? MGrouping { get; set; }

        public string? SysVer { get; set; }

        public string? Typ { get; set; }

        public string? Cbfpaging { get; set; }

        public string? Fpaging { get; set; }

        [Column("lock")]
        public string? Islock { get; set; }

        public string? Lockedby { get; set; }

        public string? MHGrouping { get; set; }

        public string? Samplecolor { get; set; }
        /// <summary>
        /// Addedcustom Property to Customer data/info hold
        /// </summary>
        /// 
        //public string? Ordrno { get; set; } = string.Empty;
        //public string? Ctitle { get; set; } = string.Empty;
        //public string? CName { get; set; } = string.Empty;
        //public string? CSurname { get; set; } = string.Empty;
        //public string? CAdd { get; set; } = string.Empty;
        //public string? CCon { get; set; } = string.Empty;
        //public string? Cmobile { get; set; } = string.Empty;
        //public string? CFax { get; set; } = string.Empty;
        //public string? ConPrsn { get; set; } = string.Empty;

        /// <summary>
        /// Collection of Areas
        /// </summary>
        public ICollection<FileFlowAreasDto> Areas { get; set; } = new List<FileFlowAreasDto>();
    }
}
