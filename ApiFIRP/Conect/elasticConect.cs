using Elastic.Clients.Elasticsearch;
namespace ApiFIRP.Conection;

public class ElasticConnection
{
    private readonly ElasticsearchClient _client;

    public ElasticConnection(IConfiguration configuration)
    {
        var url = configuration["Elasticsearch:Url"];

        var settings = new ElasticsearchClientSettings(
            new Uri(url!)
        );

        _client = new ElasticsearchClient(settings);
    }

    public ElasticsearchClient GetClient()
    {
        return _client;
    }
}