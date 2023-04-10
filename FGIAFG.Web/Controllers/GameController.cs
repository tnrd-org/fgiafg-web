using Microsoft.AspNetCore.Mvc;

namespace FGIAFG.Web.Controllers
{
    // public class GameController : Controller
    // {
    //     private readonly IDealApi dealApi;
    //
    //     public GameController(IDealApi dealApi)
    //     {
    //         this.dealApi = dealApi;
    //     }
    //
    //     // GET
    //     [Route("[controller]/{hash}")]
    //     public async Task<IActionResult> Index(string hash)
    //     {
    //         Result<IDeal> dealResult = await dealApi.GetByHash(hash);
    //         return dealResult.IsSuccess
    //             ? Redirect(dealResult.Value.StoreUrl)
    //             : NotFound();
    //     }
    // }
}
