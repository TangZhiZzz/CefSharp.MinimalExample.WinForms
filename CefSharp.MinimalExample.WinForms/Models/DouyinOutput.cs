using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Models
{
    public class DouyinOutput
    {
        public string NickName { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string VideoUrl { get; set; }
        public List<string> ImageUrlList { get; set; }

        public string DownloadUrl
        {
            get
            {
                if (Type == 2)
                {
                    return string.Join(",", ImageUrlList);
                }
                else if (Type == 4)
                {
                    return VideoUrl;
                }
                return "";
            }
        }
        public string Status { get; set; }
    }
}
