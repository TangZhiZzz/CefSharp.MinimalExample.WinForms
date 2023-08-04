using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CefSharp.MinimalExample.WinForms.Models.DouyinModel;

namespace CefSharp.MinimalExample.WinForms.Models
{
    public class KuaiShouModel
    {
        //如果好用，请收藏地址，帮忙分享。
        public class Author
        {

            /// <summary>
            /// 小A学财经
            /// </summary>
            public string name { get; set; }

        }


        public class Photo
        {

            /// <summary>
            /// 
            /// </summary>
            public string caption { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string photoUrl { get; set; }


        }



        public class FeedsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Author author { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Photo photo { get; set; }


            internal WorkOutput ToDouyinOutput()
            {
                WorkOutput workOutput = new WorkOutput();
                workOutput.NickName = this.author.name;
                workOutput.Description = this.photo.caption;
                workOutput.Type = this.type;
                workOutput.VideoUrl = this.photo.photoUrl;
                return workOutput;
            }

        }

        public class VisionProfilePhotoList
        {

            /// <summary>
            /// 
            /// </summary>
            public List<FeedsItem> feeds { get; set; }

        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public VisionProfilePhotoList visionProfilePhotoList { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
        }

    }
}
