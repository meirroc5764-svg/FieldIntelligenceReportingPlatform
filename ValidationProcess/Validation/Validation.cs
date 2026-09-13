using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;
using ValidationProcess.Models;

namespace ValidationProcess.validationProcess;

public class ValidationMyReport
{
    public Report ValidationExec(ConsumeResult<Null, string> result)
    {
        var message = result.Message.Value.Trim();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        options.Converters.Add(new JsonStringEnumConverter());

        try
        {
            var report = JsonSerializer.Deserialize<Report>(message, options);

            if (!IsEmpty(report))
            {
                return null;
            }

            if (!IsValidPriority(report))
            {
                return null;
            }

            return report;


        }
        catch
        {
            Console.WriteLine($"the message:{message} invalid");
            return null;
        }
    }

    private bool IsValidPriority(Report report)
    {
        bool subjectId = string.IsNullOrEmpty(report.SubjectId);
        bool subjectType = string.IsNullOrEmpty(report.SubjectType);

        return subjectId == subjectType;
    }

    private bool IsEmpty(Report report)
    {
        if (report == null)
        {
            Console.WriteLine("Report is invalid or empty");
            return false;
        }

        report.ReportId = IsNotEmptyNotWhiteSpace(report.ReportId);
        if (report.ReportId == null)
        {
            Console.WriteLine("ReportId is invalid or empty");
            return false;
        }

        if (report.Timestamp == default)
        {
            Console.WriteLine("Timestamp is invalid or empty");
            return false;
        }

        report.AgentId = IsNotEmptyNotWhiteSpace(report.AgentId);
        if (report.AgentId == null)
        {
            Console.WriteLine("AgentId is invalid or empty");
            return false;
        }

        report.Unit = IsNotEmptyNotWhiteSpace(report.Unit);
        if (report.Unit == null)
        {
            Console.WriteLine("Unit is invalid or empty");
            return false;
        }

        report.Theater = IsNotEmptyNotWhiteSpace(report.Theater);
        if (report.Theater == null)
        {
            Console.WriteLine("Theater is invalid or empty");
            return false;
        }

        report.Sector = IsNotEmptyNotWhiteSpace(report.Sector);
        if (report.Sector == null)
        {
            Console.WriteLine("Sector is invalid or empty");
            return false;
        }

        report.Location = IsNotEmptyNotWhiteSpace(report.Location);
        if (report.Location == null)
        {
            Console.WriteLine("Location is invalid or empty");
            return false;
        }

        report.SourceType = IsNotEmptyNotWhiteSpace(report.SourceType);
        if (report.SourceType == null)
        {
            Console.WriteLine("SourceType is invalid or empty");
            return false;
        }

        report.Message = IsNotEmptyNotWhiteSpace(report.Message);
        if (report.Message == null)
        {
            Console.WriteLine("Message is invalid or empty");
            return false;
        }

        return true;
    }

    private string IsNotEmptyNotWhiteSpace(string report)
    {
        if (string.IsNullOrEmpty(report))
        {
            return null;
        }
        report = report.Trim();
        
        return report;
    }
}