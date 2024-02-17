using Installation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Model.Installation
{
    public class FileFlowAreasDto
    {
        public int ID { get; set; }

        public int ffID { get; set; }

        public int ffno { get; set; }

        public string? TransactionID { get; set; }

        public string? PArea { get; set; }
        public int NumMod { get; set; }
        public string? source { get; set; }
        public string? AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Actn { get; set; }
        public string? liable { get; set; }
        public DateTime DateAltered { get; set; }
        public string? Categry { get; set; }
        public string? Mat { get; set; }
        public string? Tech { get; set; }
        public string? OrderID { get; set; }
        public string? src { get; set; }
        public string? CMfinal { get; set; }
    }
}
