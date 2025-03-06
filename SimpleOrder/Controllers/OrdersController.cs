using Microsoft.AspNetCore.Mvc;
using SimpleOrder.Exceptions;
using SimpleOrder.Models;
using SimpleOrder.Models.NewFolder;
using SimpleOrder.Services;

namespace SimpleOrder.Controllers
{
    public class OrdersController : Controller
    {
        public SimpleOrderContext Context { get; }

        public OrderService OrderService { get; }

        public OrdersController(SimpleOrderContext context, OrderService orderService)
        {
            Context = context;
            OrderService = orderService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await OrderService.ReadAll();

            if (orders == default)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDisplayViewModel order = null;

            try
            {
                order = await OrderService.Read(id.Value);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await OrderService.Create(order);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(order);
        }

        //GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDisplayViewModel order = null;

            try
            {
                order = await OrderService.Read(id.Value);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            OrderEditViewModel userEditModel = MapToEditViewModel(order);

            return View(userEditModel);
        }

        //POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderEditViewModel orderNewData)
        {
            if (id != orderNewData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await OrderService.Update(id, orderNewData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(orderNewData);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDisplayViewModel order = null;

            try
            {
                order = await OrderService.Read(id.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await OrderService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(id);
        }

        private static OrderEditViewModel MapToEditViewModel(OrderDisplayViewModel displayViewModel)
        {
            return new OrderEditViewModel()
            {
                Id = displayViewModel.Id,
                RecipientAddress = displayViewModel.RecipientAddress,
                RecipientCity = displayViewModel.RecipientCity,
                SenderAddress = displayViewModel.SenderAddress,
                SenderCity = displayViewModel.SenderCity,
                Weight = displayViewModel.Weight,
                ShipmentDate = displayViewModel.ShipmentDate
            };
        }
    }
}
