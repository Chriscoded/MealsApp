using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
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
    public class Orders : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Orders(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OrderDateTime = DateTime.Now;
            //CreatedBy = Session.GetObjectByKey<SecuritySystemUser>(SecuritySystem.CurrentUserId);
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            base.OnSaving();
            if (Session.IsNewObject(this))
            {
                CreatedBy = GetCurrentUser();
            }
            else
            {
                UpdatedBy = GetCurrentUser();
            }
        }


        private ApplicationUser GetCurrentUser()
        {
            var userId = SecuritySystem.CurrentUserId;
            return Session.GetObjectByKey<ApplicationUser>(userId);
        }
        private DateTime _OrderDateTime;
        private DateTime _DeliveryDateTime;
        private DateTime _DeliveredDateTime;
        private OrderStatus _Status;
        private ApplicationUser _CreatedBy;
        private ApplicationUser _UpdatedBy;

        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime OrderDateTime
        {
            get => _OrderDateTime;
            set => SetPropertyValue(nameof(OrderDateTime), ref _OrderDateTime, value);
        }

        public DateTime DeliveryDateTime
        {
            get => _DeliveryDateTime;
            set => SetPropertyValue(nameof(DeliveryDateTime), ref _DeliveryDateTime, value);
        }

        public DateTime DeliveredDateTime
        {
            get => _DeliveredDateTime;
            set => SetPropertyValue(nameof(DeliveredDateTime), ref _DeliveredDateTime, value);
        }

        public OrderStatus Status
        {
            get => _Status;
            set => SetPropertyValue(nameof(Status), ref _Status, value);
        }


        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Association("ApplicationUser-CreatedOrders")]
        public ApplicationUser CreatedBy
        {
            get => _CreatedBy;
            set => SetPropertyValue(nameof(CreatedBy), ref _CreatedBy, value);
        }

        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Association("ApplicationUser-UpdatedOrders")]
        public ApplicationUser UpdatedBy
        {
            get => _UpdatedBy;
            set => SetPropertyValue(nameof(UpdatedBy), ref _UpdatedBy, value);
        }


        [PersistentAlias("OrderLines.Sum(Amount)")]
        public decimal TotalExcludeVAT
        {
            get
            {
                object tempObject = EvaluateAlias(nameof(TotalExcludeVAT));
                return tempObject == null ? 0 : (decimal)tempObject;
            }
        }

        [PersistentAlias("OrderLines.Sum(Amount) * 0.15")] // Assuming VAT is 15%
        public decimal TotalVat
        {
            get
            {
                object tempObject = EvaluateAlias(nameof(TotalVat));
                return tempObject == null ? 0 : (decimal)tempObject;
            }
        }

        [PersistentAlias("TotalExcludeVAT + TotalVat")]
        public decimal TotalOrderPrice
        {
            get
            {
                object tempObject = EvaluateAlias(nameof(TotalOrderPrice));
                return tempObject == null ? 0 : (decimal)tempObject;
            }
        }

        [Association("Order-OrderLines")]
        public XPCollection<OrderLines> OrderLines
        {
            get { return GetCollection<OrderLines>(nameof(OrderLines)); }
        }
    }

    public enum OrderStatus
    {
        OnOrder,
        Processed,
        Delivered
    }
}