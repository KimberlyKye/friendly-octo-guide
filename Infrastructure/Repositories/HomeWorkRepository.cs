namespace Infrastructure.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;

public class HomeWorkRepository : IHomeWorkRepository
{
    private readonly AppDbContext _context;
    private readonly IBaseFactory<Entities.HomeWork, DataModels.HomeWork> _homeWorkFactory;


    public HomeWorkRepository(AppDbContext context, IBaseFactory<Entities.HomeWork, DataModels.HomeWork> homeWorkFactory)
    {
        _context = context;
        _homeWorkFactory = homeWorkFactory;
    }

    public async Task<HomeWork> SubmitHomeworkAsync(HomeWork homeWork)
    {
        var submission = await _homeWorkFactory.CreateDataModelAsync(homeWork);
        _context.HomeWorks.Add(submission);
        await _context.SaveChangesAsync();
        homeWork = await _homeWorkFactory.CreateAsync(submission);
        return homeWork;
    }

    public async Task<HomeWork?> GetSubmissionAsync(int submissionId)
    {
        var submissionData = await _context.HomeWorks
            .FirstOrDefaultAsync(s => s.Id == submissionId);
        var submission = await _homeWorkFactory.CreateAsync(submissionData);
        return submission;
    }

    public async Task<HomeWork?> GetSubmissionForStudentAsync(int submissionId, int studentId)
    {
        var submissionData = await _context.HomeWorks
            .FirstOrDefaultAsync(s => s.Id == submissionId && s.StudentId == studentId);
        var submission = await _homeWorkFactory.CreateAsync(submissionData);
        return submission;
    }

    public async Task<HomeWork> UpdateSubmissionAsync(HomeWork homeWork)
    {
        var submission = await _homeWorkFactory.CreateDataModelAsync(homeWork);
        _context.HomeWorks.Update(submission);
        await _context.SaveChangesAsync();
        homeWork = await _homeWorkFactory.CreateAsync(submission);
        return homeWork;
    }

    public async Task<bool> HasUngradedSubmissionAsync(int studentId, int homeTaskId)
    {
        return await _context.HomeWorks
             .AnyAsync(s => s.StudentId == studentId &&
                          s.HomeTaskId == homeTaskId && s.Score < 40);
    }

    public async Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId)
    {
        return await _context.HomeWorks
            .AnyAsync(s => s.StudentId == studentId &&
                         s.HomeTaskId == homeTaskId &&
                         s.Score < 40);
    }
}