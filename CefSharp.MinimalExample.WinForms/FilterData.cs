using CefSharp.MinimalExample.WinForms.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms
{
    public enum HttpMethod
    {
        None,
        GET,
        POST,
        CONNECT,
        OPTIONS,
    }
    public class FilterData : IDisposable
    {
        public FilterData(string name, string url) { Name = name; Url = url; }
        public FilterData(string name, string url, HttpMethod _Method) { Name = name; Url = url; Method = _Method; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Name { get; set; }
        public string Url { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.GET;

        public List<FilterRequestResponseData> Filters = new List<FilterRequestResponseData>();


        internal void AddFilter(IRequest request, MyResponseFilter fil)
        {
            Filters.Add(new FilterRequestResponseData(request.Identifier, Encoding.UTF8.GetString(request.PostData.Elements[0].Bytes), fil));
        }
        public void DisposeFilter()
        {
            Filters.Clear();
        }

        bool Disposed = false;
        public void Dispose()
        {
            if (Disposed)
                return;
            Filters.Clear();
            Disposed = true;
        }


    }
    public class FilterRequestResponseData
    {

        public FilterRequestResponseData(UInt64 Identifier, string PostDataStr, MyResponseFilter Filter)
        {
            this.Identifier = Identifier;
            this.PostDataStr = PostDataStr;
            this.Filter = Filter;
        }
        public UInt64 Identifier { get; set; }
        public IPostData PostData { get; set; }

        public string PostDataStr { get; set; }

        public MyResponseFilter Filter { get; set; }
    }
}
