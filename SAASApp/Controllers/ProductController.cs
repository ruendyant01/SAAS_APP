using Microsoft.AspNetCore.Mvc;
using SAASApp.Context;
using SAASApp.Data.Migrations;

namespace SAASApp.Controllers;

public class ProductController(ApplicationDbContext context, ILogger<ProductController> log) : Controller
{
    // GET
    public IActionResult Index()
    {
        List<Product> products = context.Products.ToList();
        // log.LogDebug();
        log.LogDebug(
            "Products: {0}. Product Data : {1}",
            products.Count,
            products.ToString());
        return View(products);
    }

    public IActionResult Create()
    {
        return View(new Product());
    }

    [HttpPost]
    public IActionResult Create([Bind("Name")] Product product)
    {
        if (!ModelState.IsValid) return View(product);
        context.Products.Add(product);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(long id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        return View(product);
    }

    [HttpPost]
    public IActionResult Update([Bind("Name")] Product product)
    {
        if (!ModelState.IsValid) return View(product);
        context.Products.Update(product);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(long id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteAct([Bind("Name")] Product product)
    {
        if (!ModelState.IsValid) return View(product);
        context.Products.Remove(product);
        context.SaveChanges();
        return RedirectToAction("Index");
    }
}