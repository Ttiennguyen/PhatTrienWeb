using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebChild.Data;

namespace WebChild.Components;

public class CategoryViewComponent : ViewComponent
{
    private readonly AppDbContext _dbContext;
    
    public CategoryViewComponent(AppDbContext dbContext)
    {
        _dbContext = dbContext;

    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var CateList = await _dbContext.Categories.ToListAsync();
        
        return View(CateList);
    }
    
}