using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Data.Extensions;
using DevExpress.ExpressApp.EFCore.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [DefaultProperty(nameof(Department.Title))]
    [RuleCriteria("Department_PositionsIsNotEmpty", DefaultContexts.Save, "Positions.Count > 0", CustomMessageTemplate = "The Department must contain at least one position.")]
    [RuleCriteria("Department_ContactsIsNotEmpty", DefaultContexts.Save, "Contacts.Count > 0", CustomMessageTemplate = "The Department must contain at least one employee.")]
    public class Department {
        public Department() {
            DepartmentPositions = new List<DepartmentPosition>();
            Contacts = new List<Contact>();
        }
        //[Browsable(false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 ID { get; protected set; }
        [RuleRequiredField]
        public String Title { get; set; }
        public String Office { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual IList<DepartmentPosition> DepartmentPositions { get; set; }
        [NotMapped]
        public virtual IList<Position> Positions => new EFCoreExtendedList<DepartmentPosition, Position>(DepartmentPositions, d => d.Position, AddPosition, RemovePosition);
        public virtual IList<Contact> Contacts { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        [DataSourceProperty("Contacts", DataSourcePropertyIsNullMode.SelectAll)]
        public virtual Contact DepartmentHead { get; set; }

        public void AddPosition(Position position) {
            DepartmentPositions.Add(new DepartmentPosition {
                Department = this,
                Position = position
            });
        }
        public void RemovePosition(Position position) {
            int index = DepartmentPositions.FindIndex(d => d.PositionID == position.ID);
            if(index > -1) {
                DepartmentPositions.RemoveAt(index);
            }
        }
    }
}
