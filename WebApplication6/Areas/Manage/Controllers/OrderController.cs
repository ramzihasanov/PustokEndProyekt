using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Helpers;
using Pustok.Business.Hubs;
using Pustok.Core.Models;
using WebApplication6.DAL;

namespace WebApplication6.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<AppUser> userManager;

        public OrderController(AppDbContext context,IHubContext<ChatHub> hubContext,UserManager<AppUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page =1)
        {
            var query = _context.Orders.AsQueryable();

            PaginatedList<Order> paginatedOrder = PaginatedList<Order>.Create(query, page, 1);

            return View(paginatedOrder);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Order order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null) return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Accept(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order is null) return NotFound();
            order.OrderStatus = Pustok.Core.Enums.OrderStatus.Accepted;

            await _context.SaveChangesAsync();
            if (order.AppUserId!=null) { 
            var user = await userManager.FindByIdAsync(order.AppUserId);

            if (User != null) {
                _hubContext.Clients.Client(user.ConnectionId).SendAsync("OrderAccepted");
            } }
          

            return RedirectToAction("index", "order");
        }
        [HttpPost]
        public async Task<IActionResult> Reject(int id, string AdminComment)
        {
            Order order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null) return NotFound();
            if (AdminComment == null)
            {
                ModelState.AddModelError("AdminComment", "Must be written!");
                return View("detail", order);
            }
            order.OrderStatus = Pustok.Core.Enums.OrderStatus.Rejected;
            order.AdminComment = AdminComment;
            await _context.SaveChangesAsync();
            if (order.AppUserId != null)
            {
                var user = await userManager.FindByIdAsync(order.AppUserId);

                if (User != null)
                {
                    _hubContext.Clients.Client(user.ConnectionId).SendAsync("OrderRejected",AdminComment);
                }
            }
            return  RedirectToAction("index", "order");
        }
    }
}
