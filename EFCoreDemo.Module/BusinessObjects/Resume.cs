using System;
using System.Collections.Generic;

using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace EFCoreDemo.Module.BusinessObjects {
    //[DefaultClassOptions]
    [ImageName("BO_Resume")]
    public class Resume {
        public Resume() {
            Portfolio = new List<PortfolioFileData>();
        }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 ID { get; protected set; }
        [Aggregated]
        public virtual IList<PortfolioFileData> Portfolio { get; set; }
        public virtual Contact Contact { get; set; }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public virtual FileData File { get; set; }
    }
}
