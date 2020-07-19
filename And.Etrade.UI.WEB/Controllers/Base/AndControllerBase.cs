using And.Etrade.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace And.Etrade.UI.WEB.Controllers.Base
{
    public class AndControllerBase : Controller
    {
        //Kullanıcı Login Controller
        public bool isLogin { get; private set; }
        public int LoginUserID { get; private set; }
        public User LoginUserEntity { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}