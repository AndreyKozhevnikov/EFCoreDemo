using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace EFCoreDemo.Module.BusinessObjects {
    [DefaultProperty(nameof(FileName))]
    public class FileData : IFileData, IEmptyCheckable, IObjectSpaceLink, INotifyPropertyChanged {
        private Int32 id;
        private Int32 size;
        private Byte[] content;
        private string fileName;

        [Key]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Int32 ID {
            get { return id; }
            protected set { id = value; }
        }
        public Int32 Size {
            get { return size; }
            set { SetPropertyValue(ref size, value); }
        }


        public String FileName {
            get { return fileName; }
            set { SetPropertyValue(ref fileName, value); }
        }
        public Byte[] Content {
            get { return content; }
            set {
                if(SetReferencePropertyValue(ref content, value)) {
                    Size = content != null ? content.Length : 0;
                }
            }
        }

        [NotMapped, Browsable(false)]
        public Boolean IsEmpty {
            get { return String.IsNullOrEmpty(FileName); }
        }

        public void LoadFromStream(String fileName, Stream stream) {
            FileName = fileName;
            Byte[] bytes = new Byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            Content = bytes;
            ObjectSpace.SetModified(this);
        }
        public void SaveToStream(Stream stream) {
            if(String.IsNullOrEmpty(FileName)) {
                throw new InvalidOperationException();
            }
            stream.Write(Content, 0, Size);
            stream.Flush();
        }
        public void Clear() {
            Content = null;
            FileName = "";
            ObjectSpace.SetModified(this);
        }
        public override String ToString() {
            return FileName;
        }
        [Browsable(false)]
        [NotMapped]
        public IObjectSpace ObjectSpace { get; set; }

        #region INotifyPropertyChanged
        private PropertyChangedEventHandler propertyChanged;
        protected bool SetPropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : struct {
            if(EqualityComparer<T>.Default.Equals(propertyValue, newValue)) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetPropertyValue<T>(ref T? propertyValue, T? newValue, [CallerMemberName]string propertyName = null) where T : struct {
            if(EqualityComparer<T?>.Default.Equals(propertyValue, newValue)) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetPropertyValue(ref string propertyValue, string newValue, [CallerMemberName]string propertyName = null) {
            if(propertyValue == newValue) {
                return false;
            }
            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool SetReferencePropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName]string propertyName = null) where T : class {
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
        public event PropertyChangedEventHandler PropertyChanged {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }
        #endregion
    }
}