﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace MealsApp.Module.BusinessObjects {
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DefaultProperty(nameof(UserName))]
    public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo {
        public ApplicationUser(Session session) : base(session) { }

        [Browsable(false)]
        [Aggregated, Association("User-LoginInfo")]
        public XPCollection<ApplicationUserLoginInfo> LoginInfo {
            get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
        }

        [Association("ApplicationUser-CreatedOrders")]
        public XPCollection<Orders> CreatedOrders => GetCollection<Orders>(nameof(CreatedOrders));

        [Association("ApplicationUser-UpdatedOrders")]
        public XPCollection<Orders> UpdatedOrders => GetCollection<Orders>(nameof(UpdatedOrders));


        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
            ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }
    }
}
