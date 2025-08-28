using mango.webPortal.Models;
using mango.webPortal.services;
using mango.webPortal.services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mango.webPortal.Controllers
{
    public class CoupanController : Controller
    {
        private readonly ICoupanService _coupanService;
        public CoupanController(ICoupanService coupanService)
        {
            _coupanService = coupanService;
        }
        [Authorize]
        public async Task<IActionResult> CoupanIndex()
        {
            List<coupan> list = new();
            responceDto? data = await _coupanService.getAllCoupanAsync();
            if (data!=null && data.isSuceed == true)
            {
                list = JsonConvert.DeserializeObject<List<coupan>>(Convert.ToString(data.result));
            }
            return View(list);
        }
        public async Task<IActionResult> CoupanCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CoupanCreate(coupan data)
        {
            if (ModelState.IsValid)
            {
               responceDto? output=  await _coupanService.createCoupanAsync(data);
                if (output!=null && output.isSuceed)
                {
                    //toastr notification
                    TempData["success"] = "Coupan Created successfully!";
                    //TempData["ToastrType"] = "success";
                    return RedirectToAction(nameof(CoupanIndex));
                }
                else
                {
                    TempData["error"] = "Coupan Not Created!";
                }
            }
            return View(data);
        }
        public async Task<IActionResult> CoupanDelete(int coupanId)
        {
            responceDto? data =await _coupanService.getCoupanByIdAsync(coupanId);
            if (data!=null && data.isSuceed)
            {
                coupan resultData = JsonConvert.DeserializeObject<coupan>(Convert.ToString(data.result));
                return View(resultData);
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> CoupanDelete(coupan coupanData)
        {
            responceDto data =await _coupanService.deleteCoupanAsync(coupanData.coupanId);
            if (data != null && data.isSuceed)
            {
                TempData["success"] = "Coupan Deleted successfully";
                return RedirectToAction(nameof(CoupanIndex));
            }
            else
            {
                TempData["error"] = "Coupan not deleted! there is an error";
            }
            return View(coupanData);

        }
    }
}
