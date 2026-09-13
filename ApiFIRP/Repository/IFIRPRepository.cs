using ApiFIRP.Model;
namespace ApiFIRP.Services;

public interface IReportService
{
    Task<IEnumerable<Report>> GetAllAsync();

    Task<Report?> GetByIdAsync(string reportId);

    Task<Report> CreateAsync(Report report);

    Task<bool> UpdateAsync(string reportId, Report report);

    Task<bool> DeleteAsync(string reportId);
}
