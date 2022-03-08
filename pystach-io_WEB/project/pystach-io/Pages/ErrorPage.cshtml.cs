using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pystach_io.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorPageModel : PageModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorStatusCode { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// GET /ErrorPage?code=XXX
        /// </summary>
        /// <param name="code">ErrorStatusCode</param>
        public void OnGet(int code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            if (ErrorStatusCode == 404)
            {
                ErrorMessage = "Have a good travel to the <span style='font-weight: bold; text-decoration: underline;'>404</span> zone...";
            }
            else if (ErrorStatusCode == 500 | ErrorStatusCode == 502)
            {
                ErrorMessage = "Well... there is some <span style='font-weight: bold; text-decoration: underline;'>" + ErrorStatusCode + "</span> happening here...";
            }
            else
            {
                ErrorMessage = "We have an error <span style='font-weight: bold; text-decoration: underline;'>" + ErrorStatusCode + "</span> here...";
            }
        }
    }
}
