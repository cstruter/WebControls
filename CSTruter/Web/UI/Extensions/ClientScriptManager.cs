using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTruter.Web.UI.Extensions
{
    public static class ClientScriptManager
    {
        public static void RegisterClientStyleInclude(this System.Web.UI.ClientScriptManager csm, Type type, string key, string script)
        {
            string styles = "<link href=\"" + script + "\" type=\"text/css\" rel=\"stylesheet\" />";
            csm.RegisterClientScriptBlock(type, key, styles, false);
        }
    }
}
