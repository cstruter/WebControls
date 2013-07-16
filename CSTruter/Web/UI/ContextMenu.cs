using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSTruter.Web.UI.Extensions;

namespace CSTruter.Web.UI
{
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    [ParseChildren(true, "Items")]
    public class ContextMenu : CompositeControl, IPostBackEventHandler
    {
        #region Fields

        private List<ContextMenuItem> _items = new List<ContextMenuItem>();

        #endregion

        #region Delegates

        public delegate void ContextMenuDelegate(object sender, ContextMenuEventArgs e);

        #endregion

        #region Public Properties

        [Browsable(true)]
        [Category("Action")]
        public String OnClientClick
        {
            get;
            set;
        }

        [Category("Action")]
        public event ContextMenuDelegate Click;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Category("Default")]
        public List<ContextMenuItem> Items
        {
            get
            {
                return _items;
            }
        }

        [Browsable(true)]
        [Category("Default")]
        public String TargetSelector
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("Appearance")]
        public System.Drawing.Color HoverForeColor
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("Appearance")]
        public System.Drawing.Color HoverBackColor
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("Appearance")]
        public System.Drawing.Color InactiveForeColor
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("Appearance")]
        public System.Drawing.Color InactiveBackColor
        {
            get;
            set;
        }

        #endregion

        #region Overridden

        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CssClass))
            {
                this.CssClass = "cstruter_contextmenu";
            }
            Page.ClientScript.RegisterClientStyleInclude(this.GetType(), "ContextMenuStyles", Page.ClientScript.GetWebResourceUrl(this.GetType(), "CSTruter.Web.UI.css.ContextMenu.css"));
            Page.ClientScript.RegisterClientScriptInclude("Core", Page.ClientScript.GetWebResourceUrl(this.GetType(), "CSTruter.Web.UI.js.Core.js"));
            Page.ClientScript.RegisterClientScriptInclude("ContextMenu", Page.ClientScript.GetWebResourceUrl(this.GetType(), "CSTruter.Web.UI.js.ContextMenu.js"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ContextMenuBlock", @"CSTruter.ContextMenu.Init();", true);
            base.OnInit(e);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Ul;
            }
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.Write("[" + this.ID + "]");
                base.RenderChildren(writer);
                return;
            }

            if (string.IsNullOrEmpty(this.TargetSelector))
            {
                throw new ArgumentException(string.Format("Control {0} requires a TargetSelector", this.ID));
            }

            if (Items.Count == 0)
            {
                return;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(),
                string.Format("ContextMenuBlock{0}", this.ClientID),
                string.Format(@"$(""{0}"").on(""contextmenu"", function(e) {{ return CSTruter.ContextMenu.Handler(e, ""#{1}""); }});",
                    this.TargetSelector,
                    this.ClientID), true);

            foreach (ContextMenuItem item in Items)
            {
                if (!item.Visible)
                {
                    continue;
                }
                writer.WriteBeginTag("li");
                if (item.Active)
                {   
                    RenderHoverStyles(writer);
                    RenderClick(item, writer);
                }
                else
                {   
                    RenderInactive(writer);
                }
                writer.Write(">");

                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    Image image = new Image();
                    image.ImageUrl = item.ImageUrl;
                    image.RenderControl(writer);
                }

                writer.Write(item.Text);
                writer.WriteEndTag("li");
            }

            base.RenderChildren(writer);
        }

        #endregion

        #region Protected & Private Methods

        protected void RenderHoverStyles(HtmlTextWriter writer)
        {
            writer.WriteAttribute("class", "cstruter_contextmenu_option cstruter_contextmenu_active");
            if ((!this.HoverForeColor.IsEmpty) || (!this.HoverBackColor.IsEmpty))
            {
                writer.WriteAttribute("onmouseover",
                    string.Format("CSTruter.ContextMenu.Hover(this, '{0}', '{1}')",
                        System.Drawing.ColorTranslator.ToHtml(this.HoverForeColor),
                        System.Drawing.ColorTranslator.ToHtml(this.HoverBackColor)));
                if ((this.ForeColor.IsEmpty) && (this.BackColor.IsEmpty))
                {
                    writer.WriteAttribute("onmouseout", "CSTruter.ContextMenu.Hover(this, '#000', '#CCC')");
                }
            }
            if ((!this.ForeColor.IsEmpty) || (!this.BackColor.IsEmpty))
            {
                writer.WriteAttribute("onmouseout",
                    string.Format("CSTruter.ContextMenu.Hover(this, '{0}', '{1}')",
                        System.Drawing.ColorTranslator.ToHtml(this.ForeColor),
                        System.Drawing.ColorTranslator.ToHtml(this.BackColor)));
            }
        }

        protected void RenderClick(ContextMenuItem item, HtmlTextWriter writer)
        {
            string clickValue = "";
            if (!string.IsNullOrEmpty(this.OnClientClick))
            {
                clickValue += this.OnClientClick + string.Format("({0});", HttpUtility.HtmlEncode(@"""" + item.CommandName + @""""));
            }
            if (this.Click != null)
            {
                clickValue += Page.ClientScript.GetPostBackEventReference(this, item.CommandName) + ";";
            }
            if (!string.IsNullOrEmpty(clickValue))
            {
                writer.WriteAttribute("onclick", clickValue);
            }
        }

        protected void RenderInactive(HtmlTextWriter writer)
        {
            writer.WriteAttribute("class", "cstruter_contextmenu_option cstruter_contextmenu_inactive");
            string styles = "";
            if (!InactiveForeColor.IsEmpty)
            {
                styles += string.Format("color:{0};", System.Drawing.ColorTranslator.ToHtml(InactiveForeColor));
            }
            if (!InactiveBackColor.IsEmpty)
            {
                styles += string.Format("background-color:{0};", System.Drawing.ColorTranslator.ToHtml(InactiveBackColor));
            }
            if (!string.IsNullOrEmpty(styles))
            {
                writer.WriteAttribute("style", styles);
            }
        }

        protected virtual void OnClick(ContextMenuEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        #endregion

        #region IPostBackEventHandler Implementation

        public void RaisePostBackEvent(string eventArgument)
        {
            ContextMenuEventArgs args = new ContextMenuEventArgs();
            args.CommandName = eventArgument;
            OnClick(args);
        }

        #endregion
    }

}
