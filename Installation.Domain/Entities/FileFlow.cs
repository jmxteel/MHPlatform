using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.Entities
{
	public class FileFlow
	{
		public int ID { get; set; }

		public string? OrderID { get; set; }

		public int FileFlowNo { get; set; }

		public string? datecreated { get; set; }

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

		public string? reasons { get; set; }

		public DateTime? LeadStart { get; set; }

		public DateTime? LeadClosed { get; set; }

		public string? Revision { get; set; }

		public DateTime? chckddocsDate { get; set; }

		public DateTime? chckdforTechnclDate { get; set; }

		public DateTime? ApprovedDate { get; set; }

		public DateTime? leftshwroom { get; set; }

		public DateTime? recvinplant { get; set; }

		public string? shwrm { get; set; }

		public string? deleted { get; set; }

		public string? deletedby { get; set; }

		public string? GrpngSysGen { get; set; }

		public string? GrpngCtgry { get; set; }

		public string? GrpngMat { get; set; }

		public string? ManualGrpngCtgry { get; set; }

		public string? ManualGrpngMat { get; set; }

		public DateTime? chckbySalesDesigner { get; set; }

		public string? variation { get; set; }

		public string? workingDaysOver { get; set; }

		public string? workingDaysUnder { get; set; }

		public string? FFsrc { get; set; }

		public string? FGrouping { get; set; }

		public string? MGrouping { get; set; }

		public string? SysVer { get; set; }

		public string? typ { get; set; }

		public string? cbfpaging { get; set; }

		public string? fpaging { get; set; }

		[Column("lock")]
		public string? islock { get; set; }

		public string? lockedby { get; set; }

		public string? MHGrouping { get; set; }

		public string? samplecolor { get; set; }
	}
}
