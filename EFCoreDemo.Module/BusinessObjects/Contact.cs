using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Data.Extensions;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore.Utils;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Contact : Person, IObjectSpaceLink {
        IObjectSpace objectSpace;
        private int titleOfCourtesyInt;
        public Contact() {
            Resumes = new List<Resume>();
            Contacts = new List<Contact>();
            ContactTasks = new List<ContactDemoTask>();
        }
        [RuleRegularExpression(@"(((http|https)\://)[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})", CustomMessageTemplate = @"Invalid ""Web Page Address"".")]
        public String WebPageAddress { get; set; }
        public String NickName { get; set; }
        public String SpouseName { get; set; }
        [Browsable(false)]
        public Int32 TitleOfCourtesy_Int {
            get { return titleOfCourtesyInt; }
            protected set { SetPropertyValue(ref titleOfCourtesyInt, value); }
        }
        public Nullable<DateTime> Anniversary { get; set; }
        [FieldSize(4096)]
        [Browsable(false)]
        public String Notes { get; set; }
        private Department department;
        [ImmediatePostData]
        public virtual Department Department {
            get {
                return department;
            }
            set {
                department = value;
                Position = null;
                if(Manager != null && Manager.Department != value) {
                    Manager = null;
                }
            }
        }
        public virtual Position Position { get; set; }
        [Browsable(false)]
        public virtual IList<Resume> Resumes { get; set; }
        [DataSourceProperty("Department.Contacts", DataSourcePropertyIsNullMode.SelectAll), DataSourceCriteria("Position.Title = 'Manager'")]
        public virtual Contact Manager { get; set; }
        public virtual IList<Contact> Contacts { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual IList<ContactDemoTask> ContactTasks { get; set; }
        [NotMapped]
        public virtual IList<DemoTask> Tasks => new EFCoreExtendedList<ContactDemoTask, DemoTask>(ContactTasks, t => t.Task, AddTask, RemoveTask);

        [NotMapped]
        public TitleOfCourtesy TitleOfCourtesy {
            get { return (TitleOfCourtesy)TitleOfCourtesy_Int; }
            set { TitleOfCourtesy_Int = (Int32)value; }
        }
        private Location location;
        [ExpandObjectMembers(ExpandObjectMembers.Never), VisibleInListView(false)]
        public virtual Location Location {
            get {
                return location;
            }
            set {
                SetReferencePropertyValue(ref location, value);
            }
        }
        IObjectSpace IObjectSpaceLink.ObjectSpace {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        private void AddTask(DemoTask task) {
            ContactTasks.Add(new ContactDemoTask {
                Contact = this,
                Task = task
            });
        }
        private void RemoveTask(DemoTask task) {
            int index = ContactTasks.FindIndex(t => t.TaskID == task.ID);
            if(index > -1) {
                ContactTasks.RemoveAt(index);
            }
        }
    }

    public enum TitleOfCourtesy {
        Dr,
        Miss,
        Mr,
        Mrs,
        Ms
    }
}

