using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Management;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.ProcessOperativeSystem;

namespace WonderfullOffers.Domain.Domain.ProcessOperativeSystem;

public class ProcessOS : IProcessOS
{
    private readonly string _queryCmdWindows = "SELECT CommandLine FROM Win32_Process WHERE ProcessId = ";
    private readonly ErrorSettings _errorSettings;

    public ProcessOS(
        IOptions<ErrorSettings> optionsErrors)
    {
        _errorSettings = optionsErrors.Value;
    }
    public List<Process> GetProcesses(string processName, string filterOption)
    {
        List<Process> chromeProcesses = Process
            .GetProcessesByName(processName).ToList();

        List<Process> processs = new();
        foreach (Process process in chromeProcesses)
        {
            string? commandLine =  GetCommandLineWindows(process.Id);
            if (commandLine != null
                && commandLine.Contains(filterOption))
                processs.Add(process);
        }
        
        return processs;
    }
    public Process GetProcessById(int processId)
    {
        return Process.GetProcessById(processId);
    }

    private string? GetCommandLineWindows(int processId)
    {
        string queryCmdWindowsProccesID = $"{_queryCmdWindows}{processId}";
        ManagementObjectSearcher searcher;

        try
        {
            searcher = new(queryCmdWindowsProccesID);
        }
        catch (Exception)
        {
            throw new Exception(_errorSettings.NoWindowsSO);
        }
        foreach (ManagementObject process in searcher.Get().Cast<ManagementObject>())
        {
            return process["CommandLine"].ToString();
        }
        return null;
    }

    public List<Process> GetProcessByName(List<string> names)
    {
        List<Process> processs = new();

        foreach (string name in names)
        {
            processs.AddRange(Process.GetProcessesByName(name).ToList());
        }
        return processs;
    }

    public async Task KillProcess(
        List<Process> processes) 
    {
        await Task.Run(() =>
            {
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
        );
    }

    public bool HighMeMoryUse(List<string> nameProcess) 
    {
        return GetProcessByName(nameProcess).Count >= 180;
    }
}
