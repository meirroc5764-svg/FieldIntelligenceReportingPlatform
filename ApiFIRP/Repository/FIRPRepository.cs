using ApiFIRP.Model;
using Nest;
namespace ApiFIRP.Services;

public class ReportService : IReportService
{
    private readonly IElasticClient _elasticClient;

    public ReportService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<Report>> GetAllAsync()
    {
        var response = await _elasticClient.SearchAsync<Report>(s => s
            .Query(q => q.MatchAll())
        );

        return response.Documents;
    }

    public async Task<Report?> GetByIdAsync(string reportId)
    {
        var response = await _elasticClient.GetAsync<Report>(reportId);

        if (!response.Found)
            return null;

        return response.Source;
    }

    public async Task<Report> CreateAsync(Report report)
    {
        await _elasticClient.IndexAsync(
            report,
            i => i.Id(report.ReportId)
        );

        return report;
    }

    public async Task<bool> UpdateAsync(string reportId, Report report)
    {
        var response = await _elasticClient.UpdateAsync<Report, Report>(
            reportId,
            u => u.Doc(report)
        );

        return response.IsValid;
    }

    public async Task<bool> DeleteAsync(string reportId)
    {
        var response = await _elasticClient.DeleteAsync<Report>(reportId);

        return response.IsValid;
    }
}