using CefSharp.MinimalExample.WinForms.Filter;
using System;
using System.Collections.Generic;
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
        public bool HasData { get { return Filter?.Data?.Length > 0; } }

        public Dictionary<UInt64, MyResponseFilter> Filters = new Dictionary<ulong, MyResponseFilter>();
        public UInt64 Identifier { get; set; }
        public MyResponseFilter Filter { get; set; }
        public void AddFilter(UInt64 _Identifier, MyResponseFilter _Filter)
        {
            //Filter?.Dispose(true);
            Filters.Add(_Identifier, _Filter);
            Identifier = _Identifier;
            Filter = _Filter;
        }
        public void DisposeFilter()
        {
            Filter?.Dispose(true);
            Filters.Clear();
        }

        bool Disposed = false;
        public void Dispose()
        {
            if (Disposed)
                return;
            Filter?.Dispose(true);
            Disposed = true;
        }
        public override string ToString()
        {
            return $"Name={Name},DataStr={Filter?.DataStr}";
        }
    }
}
