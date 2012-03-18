using System.Drawing;
using Enhance.Logic.Services.Interfaces;
using Enhance.Models;
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

        public ActionResult ManageDocuments()
        {
            return Page(new ManageDocumentsViewModel(null));
        }

        public ActionResult ManageDocuments(EnhanceImage image)
        {
            return Page(new ManageDocumentsViewModel(image));
        }
    }
}