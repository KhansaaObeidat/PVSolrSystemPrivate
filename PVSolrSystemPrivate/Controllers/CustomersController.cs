using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVSolrSystemPrivate.Data;
using PVSolrSystemPrivate.Models;
using PVSolrSystemPrivate.Models.ViewModels;

namespace PVSolrSystemPrivate.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(SearchViewModel model, int page = 1, int pageSize = 10)
        {
            var customers = from c in _context.customers
                            select c;

            if (!string.IsNullOrEmpty(model.SearchString))
            {
                customers = customers.Where(c => c.CustomerName.Contains(model.SearchString) || c.PhoneNumber.Contains(model.SearchString));
            }

            var totalCustomers = await customers.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize);
            var pagedCustomers = await customers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(pagedCustomers.Select(c => new
                {
                    c.CustomerId,
                    c.CustomerName,
                    c.PhoneNumber,
                    c.SubscriptionNumber,
                    c.Phase,
                    c.Location,
                    c.Notes
                }).ToList());
            }

            model.Customers = pagedCustomers;
            return View(model);
        }
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private bool CustomerExists(int id)
        {
            return _context.customers.Any(e => e.CustomerId == id);
        }

        public IActionResult Profile(int id)
        {
            var customer = _context.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            // يمكنك إضافة أي بيانات إضافية تحتاجها للعرض هنا
            return View(customer); // عرض الصفحة الشخصية للعميل
        }

        public IActionResult CalculateConsumption(int id, decimal consumptionInput)
        {
            var customer = _context.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Consumption = consumptionInput; // حفظ الاستهلاك
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UploadDocuments(IFormFile[] customerFiles, int customerId)
        {
            if (customerFiles == null || customerFiles.Length == 0)
            {
                return BadRequest(new { message = "لا توجد ملفات للرفع." });
            }

            try
            {
                foreach (var file in customerFiles)
                {
                    if (file.Length > 0)
                    {
                        var uploadsFolder = Path.Combine("wwwroot/uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var filePath = Path.Combine(uploadsFolder, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var customerFile = new CustomerFile
                        {
                            CustomerId = customerId,
                            FileName = file.FileName,
                            FilePath = filePath
                        };

                        _context.CustomerFiles.Add(customerFile);
                    }
                }

                await _context.SaveChangesAsync();
                return Json(new { message = "تم رفع الملفات بنجاح." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ أثناء رفع الملفات." });
            }
        }

        [HttpGet]
        public IActionResult GetCustomerFiles(int customerId)
        {
            var files = _context.CustomerFiles
                                .Where(f => f.CustomerId == customerId)
                                .Select(f => new { f.Id, f.FileName })
                                .ToList();

            return Json(files);
        }

        public IActionResult DownloadFile(int fileId)
        {
            try
            {
                var file = _context.CustomerFiles.Find(fileId);
                if (file == null)
                {
                    return NotFound(new { message = "الملف غير موجود." });
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { message = "الملف غير موجود على الخادم." });
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", file.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ أثناء تنزيل الملف." });
            }
        }

        public IActionResult DeleteFile(int fileId)
        {
            try
            {
                var file = _context.CustomerFiles.Find(fileId);
                if (file == null)
                {
                    return NotFound(new { message = "الملف غير موجود." });
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.CustomerFiles.Remove(file);
                _context.SaveChanges();

                return Ok(new { message = "تم حذف الملف بنجاح." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ أثناء حذف الملف." });
            }
        }

    }
}
