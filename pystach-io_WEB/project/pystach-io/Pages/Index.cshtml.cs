using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pystach_io.Models;
using pystach_io.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace pystach_io.Pages
{
    [AutoValidateAntiforgeryToken]
    public class IndexModel : PageModel
    {

        /// <summary>
        /// GET /Index
        /// </summary>
        /// <param name="error">Type of the error</param>
        public void OnGet(string error = "")
        {
            //Get the error
            ViewData["error"] = error;

            //If it had an error, send different infos to the page
            if (error != "")
            {
                ViewData["len"] = TempData["len"] == null ? 0 : TempData["len"];
                ViewData["addresses"] = TempData["addresses"];
                ViewData["errorContent"] = TempData["adressError"];
                TempData.Remove("len");
                TempData.Remove("addresses");
                TempData.Remove("adressError");
            } else
            {
                ViewData["len"] = 1;
                ViewData["url"] = TempData["gmapUrl"];
                TempData.Remove("gmapUrl");
            }
        }

        /// <summary>
        /// POST /Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            //Get the inputs
            var form = await HttpContext.Request.ReadFormAsync();

            //Get the number of input addresses
            var number = form.Count - 1;

            //If one input addresses, send back an error
            if (number == 1)
            {
                return Redirect("./?error=address");
            }

            //Local variables
            var addressesList = new List<string>();
            int i = 0;

            //Get the input addresses
            while (i < number)
            {
                var address = form["address_" + i];

                addressesList.Add(address);

                i++;
            }

            //Use the MaPyto service
            var result = PystachioAPI.useIt(addressesList);

            //If an error in the result, redirect
            if (result.Contains("Error"))
            {
                var jsonObj = JsonSerializer.Deserialize<List<string>>(result);

                TempData["adressError"] = jsonObj[1].Substring(13);

                jsonObj.Remove("Error");
                TempData["len"] = jsonObj.Count();

                jsonObj.RemoveAt(0);
                TempData["addresses"] = jsonObj;

                return Redirect("./?error=pystach");

            } else
            {
                var jsonObj = JsonSerializer.Deserialize<jsonModel>(result);

                TempData["gmapUrl"] = jsonObj.gmapURL;
            }

            return Redirect("./");
        }
    }
}
