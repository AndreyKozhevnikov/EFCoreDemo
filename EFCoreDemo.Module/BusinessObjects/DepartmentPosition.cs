using DevExpress.Persistent.Base;

namespace EFCoreDemo.Module.BusinessObjects {
    public class DepartmentPosition {
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ID { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int DepartmentID { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PositionID { get; set; }

        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
    }
}