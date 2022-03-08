using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pystach_io.Pages.Shared
{
    public class TabNavPages
    {
        public static string Home => "Home";
        public static string Infos => "Infos";
        public static string Register => "Register";

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);
        public static string InfosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Infos);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["Active"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
