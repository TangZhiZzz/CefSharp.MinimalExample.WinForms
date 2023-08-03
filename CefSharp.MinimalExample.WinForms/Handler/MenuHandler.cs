using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Handler
{
    internal class MenuHandler : IContextMenuHandler
    {
        public void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            IMenuModel model)
        {
            model.Clear();
        }

        public bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser webBrowser, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
