using System.ComponentModel.DataAnnotations;

namespace EFCoreDemo.Module.BusinessObjects {
    public class ContactDemoTask {
        [Key]
        public int ID { get; protected set; }
        
        public int ContactID { get; set; }
        public int TaskID { get; set; }
        
        public virtual Contact Contact { get; set; }
        public virtual DemoTask Task { get; set; }
    }
}
