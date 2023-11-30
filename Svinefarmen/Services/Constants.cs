namespace Svinefarmen.Services
{
    public static class Constants
    {
        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production.
        public static readonly string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.0.17:8080/Stable/{0}" : "https://localhost:7057/Stable/{0}";
    }
}
