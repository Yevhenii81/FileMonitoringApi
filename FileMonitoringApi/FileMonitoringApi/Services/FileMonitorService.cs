namespace FileMonitoringApi.Services;

public class FileMonitorService
{
    private readonly string _filePath;

    public FileMonitorService(IConfiguration configuration)
    {
        _filePath = configuration["FileMonitor:FilePath"]
            ?? throw new InvalidOperationException("FileMonitor:FilePath is not configured");
    }

    public int GetRecordCount()
    {
        if (!File.Exists(_filePath))
            return 0;

        var lines = File.ReadAllLines(_filePath);

        if (lines.Length == 0)
            return 0;

        return lines.Skip(1).Count(line => !string.IsNullOrWhiteSpace(line));
    }

    public (string newFilePath, string newFileName) ExportFile()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException("Monitored file not found", _filePath);

        var directory = Path.GetDirectoryName(_filePath)!;

        var originalFileName = Path.GetFileName(_filePath);

        var timestamp = DateTime.Now.ToString("yyyy.MM.dd_HH.mm");
        var newFileName = $"[{timestamp}]_[{originalFileName}]";

        var outDirectory = Path.Combine(directory, "out");

        Directory.CreateDirectory(outDirectory);

        var newFilePath = Path.Combine(outDirectory, newFileName);

        File.Move(_filePath, newFilePath);

        return (newFilePath, newFileName);
    }
}