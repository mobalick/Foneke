using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider.Web.Html.Models;

namespace FoNeke.Web.App_Start
{
    public static class SiteMapExtentions
    {
        public static bool IsParentOfActive(this SiteMapNodeModel node)
        {
            return GetActive(node).IsCurrentNode;
        }

        private static SiteMapNodeModel GetActive(this SiteMapNodeModel node)
        {
            if (node.Children.Any(c => c.IsCurrentNode))
            {
                node = node.Children.First(c => c.IsCurrentNode);
            }
            else
            {
                foreach (var child in node.Children)
                {
                    node = GetActive(child);
                }
            }

            return node;
        }
    }
}