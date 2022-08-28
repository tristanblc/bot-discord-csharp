namespace ServiceClassLibrary.Interfaces
{
    public interface IServerInfoService
    {
        string GetMemeryUsedInformation(int occ);
        string GetProcessorPerformamceUsed(int occ);
    }
}