using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Core.Models;
using WebApplication6.DAL;
using WebApplication6.Models;
using WebApplication6.Repositories.Interfaces;
using WebApplication6.Services.IImplementations;
using WebApplication6.Services.Interfaces;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService bookService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public BasketController(IBookRepository bookRepository,IBookService bookService,UserManager<AppUser> userManager,AppDbContext context)
        {
            _bookRepository = bookRepository;
            this.bookService = bookService;
            this._userManager = userManager;
            this._context = context;
        }
        public BasketController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            Book book = await bookService.GetByIdAsync(id);
            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel()
            {
                Book = book,
                RelatedBooks = await bookService.GetAllRelatedBooksAsync(book)
            };

            return View(productDetailViewModel);
        }


        public async Task<IActionResult> GetBookModal(int id)
        {
            var book = await bookService.GetByIdAsync(id);

            return PartialView("_BookModalPartial", book);
        }


        public async Task<IActionResult> AddToBasket(int id)
        {
            if(!_bookRepository.Table.Any(x=>x.Id==id )) return NotFound();
            List<BasketViewModel> basketViewModels = new List<BasketViewModel>();
            BasketViewModel basketViewModel = null;
            BasketItem userbasketItem=null;
            AppUser user = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (user == null)
            {
                string basketItemStr = HttpContext.Request.Cookies["BasketItems"];
                if (basketItemStr is not null)
                {

                    basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketItemStr);
                    basketViewModel = basketViewModels.FirstOrDefault(x => x.BookId == id);
                    if (basketViewModel is not null)
                    {
                        basketViewModel.Count++;
                    }
                    else
                    {
                        basketViewModel = new BasketViewModel
                        {
                            BookId = id,
                            Count = 1
                        };

                        basketViewModels.Add(basketViewModel);

                    }

                }
                else
                {
                    basketViewModel = new BasketViewModel
                    {
                        BookId = id,
                        Count = 1
                    };

                    basketViewModels.Add(basketViewModel);
                }



                basketItemStr = JsonConvert.SerializeObject(basketViewModels);

                HttpContext.Response.Cookies.Append("BasketItems", basketItemStr);
            }
            else
            {
                userbasketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.BookId == id && x.AppUserId == user.Id);
                if (userbasketItem is not null)
                {
                    userbasketItem.Count++;
                }
                else
                {
                    userbasketItem = new BasketItem
                    {
                        BookId = id,
                        Count = 1,
                        AppUserId = user.Id,
                        IsDeleted = false
                    };
                    _context.BasketItems.Add(userbasketItem);
                }
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        public IActionResult GetBasketItems()
        {
            List<BasketViewModel> basketItemList = new List<BasketViewModel>();

            string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];

            if (basketItemListStr != null)
            {
                basketItemList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketItemListStr);
            }

            return Json(basketItemList);
        }

        public async Task<IActionResult> Checkout()
        {
            List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
            List<BasketViewModel> basketItemList = new List<BasketViewModel>();
            CheckoutViewModel checkoutItem = null;
            List<BasketItem> userBasketItem = new List<BasketItem>();
            AppUser user = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            if (user == null)
            {
                string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];
                if (basketItemListStr != null)
                {
                    basketItemList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketItemListStr);

                    foreach (var item in basketItemList)
                    {
                        checkoutItem = new CheckoutViewModel
                        {
                            Book = await _bookRepository.GetByIdAsync(x => x.Id == item.BookId),
                            Count = item.Count
                        };
                        checkoutItemList.Add(checkoutItem);
                    }
                }
            }
            else
            {
                userBasketItem = await _context.BasketItems.Include(x => x.Book).Where(x => x.AppUserId == user.Id).ToListAsync();
                foreach (var item in userBasketItem)
                {
                    checkoutItem = new CheckoutViewModel
                    {
                        Book = item.Book,
                        Count = item.Count
                    };
                    checkoutItemList.Add(checkoutItem);
                }
            }
            OrderViewModel orderViewModel = new OrderViewModel
            {
                CheckoutViewModels = checkoutItemList,
                FullName = user?.FullName,
            };



            return View(orderViewModel);
        }
            [HttpPost]
            public async Task<IActionResult> Checkout(OrderViewModel orderViewModel)
            {
                List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
                List<BasketViewModel> basketItemList = new List<BasketViewModel>();
                List<BasketItem> userBasketItem = new List<BasketItem>();
                CheckoutViewModel checkoutItem = null;
                AppUser user = null;
                OrderItem orderItem = null;
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                }
                Order order = new Order
                {
                    FullName = orderViewModel.FullName,
                    Country = orderViewModel.Country,
                    Email = orderViewModel.Email,
                    Address = orderViewModel.Address,
                    Phone = orderViewModel.Phone,
                    ZipCode = orderViewModel.ZipCode,
                    Note = orderViewModel.Note,
                    OrderItems = new List<OrderItem>(),
                    AppUserId = user?.Id,
                    CreatedDate = DateTime.UtcNow.AddHours(4)
                };
                if (user == null)
                {
                    string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];
                    if (basketItemListStr != null)
                    {
                        basketItemList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketItemListStr);

                        foreach (var item in basketItemList)
                        {
                            Book book = _context.Books.FirstOrDefault(x => x.Id == item.BookId);
                            orderItem = new OrderItem
                            {
                                Book = book,
                                BookName = book.Name,
                                CostPrice = book.CostPrice,
                                DiscountPercent = book.DisPrice,
                                SalePrice = book.SalePrice * ((100 - book.DisPrice) / 100),
                                Count = item.Count,
                                Order = order


                            };
                            order.TotalPrice = orderItem.SalePrice * orderItem.Count;
                            order.OrderItems.Add(orderItem);
                        }
                    }
                }
                else
                {
                    userBasketItem = await _context.BasketItems.Include(x => x.Book).Where(x => x.AppUserId == user.Id).ToListAsync();
                    foreach (var item in userBasketItem)
                    {
                        Book book = _context.Books.FirstOrDefault(x => x.Id == item.BookId);
                        orderItem = new OrderItem
                        {
                            Book = book,
                            BookName = book.Name,
                            CostPrice = book.CostPrice,
                            DiscountPercent = book.DisPrice,
                            SalePrice = book.SalePrice * ((100 - book.DisPrice) / 100),
                            Count = item.Count,
                            Order = order


                        };
                        order.TotalPrice = orderItem.SalePrice * orderItem.Count;
                        order.OrderItems.Add(orderItem);
                        item.IsDeleted = true;
                    }
                }
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
        public async Task<IActionResult> SearchBooks(string value)
        {
            List<Book> searchedBooks = await bookService.GetAllAsync(x => x.Name.ToLower().Contains(value.Trim().ToLower()));

            return Ok(searchedBooks);
        }
    }


}