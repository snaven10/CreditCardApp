using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Web;

public class ServerHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;
    private readonly long _minimumFreeDiskSpaceInBytes;

    public ServerHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
        _minimumFreeDiskSpaceInBytes = _configuration.GetValue<long>("HealthChecks:MinimumFreeDiskSpace");
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var unhealthyDrives = new List<string>();
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                if (drive.AvailableFreeSpace < _minimumFreeDiskSpaceInBytes)
                {
                    unhealthyDrives.Add($"{drive.Name}: {drive.AvailableFreeSpace} bytes disponibles.");
                }
            }

            if (unhealthyDrives.Count == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Todos los discos tienen suficiente espacio."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Degraded($"Problemas en los discos: {string.Join(", ", unhealthyDrives)}"));
            }
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Ocurrió un error al verificar los discos: {ex.Message}"));
        }
    }
}
