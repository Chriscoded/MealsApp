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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class OrderLines : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public OrderLines(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            
        }

        private Orders order;
        private Meals orderItem;
        private int quantity;
        private decimal unitPrice;

        [Association("Order-OrderLines")]
        public Orders Order
        {
            get => order;
            set => SetPropertyValue(nameof(Order), ref order, value);
        }

        public Meals OrderItem
        {
            get => orderItem;
            set
            {
                SetPropertyValue(nameof(OrderItem), ref orderItem, value);
                if (!IsLoading && !IsSaving && value != null)
                {
                    UnitPrice = (decimal)value.Price;
                }
            }
        }

        [RuleRange(1, 10, CustomMessageTemplate = "Quantity must be between 1 and 10")]
        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal UnitPrice
        {
            get => unitPrice;
            set
            {
                SetPropertyValue(nameof(UnitPrice), ref unitPrice, value);
                //unitPrice = (decimal)OrderItem.Price;
                //UnitPrice = (decimal)OrderItem.Price;

            }
        }

        [PersistentAlias("Quantity * UnitPrice")]
        public decimal Amount
        {
            get
            {
                object tempObject = EvaluateAlias(nameof(Amount));
                return tempObject == null ? 0 : (decimal)tempObject;
            }
        }
    }

}