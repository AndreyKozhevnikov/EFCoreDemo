using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [RuleCriteria("Payroll_Hours_PayPeriod_Range", DefaultContexts.Save, "DateDiffHour(PayPeriodStart, PayPeriodEnd) >= [Hours] + [OvertimeHours]", CustomMessageTemplate = @"Sum of ""Hours"" and ""Overtime hours"" must be less than or equal to the difference between ""Pay Period End"" and ""Pay Period Start"" in hours.")]
    public class Paycheck : INotifyPropertyChanged {
        private Contact contact;
        private int payPeriod;
        private DateTime paymentDate;
        private DateTime payPeriodEnd;
        private DateTime payPeriodStart;
        private decimal payRate;
        private int hours;
        private decimal overtimePayRate;
        private int overtimeHours;
        private double taxRate;
        private string notes;
        private Int32 id;

        public Paycheck() {
            DateTime now = DateTime.Now;
            this.payPeriod = (2 * (now.Month - 1)) + (now.Day > 15 ? 2 : 1);
            this.payPeriodStart = new DateTime(day: 1, month: now.Month, year: now.Year);
            this.payPeriodEnd = new DateTime(day: (now.Day > 15 ? DateTime.DaysInMonth(now.Year, now.Month) : 15), month: now.Month, year: now.Year);
            this.paymentDate = this.payPeriodEnd;
        }
        [Key]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 ID {
            get { return id; }
            protected set { id = value; }
        }
        [RuleRequiredField]
        [ImmediatePostData]        
        public virtual Contact Contact {
            get { return contact; }
            set {
                SetReferencePropertyValue(ref contact, value);
            }
        }
        [RuleRange(DefaultContexts.Save, 0, 26)]
        public int PayPeriod {
            get { return payPeriod; }
            set { SetPropertyValue(ref payPeriod, value); }
        }
        [RuleRequiredField]
        public DateTime PayPeriodStart {
            get { return payPeriodStart; }
            set { SetPropertyValue(ref payPeriodStart, value); }
        }
        [RuleRequiredField]
        [RuleValueComparison("Payroll_PeriodStart_PeriodEnd", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, nameof(PayPeriodStart), ParametersMode.Expression)]
        [RuleValueComparison("Payroll_PaymentDate_PeriodEnd", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, nameof(PaymentDate), ParametersMode.Expression)]
        public DateTime PayPeriodEnd {
            get { return payPeriodEnd; }
            set { SetPropertyValue(ref payPeriodEnd, value); }
        }
        public DateTime PaymentDate {
            get { return paymentDate; }
            set { SetPropertyValue( ref paymentDate, value); }
        }
        [ImmediatePostData]
        [RuleValueComparison("Payroll_PayRate", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public decimal PayRate {
            get { return payRate; }
            set {
                if(SetPropertyValue(ref payRate, value))
                    NotifyCalculatedPropertiesChanged();
            }
        }
        [ImmediatePostData]
        [RuleValueComparison("Payroll_Hours", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        public int Hours {
            get { return hours; }
            set {
                if(SetPropertyValue(ref hours, value))
                    NotifyCalculatedPropertiesChanged();
            }
        }
        [ImmediatePostData]
        [RuleValueComparison("Payroll_OvertimePayRate", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public decimal OvertimePayRate {
            get { return overtimePayRate; }
            set {
                if(SetPropertyValue(ref overtimePayRate, value))
                    NotifyCalculatedPropertiesChanged();
            }
        }
        [ImmediatePostData]
        [RuleValueComparison("Payroll_OvertimeHours", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        public int OvertimeHours {
            get { return overtimeHours; }
            set {
                if(SetPropertyValue(ref overtimeHours, value))
                    NotifyCalculatedPropertiesChanged();
            }
        }
        [ImmediatePostData]
        [RuleRange(DefaultContexts.Save, 0, 100)]
        public double TaxRate {
            get { return taxRate; }
            set {
                if(SetPropertyValue( ref taxRate, value))
                    NotifyCalculatedPropertiesChanged();
            }
        }
        public string Notes {
            get {
                return notes;
            }
            set {
                SetPropertyValue(ref notes, value);
            }
        }
        private void NotifyCalculatedPropertiesChanged() {
            OnPropertyChanged(nameof(TotalTax));
            OnPropertyChanged(nameof(GrossPay));
            OnPropertyChanged(nameof(NetPay));
        }
        [NotMapped]
        public decimal TotalTax {
            get {
                return Convert.ToDecimal(Convert.ToDouble((PayRate * Hours) + (OvertimePayRate * OvertimeHours)) * TaxRate);
            }
        }
        [NotMapped]
        public decimal GrossPay {
            get {
                return (decimal)(((PayRate * Hours) + (OvertimePayRate * OvertimeHours)));
            }
        }
        [NotMapped]
        public decimal NetPay {
            get {
                return (decimal)GrossPay - TotalTax;
            }
        }
        #region INotifyPropertyChanged
        private PropertyChangedEventHandler propertyChanged;
        protected bool SetPropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName] string propertyName = null) where T : struct {
            if(EqualityComparer<T>.Default.Equals(propertyValue, newValue)) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetPropertyValue<T>(ref T? propertyValue, T? newValue, [CallerMemberName] string propertyName = null) where T : struct {
            if(EqualityComparer<T?>.Default.Equals(propertyValue, newValue)) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetPropertyValue(ref string propertyValue, string newValue, [CallerMemberName] string propertyName = null) {
            if(propertyValue == newValue) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetReferencePropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName] string propertyName = null) where T : class {
            if(propertyValue == newValue) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string propertyName) {
            if(propertyChanged != null) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }
        #endregion
    }
}
