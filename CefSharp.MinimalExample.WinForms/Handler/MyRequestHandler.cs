using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Handler
{
    internal class MyRequestHandler : IRequestHandler, IDisposable
    {
        public MyRequestHandler()
        {

        }
        public void Dispose()
        {
            if (Disposed)
                return;
            handler?.Dispose();
            Filters?.ForEach(f => f.Dispose());
            Filters?.Clear();
            Filters = null;
            Disposed = true;
        }
        public override string ToString()
        {
            if (!Filters?.Any() ?? true)
                return string.Empty;
            return string.Join("\r\n", Filters.Select(f => f.ToString()));
        }

        public MyRequestHandler(string name, string _url, HttpMethod _Method = HttpMethod.GET, bool _filter = false)
        {
            Filters.Add(new FilterData(name, _url, _Method));
            filter = _filter;
        }
        public MyRequestHandler(Dictionary<string, string> _urls, HttpMethod _Method = HttpMethod.GET, bool _filter = false)
        {
            foreach (var u in _urls)
            {
                Filters.Add(new FilterData(u.Key, u.Value, _Method));
            }
            filter = _filter;
        }
        public MyRequestHandler(FilterData Filter, bool _filter = false)
        {
            Filters.Add(Filter);
            filter = _filter;
        }
        public MyRequestHandler(List<FilterData> _Filters, bool _filter = false)
        {
            Filters.AddRange(_Filters);
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
        public List<FilterData> Filters = new List<FilterData>();
        MyResourceRequestHandler handler;


        public void DisposeFilters()
        {
            foreach (var u in Filters)
            {
                u.DisposeFilter();
            }
        }


        public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            return false;
        }

        public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            var fd = Filters.FirstOrDefault(u => request.Url.StartsWith(u.Url) && (u.Method == HttpMethod.None || $"{u.Method}".ToLower() == request.Method.ToLower()));
            if (fd != null)
            {
                handler = new MyResourceRequestHandler(fd, filter);

                return handler;
            }
            return null;

        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            return false;
        }

        public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            return false;
        }

        public void OnDocumentAvailableInMainFrame(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            return false;
        }
    }
}
