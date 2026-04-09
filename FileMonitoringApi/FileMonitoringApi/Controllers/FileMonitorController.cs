using Microsoft.AspNetCore.Mvc;
using FileMonitoringApi.Services;

namespace FileMonitoringApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileMonitorController : ControllerBase
{
    private readonly FileMonitorService _fileMonitorService;

    public FileMonitorController(FileMonitorService fileMonitorService)
    {
        _fileMonitorService = fileMonitorService;
    }

    [HttpGet("count")]
    public IActionResult Count()
    {
        var count = _fileMonitorService.GetRecordCount();
        return Ok(new { count });
    }

    [HttpGet("export")]
    public IActionResult Export()
    {
        try
        {
            var (newFilePath, newFileName) = _fileMonitorService.ExportFile();

            var fileBytes = System.IO.File.ReadAllBytes(newFilePath);

            return File(fileBytes, "text/csv", newFileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { error = "Monitored file not found" });
        }
    }
}