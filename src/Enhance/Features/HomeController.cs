using Enhance.Logic.Services.Interfaces;
using Phoenix;
using Phoenix.ActionResults;

namespace Enhance.Features
{
    public class HomeController : Controller
    {
        readonly IScannerService scannerService;

        public HomeController(IScannerService scannerService)
        {
            this.scannerService = scannerService;
        }

        public ActionResult Index()
        {
            return Page(new IndexViewModel());
        }

        public ActionResult ScanDocument()
        {
            return Page(new ScanDocumentViewModel(scannerService));
        }
    }
}