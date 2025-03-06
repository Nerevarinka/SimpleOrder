using Microsoft.AspNetCore.Mvc;
using SimpleOrder.Models;
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

            var order = await OrderService.Read(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await OrderService.Create(order);

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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            OrderEditViewModel userEditModel = MapToEditViewModel(order);

            return View(userEditModel);
        }

        //POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                OrderDisplayViewModel entity = await OrderService.Read(id);
                await OrderService.Delete(entity.Id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private OrderEditViewModel MapToEditViewModel(OrderDisplayViewModel displayViewModel)
        {
            return new OrderEditViewModel()
            {
                Id = displayViewModel.Id,
                Number = displayViewModel.Number,
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
