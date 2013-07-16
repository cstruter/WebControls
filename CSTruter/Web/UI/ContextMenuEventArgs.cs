using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTruter.Web.UI
{
    public class ContextMenuEventArgs : EventArgs
    {
        #region Properties

        public String CommandName
        {
            get;
            set;
        }

        #endregion
    }
}
