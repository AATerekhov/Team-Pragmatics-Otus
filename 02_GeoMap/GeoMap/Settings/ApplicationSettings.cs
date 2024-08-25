namespace GeoMap.Settings
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class ApplicationSettings
    {
        public string? ConnectionString { get; set; }
        public RmqSettings RmqSettings { get; set; }
    }
}
