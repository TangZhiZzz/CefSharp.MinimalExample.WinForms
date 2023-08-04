using CefSharp;
using CefSharp.MinimalExample.WinForms.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Handler
{
    internal class MyResourceRequestHandler : IResourceRequestHandler
    {

        public MyResourceRequestHandler(FilterData Filter, bool _filter)
        {
            filterData = Filter;
            filter = _filter;
        }

        bool Disposed = false;
        /// <summary>
        /// 是否过滤，如果不过滤就截获
        /// </summary>
        bool filter = false;
        /// <summary>
        /// 过滤、截获 url 列表 和 捕获过滤数据
        /// </summary>
        FilterData filterData { get; set; }

        public void Dispose()
        {
            if (Disposed)
                return;
            Disposed = true;
        }

        public ICookieAccessFilter GetCookieAccessFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

        public IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            var fil = new MyResponseFilter(filter);
            filterData.AddFilter(request, fil);
            return fil;

        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            return CefReturnValue.Continue;
        }

        public bool OnProtocolExecution(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return false;
        }

        public void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
        }

        public void OnResourceRedirect(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
        }

        public bool OnResourceResponse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
           
            return false;
        }
    }
}
