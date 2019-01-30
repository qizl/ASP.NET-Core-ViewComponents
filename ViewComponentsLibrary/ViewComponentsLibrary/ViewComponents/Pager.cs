using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnjoyCodes.ViewComponentsLibrary.ViewComponents
{
    public class Pager : ViewComponent
    {
        public IViewComponentResult Invoke(int pageSize, int currentPage, int totalItemCount, object urlParameters)
        {
            var paras = null as Dictionary<string, string>;
            try
            {
                paras = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(urlParameters));
            }
            catch
            { }

            var links = new List<Link>();

            var pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            // Previous
            links.Add(currentPage > 1 ?
                new Link { Active = true, DisplayText = "上页", PageIndex = currentPage - 1, UrlParameters = this.generateUrlParameters(currentPage - 1, paras) } :
                new Link { Active = false, DisplayText = "上页" });

            var start = 1;
            var end = pageCount;
            var nrOfPagesToDisplay = 5;

            if (pageCount > nrOfPagesToDisplay)
            {
                var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                var below = (currentPage - middle);
                var above = (currentPage + middle);

                if (below < 2)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 2))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay + 1);
                }

                start = below;
                end = above;
            }

            if (start > 1)
            {
                links.Add(new Link { Active = true, PageIndex = 1, DisplayText = "1", UrlParameters = this.generateUrlParameters(1, paras) });
                if (start > 3)
                    links.Add(new Link { Active = true, PageIndex = 2, DisplayText = "2", UrlParameters = this.generateUrlParameters(1, paras) });
                if (start > 2)
                    links.Add(new Link { Active = true, DisplayText = "..." });
            }

            for (var i = start; i <= end; i++)
            {
                if (i == currentPage || (currentPage <= 0 && i == 0))
                {
                    links.Add(new Link { Active = true, PageIndex = i, IsCurrent = true, DisplayText = i.ToString() });
                }
                else
                {
                    links.Add(new Link { Active = true, PageIndex = i, DisplayText = i.ToString(), UrlParameters = this.generateUrlParameters(i, paras) });
                }
            }
            if (end < pageCount)
            {
                if (end < pageCount - 1)
                {
                    links.Add(new Link { Active = true, DisplayText = "..." });
                }
                if (pageCount - 2 > end)
                {
                    links.Add(new Link { Active = true, PageIndex = pageCount - 1, DisplayText = (pageCount - 1).ToString(), UrlParameters = this.generateUrlParameters(pageCount - 1, paras) });
                }

                links.Add(new Link { Active = true, PageIndex = pageCount, DisplayText = pageCount.ToString(), UrlParameters = this.generateUrlParameters(pageCount, paras) });
            }

            // Next
            links.Add(currentPage < pageCount ?
                new Link { Active = true, PageIndex = currentPage + 1, DisplayText = "下页", UrlParameters = this.generateUrlParameters(currentPage + 1, paras) } :
                new Link { Active = false, DisplayText = "下页" });

            return View(links);
        }

        private string generateUrlParameters(int pageNumber, Dictionary<string, string> paras)
        {
            var paramUrl = new StringBuilder($"?page={pageNumber}");
            if (paras?.Count > 0)
            {
                paramUrl.Append("&");
                paramUrl.Append(string.Join("&", paras.Select(m => $"{m.Key}={m.Value}")));
            }
            return paramUrl.ToString();
        }

        /// <summary>
        /// 分页链接对象
        /// </summary>
        public class Link
        {
            public bool Active { get; set; }

            public bool IsCurrent { get; set; }

            public int? PageIndex { get; set; }

            public string DisplayText { get; set; }

            public string UrlParameters { get; set; }
        }
    }
}
