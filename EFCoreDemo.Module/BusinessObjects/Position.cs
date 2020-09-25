using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Data.Extensions;
using DevExpress.ExpressApp.EFCore.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [DefaultProperty(nameof(Position.Title))]
    public class Position {
        public Position() {
            PositionDepartments = new List<DepartmentPosition>();
            Contacts = new List<Contact>();
        }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 ID { get; protected set; }
        [RuleRequiredField("RuleRequiredField for Position.Title", DefaultContexts.Save)]
        public String Title { get; set; }
        
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual IList<DepartmentPosition> PositionDepartments { get; set; }

        [NotMapped]
        public virtual IList<Department> Departments => new EFCoreExtendedList<DepartmentPosition, Department>(PositionDepartments, p => p.Department, AddDepartment, RemoveDepartment);

        public virtual IList<Contact> Contacts { get; set; }

        public void AddDepartment(Department department) {
            PositionDepartments.Add(new DepartmentPosition {
                Position = this,
                Department = department
            });
        }
        public void RemoveDepartment(Department department) {
            int index = PositionDepartments.FindIndex(d => d.DepartmentID == department.ID);
            if(index > -1) {
                PositionDepartments.RemoveAt(index);
            }
        }
    }
}
