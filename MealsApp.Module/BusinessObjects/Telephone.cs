using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MealsApp.Module.BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MealsApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("Profile Telephone")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("Telephone")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Telephone : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Telephone(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private TelephoneType _TelephoneType;
        private string _TelephoneNumber;
        private bool _Active;
        private DateTime _DateCreated;
        private Profile _Profile;

        [XafDisplayName("Telephone Type"), ToolTip("Telephone Type")]
        public TelephoneType TelephoneType
        {
            get { return _TelephoneType; }
            set { SetPropertyValue(nameof(TelephoneType), ref _TelephoneType, value); }
        }

        [XafDisplayName("Telephone Number"), ToolTip("Telephone Number")]
        [ModelDefault("EditMask", "(000) 000-0000"), Index(0), VisibleInListView(true)]

        [Persistent("TelephoneNumber"), RuleRequiredField(DefaultContexts.Save)]
        public string TelephoneNumber
        {
            get { return _TelephoneNumber; }
            set { SetPropertyValue(nameof(TelephoneNumber), ref _TelephoneNumber, value); }
        }

        [XafDisplayName("Active"), ToolTip("Active ?")]
        [Persistent("Active")]
        public bool Active
        {
            get { return _Active; }
            set { SetPropertyValue(nameof(Active), ref _Active, value); }
        }

        [XafDisplayName("Date Created"), ToolTip("Date Created")]
        [Persistent("DateCreated"), RuleRequiredField(DefaultContexts.Save)]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { SetPropertyValue(nameof(DateCreated), ref _DateCreated, value); }
        }

        [Association("Profile-Telephone")]
        public Profile Profile
        {
            get { return _Profile; }
            set { SetPropertyValue(nameof(Profile), ref _Profile, value); }
        }


        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}