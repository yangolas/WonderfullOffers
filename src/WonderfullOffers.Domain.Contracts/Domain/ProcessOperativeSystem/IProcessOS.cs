using System.Diagnostics;

namespace WonderfullOffers.Domain.Contracts.Domain.ProcessOperativeSystem;

public interface IProcessOS
{
    Task KillProcess(
        List<Process> processes);

    List<Process> GetProcesses(
        string processName,
        string filterOption);

    Process GetProcessById(int processId);

    bool HighMeMoryUse(List<string> nameProcess);
}