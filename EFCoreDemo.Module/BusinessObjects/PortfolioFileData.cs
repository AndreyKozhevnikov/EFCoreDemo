using System;
using System.ComponentModel.DataAnnotations.Schema;

using DevExpress.Persistent.Base;

namespace EFCoreDemo.Module.BusinessObjects {
    [ImageName("BO_FileAttachment")]
    public class PortfolioFileData : FileAttachment {
        public PortfolioFileData()
            : base() {
            DocumentType = DocumentType.Unknown;
        }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 DocumentType_Int { get; protected set; }
        public virtual Resume Resume { get; set; }

        [NotMapped]
        public DocumentType DocumentType {
            get { return (DocumentType)DocumentType_Int; }
            set { DocumentType_Int = (Int32)value; }
        }
    }
    public enum DocumentType {
        SourceCode = 1,
        Tests = 2,
        Documentation = 3,
        Diagrams = 4,
        ScreenShots = 5,
        Unknown = 6
    }
}
