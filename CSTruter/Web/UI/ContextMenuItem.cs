using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace CSTruter.Web.UI
{
    public class ContextMenuItem
    {
        #region Fields

        private bool _active = true;
        private bool _visible = true;

        #endregion

        #region Properties

        [DefaultValue(true)]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        [DefaultValue(true)]
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public string Text { get; set; }

        public string CommandName { get; set; }

        [UrlProperty]
        public string ImageUrl { get; set; }

        #endregion
    }
}
