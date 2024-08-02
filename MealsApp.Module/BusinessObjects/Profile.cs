using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MealsApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [DefaultProperty("Profile")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("Profile")]
    //Specify more UI options using a declarative approach(https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Profile : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Profile(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string _Title;
        private string _Initials;
        private string _Surname;
        private string _Email;
        private Department _Department;
        private int? _Building_Number;
        private int? _Floor;

        [XafDisplayName("Title"), ToolTip("Individual Title")]
        [Persistent("Title"), RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return _Title; }
            set { SetPropertyValue(nameof(Title), ref _Title, value); }
        }

        [XafDisplayName("Initials"), ToolTip("Persons Initials")]
        [Persistent("Initials"), RuleRequiredField(DefaultContexts.Save)]
        public string Initials
        {
            get { return _Initials; }
            set { SetPropertyValue(nameof(Initials), ref _Initials, value); }
        }

        [XafDisplayName("Surname"), ToolTip("Individual Surname")]
        [Persistent("Surname"), RuleRequiredField(DefaultContexts.Save)]
        public string Surname
        {
            get { return _Surname; }
            set { SetPropertyValue(nameof(Surname), ref _Surname, value); }
        }

        // Email property with regular expression validation
        [RuleRequiredField("Email address is required", DefaultContexts.Save)]
        [RuleRegularExpression(DefaultContexts.Save, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", CustomMessageTemplate = "Invalid email address format")]
        public string Email
        {
            get => _Email;
            set => SetPropertyValue(nameof(Email), ref _Email, value);
        }
        [XafDisplayName("Department"), ToolTip("Department individual belongs to")]
        [Persistent("Department"), RuleRequiredField(DefaultContexts.Save)]
        public Department Department
        {
            get { return _Department; }
            set { SetPropertyValue(nameof(Department), ref _Department, value); }
        }

        [XafDisplayName("Building Number"), ToolTip("The Building Number")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 1, CustomMessageTemplate = "Building number must be a positive integer")]
        public int? Building_Number
        {
            get => _Building_Number;
            set => SetPropertyValue(nameof(Building_Number), ref _Building_Number, value);
        }


        [XafDisplayName("Floor"), ToolTip("The Floor")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 1, CustomMessageTemplate = "Floor must be a positive integer")]

        public int? Floor
        {
            get { return _Floor; }
            set { SetPropertyValue(nameof(Floor), ref _Floor, value); }
        }

        [Association("Profile-Telephone")]
        public XPCollection<Telephone> Telephone
        {
            get { return GetCollection<Telephone>(nameof(Telephone)); }
        }

        // Custom validation rule to ensure at least one telephone number exists
        [RuleFromBoolProperty("AtLeastOneTelephoneNumberRule", DefaultContexts.Save, "At least one telephone number is required")]
        public bool AtLeastOneTelephoneNumber
        {
            get { return Telephone != null && Telephone.Count > 0; }
        }




        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}