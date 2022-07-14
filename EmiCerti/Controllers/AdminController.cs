using EmiCerti.Data;
using Hashgraph;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmiCerti.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApplicationDbContext _db;
        private IConfiguration _configuration;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var projects = _db.Projects.OrderByDescending(x => x.CreatedAt);
            return View(projects);
        }

        public IActionResult ApproveSendHBAR(int id)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Id == id);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveSendHBARPost(int id)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Id == id);
            var gateway = new Gateway(_configuration["HederaGatewayUrl"], 0, 0, int.Parse(_configuration["HederaNodeNumber"]));
            var payer = new Address(0, 0, long.Parse(_configuration["HederaAccountId"]));
            var account = new Address(0, 0, project.OwnerAccountNumber.GetValueOrDefault());
            var payerSignatory = new Signatory(Hex.ToBytes(_configuration["HederaPrivateKey"]));
            //var accountSignatory = new Signatory(Hex.ToBytes(Form.PrivateKey));
            try
            {
                await using var client = new Client(ctx =>
                {
                    ctx.Gateway = gateway;
                    ctx.Payer = payer;
                    ctx.Signatory = payerSignatory;
                });
                var transactionsTokens = _db.Transactions.Where(x => x.ProjectId == id).Sum(x => x.Quantity).GetValueOrDefault();

                var sendHBAR = await client.TransferAsync(payer, account, transactionsTokens * 100_000_000);
                if (sendHBAR.Status == ResponseCode.Success)
                {
                    project.HBARsent = true;
                    _db.SaveChanges();
                    ViewBag.Status = "HBAR sent to account " + project.OwnerAccountNumber.GetValueOrDefault();
                }
                else
                {
                    
                    ViewBag.Status = "Error for sendging HBAR";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Status = "Error for sendging HBAR";
            }
            

            return View(project);
        }
    }
}
