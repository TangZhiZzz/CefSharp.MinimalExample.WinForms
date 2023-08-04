using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms.Models
{
    public class DouyinModel
    {
        public class Author
        {
           
            /// <summary>
            /// 半甜梦.
            /// </summary>
            public string nickname { get; set; }
           
        }

     
        public class Play_addr
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> url_list { get; set; }
           
        }


        public class Video
        {
            /// <summary>
            /// 
            /// </summary>
            public Play_addr play_addr { get; set; }
           
        }



        public class ImagesItem
        {
           
            /// <summary>
            /// 
            /// </summary>
            public List<string> url_list { get; set; }
         
        }
       

        public class Aweme_listItem
        {
          
            /// <summary>
            /// #一张褪色的照片 #回不去的时光
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Author author { get; set; }
           
            /// <summary>
            /// 
            /// </summary>
            public Video video { get; set; }
            
            /// <summary>
            /// 
            /// </summary>
            public int media_type { get; set; }
          
            /// <summary>
            /// 
            /// </summary>
            public List<ImagesItem> images { get; set; }
          

            internal WorkOutput ToDouyinOutput()
            {
                WorkOutput workOutput = new WorkOutput();
                workOutput.NickName = this.author.nickname;
                workOutput.Description = this.desc;
                workOutput.Type = this.media_type;
                if (this.media_type == 2)
                {
                    workOutput.ImageUrlList = new List<string>();
                    foreach (var item in this.images)
                    {
                        var url = item.url_list.FirstOrDefault();
                        if (!url.Contains("http"))
                        {
                            url = "http://" + url;
                        }
                        workOutput.ImageUrlList.Add(url);
                    }
                }
                else if (this.media_type == 4)
                {
                    workOutput.VideoUrl=this.video.play_addr.url_list.FirstOrDefault();
                }
                return workOutput;
            }
        }


        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int status_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long min_cursor { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long max_cursor { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int has_more { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Aweme_listItem> aweme_list { get; set; }
          
        }

    }
}
