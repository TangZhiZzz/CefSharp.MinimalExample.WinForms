using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Filter
{
    public class MyResponseFilter : IResponseFilter
    {
        public MyResponseFilter(bool _filter) { filter = _filter; }

        bool Disposed = false;
        /// <summary>
        /// 是否过滤，如果不过滤就截获
        /// </summary>
        bool filter = false;
        private MemoryStream memoryStream;
        public void Dispose()
        {
            //memoryStream?.Dispose();
            //memoryStream = null;
        }
        public void Dispose(bool r)
        {
            if (Disposed)
                return;
            if (r)
            {
                memoryStream?.Dispose();
                memoryStream = null;
            }
            Disposed = true;
        }

        public FilterStatus Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            if (dataIn == null)
            {
                dataInRead = 0;
                dataOutWritten = 0;

                return FilterStatus.Done;
            }

            dataInRead = dataIn.Length;
            dataOutWritten = Math.Min(dataInRead, dataOut.Length);

            //Important we copy dataIn to dataOut
            dataIn.CopyTo(dataOut);

            //Copy data to stream
            dataIn.Position = 0;
            dataIn.CopyTo(memoryStream);

            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            memoryStream = new MemoryStream();

            return true;
        }
        public byte[] Data
        {
            get { return memoryStream.ToArray(); }
        }

        public string DataStr
        {
            get { return Encoding.UTF8.GetString(memoryStream.ToArray()); }
        }
    }
}
