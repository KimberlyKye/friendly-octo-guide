namespace Infrastructure.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class HomeTaskRepository : IHomeTaskRepository
{
    private readonly AppDbContext _context;
    private readonly IHomeTaskFactory _homeTaskFactory;


    public HomeTaskRepository(AppDbContext context, IHomeTaskFactory homeTaskFactory)
    {
        _context = context;
        _homeTaskFactory = homeTaskFactory;
    }

    public async Task<HomeTask?> GetByIdAsync(int id)
    {
        var homeTask = await _context.HomeTasks.FirstOrDefaultAsync(h => h.Id == id);
        if (homeTask == null) return null;
        var convertedHomeTask = await _homeTaskFactory.CreateAsync(homeTask);
        return convertedHomeTask;
    }
}