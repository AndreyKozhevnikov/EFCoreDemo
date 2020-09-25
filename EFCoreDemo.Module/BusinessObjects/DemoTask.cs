using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Data.Extensions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.EFCore.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    [Appearance("FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView", Criteria = "Status!='Completed'", FontColor = "Red")]
    [RuleCriteria("Task_Status", DefaultContexts.Save, "IIF(Status != 'NotStarted' and Status != 'Deferred', AssignedTo is not null, True)", CustomMessageTemplate = @"The task must have an assignee when its Status is ""In progress"", ""Waiting for someone else"", or ""Completed"".", SkipNullOrEmptyValues = false)]
    [RuleCriteria("TaskIsNotStarted", DefaultContexts.Save, "Status != 'NotStarted'", CustomMessageTemplate = "Cannot set the task completed because it's not started.", TargetContextIDs = "MarkCompleted")]
    public class DemoTask : Task {
        public DemoTask()
            : base() {
            Priority = Priority.Normal;
            TaskContacts = new List<ContactDemoTask>();
        }

        public Nullable<Int32> EstimatedWork { get; set; }
        public Nullable<Int32> ActualWork { get; set; }
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual IList<ContactDemoTask> TaskContacts { get; set; }
        [NotMapped]
        public virtual IList<Contact> Contacts => new EFCoreExtendedList<ContactDemoTask, Contact>(TaskContacts, c => c.Contact, AddContact, RemoveContact);
        private void AddContact(Contact contact) {
            TaskContacts.Add(new ContactDemoTask {
                Task = this,
                Contact = contact
            });
        }
        private void RemoveContact(Contact contact) {
            int index = TaskContacts.FindIndex(c => c.ContactID == contact.ID);
            if(index > -1) {
                TaskContacts.RemoveAt(index);
            }
        }
        private Priority priority;
        [Appearance("PriorityBackColorPink", AppearanceItemType = "ViewItem", Criteria = "Priority=2", BackColor = "0xfff0f0")]
        public Priority Priority {
            get { return priority; }
            set { SetPropertyValue(ref priority, value); }
        }

        public override String ToString() {
            return Subject;
        }
        [Action(ToolTip = "Postpone the task to the next day", ImageName = "State_Task_Deferred")]
        public void Postpone() {
            if(DueDate == DateTime.MinValue) {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }
        [RuleValueComparison("Task_ActualWorkHours", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        public int ActualWorkHours { get; set; }
        [RuleValueComparison("Task_EstimatedWorkHours", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        public int EstimatedWorkHours { get; set; }
    }
    public enum Priority {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}
