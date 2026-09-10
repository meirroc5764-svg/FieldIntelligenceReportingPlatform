using Elastic.Clients.Elasticsearch;
using ValidationProcess.Models;

namespace validationProcess.ConectElastic;

public class ElasticMappingConect
{
    private ElasticsearchClient _client;

    public ElasticMappingConect()
    {
        var setting = new ElasticsearchClientSettings(
            new Uri("http://localhost:9200"));

        _client = new ElasticsearchClient(setting);
    }

    public async Task CreateMappingAsync()
    {
        await _client.Indices.CreateAsync<Report>("reports",
            r => r
            .Mappings(m => m
            .Properties(p => p
            .Keyword(x => x.ReportId)
            .Date(x => x.Timestamp)
            .Keyword(x => x.AgentId)
            .Keyword(x => x.Unit)
            .Keyword(x => x.Theater)
            .Keyword(x => x.Sector)
            .Keyword(x => x.Location)
            .Keyword(x => x.ReportType)
            .Keyword(x => x.Priority)
            .Keyword(x => x.SourceType)
            .Text(x => x.Message)
            .Keyword(x => x.SubjectId)
            .Keyword(x => x.SubjectType)
            )));
    }

    public async Task Send(Report report)
    {
        await _client.IndexAsync(report, x => x.Index("reports")
                                                .Id(report.ReportId.ToString()));
    }
}