// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.DevTools.IO;
using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.MinimalExample.WinForms.Handler;
using CefSharp.MinimalExample.WinForms.Models;
using CefSharp.Web;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
#if DEBUG
        private const string Build = "Debug";
#else
        private const string Build = "Release";
#endif
        private readonly string title = "CefSharp.MinimalExample.WinForms (" + Build + ")";
        private readonly ChromiumWebBrowser browser;

        MyRequestHandler MyRequestHandler;
        Dictionary<string, string> FilterUrls = new Dictionary<string, string>()
        {
            {"douyinapi","https://www.douyin.com/aweme/v1/web/aweme/post" },
            {"kuaishouapi","https://www.kuaishou.com/graphql" },
        };
        public BrowserForm()
        {
            InitializeComponent();

            Text = title;
            WindowState = FormWindowState.Maximized;

            //browser = new ChromiumWebBrowser("https://www.douyin.com/user/MS4wLjABAAAAd4IEE9JOezbMuKOhRFAEAwlN3D5qgBDvTjjqV2g5FHM?is_search=0&list_name=follow&nt=0");
            browser = new ChromiumWebBrowser("https://www.kuaishou.com/profile/3xr8n69a72wdrtq");
            toolStripContainer.ContentPanel.Controls.Add(browser);

            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
            browser.LoadError += OnBrowserLoadError;
            browser.LifeSpanHandler = new LifeSpanHandler();


            #region  自定义捕获或过滤
            //new有很多重载方法，由于我这次用默认全是GET，所以没有定义请求类型
            MyRequestHandler = new MyRequestHandler(FilterUrls,HttpMethod.None);
            browser.RequestHandler = MyRequestHandler;
            #endregion

            var version = string.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}",
               Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);

#if NETCOREAPP
            // .NET Core
            var environment = string.Format("Environment: {0}, Runtime: {1}",
                System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant(),
                System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
#else
            // .NET Framework
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var environment = String.Format("Environment: {0}", bitness);
#endif

            DisplayOutput(string.Format("{0}, {1}", version, environment));
        }




        private void OnBrowserLoadError(object sender, LoadErrorEventArgs e)
        {
            //Actions that trigger a download will raise an aborted error.
            //Aborted is generally safe to ignore
            if (e.ErrorCode == CefErrorCode.Aborted)
            {
                return;
            }

            var errorHtml = string.Format("<html><body><h2>Failed to load URL {0} with error {1} ({2}).</h2></body></html>",
                                              e.FailedUrl, e.ErrorText, e.ErrorCode);

            _ = e.Browser.SetMainFrameDocumentContentAsync(errorHtml);

            //AddressChanged isn't called for failed Urls so we need to manually update the Url TextBox
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = e.FailedUrl);
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = title + " - " + args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            MyRequestHandler.DisposeFilters();
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void DisplayOutput(string output)
        {
            this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }

        private void LoadUrl(string urlString)
        {
            // No action unless the user types in some sort of url
            if (string.IsNullOrEmpty(urlString))
            {
                return;
            }

            Uri url;

            var success = Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out url);

            // Basic parsing was a success, now we need to perform additional checks
            if (success)
            {
                // Load absolute urls directly.
                // You may wish to validate the scheme is http/https
                // e.g. url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps
                if (url.IsAbsoluteUri)
                {
                    browser.LoadUrl(urlString);

                    return;
                }

                // Relative Url
                // We'll do some additional checks to see if we can load the Url
                // or if we pass the url off to the search engine
                var hostNameType = Uri.CheckHostName(urlString);

                if (hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6)
                {
                    browser.LoadUrl(urlString);

                    return;
                }

                if (hostNameType == UriHostNameType.Dns)
                {
                    try
                    {
                        var hostEntry = Dns.GetHostEntry(urlString);
                        if (hostEntry.AddressList.Length > 0)
                        {
                            browser.LoadUrl(urlString);

                            return;
                        }
                    }
                    catch (Exception)
                    {
                        // Failed to resolve the host
                    }
                }
            }

            // Failed parsing load urlString is a search engine
            var searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(urlString);

            browser.LoadUrl(searchUrl);
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        private void ConsoleDouyinApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var douyinOutputList = new List<WorkOutput>();
            var str = MyRequestHandler.ToString();
            //提取某一个url拦截的数据
            var fd = MyRequestHandler.Filters.FindAll(f => f.Name == "douyinapi" || f.Name == "douyinuser");
            FilterData filterData = fd.FirstOrDefault(f => f.Name == "douyinapi");
            if (filterData != null)
            {
                foreach (var item in filterData.Filters)
                {
                    Console.WriteLine(item.Filter.DataStr.ToString());
                    var data = JsonSerializer.Deserialize<DouyinModel.Root>(item.Filter.DataStr);
                    var douyinoutput = data.aweme_list.Select(m => m.ToDouyinOutput());
                    douyinOutputList.AddRange(douyinoutput);
                }
            }
            if (douyinOutputList.Count > 0)
            {
                if (MessageBox.Show(string.Format("共拦截{0}作品，是否跳转至下载？", douyinOutputList.Count)) == DialogResult.OK)
                {
                    DownloaderForm downloaderForm = new DownloaderForm();
                    downloaderForm.douyinOutputs = douyinOutputList;
                    downloaderForm.Show();

                }
            }
        }

        private void AddDouyinApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Dictionary<string, string> FilterUrls = new Dictionary<string, string>()
            //{
            //    {"douyinapi","https://www.douyin.com/aweme/v1/web/aweme/post" },
            //};
            //#region  自定义捕获或过滤
            ////new有很多重载方法，由于我这次用默认全是GET，所以没有定义请求类型
            //MyRequestHandler = new MyRequestHandler(FilterUrls);
            //browser.RequestHandler = MyRequestHandler;
            //#endregion
        }

        private void AddKuaiShouApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Dictionary<string, string> FilterUrls = new Dictionary<string, string>()
            //{
            //    {"kuaishouapi","https://www.kuaishou.com/graphql" },
            //};
            //#region  自定义捕获或过滤
            ////new有很多重载方法，由于我这次用默认全是GET，所以没有定义请求类型
            //MyRequestHandler = new MyRequestHandler(FilterUrls,HttpMethod.POST);
            //browser.RequestHandler = MyRequestHandler;
            //#endregion
        }

        private void ConsoleKuaiShouApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var kuaishouOutputList = new List<WorkOutput>();
            var str = MyRequestHandler.ToString();
            //提取某一个url拦截的数据
            var fd = MyRequestHandler.Filters.FindAll(f => f.Name == "kuaishouapi");
            FilterData filterData = fd.FirstOrDefault(f => f.Name == "kuaishouapi");
            var filters = filterData.Filters.Where(f => f.PostDataStr.Contains("visionProfilePhotoList")).ToList();
            if (filterData != null)
            {
                foreach (var item in filters)
                {
                    Console.WriteLine(item.Filter.DataStr.ToString());
                    var data = JsonSerializer.Deserialize<KuaiShouModel.Root>(item.Filter.DataStr);
                    var douyinoutput = data.data.visionProfilePhotoList.feeds.Select(m => m.ToDouyinOutput());
                    kuaishouOutputList.AddRange(douyinoutput);
                }
            }
            if (kuaishouOutputList.Count > 0)
            {
                if (MessageBox.Show(string.Format("共拦截{0}作品，是否跳转至下载？", kuaishouOutputList.Count)) == DialogResult.OK)
                {
                    DownloaderForm downloaderForm = new DownloaderForm();
                    downloaderForm.douyinOutputs = kuaishouOutputList;
                    downloaderForm.Show();

                }
            }
        }
    }
}
