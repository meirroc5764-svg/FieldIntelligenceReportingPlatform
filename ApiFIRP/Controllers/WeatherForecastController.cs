namespace ApiFIRP.Controllers;

using ApiFIRP.Model;
using ApiFIRP.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _service;

    public ReportController(IReportService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _service.GetAllAsync();

        return Ok(reports);
    }

    [HttpGet("{reportId}")]
    public async Task<IActionResult> GetById(string reportId)
    {
        var report = await _service.GetByIdAsync(reportId);

        if (report == null)
            return NotFound();

        return Ok(report);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Report report)
    {
        var result = await _service.CreateAsync(report);

        return Ok(result);
    }

    [HttpPut("{reportId}")]
    public async Task<IActionResult> Update(
        string reportId,
        Report report)
    {
        var result = await _service.UpdateAsync(reportId, report);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{reportId}")]
    public async Task<IActionResult> Delete(string reportId)
    {
        var result = await _service.DeleteAsync(reportId);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
