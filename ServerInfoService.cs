using System.Diagnostics;
using System.Management;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using Microsoft.Win32;

public class ServerInfoService
{
    public Task<List<ServiceInfo>> GetServicesAsync()
    {
        return Task.Run(() =>
        {
            var servicesList = new List<ServiceInfo>();
            ServiceController[] services = ServiceController.GetServices();

            foreach (var service in services)
            {
                servicesList.Add(new ServiceInfo
                {
                    ServiceName = service.ServiceName,
                    DisplayName = service.DisplayName,
                    Status = service.Status.ToString()
                });
            }

            return servicesList;
        });
    }

    public Task<List<Port>> GetNetStatPortsAsync()
    {
        var ports = new List<Port>();
        return Task.Run(() =>
        {
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo ps = new ProcessStartInfo
                    {
                        Arguments = "-a -n -o",
                        FileName = "netstat.exe",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    if (exitStatus != "0")
                    {
                        // Command errored. Handle here if need be.
                    }

                    // Process netstat output
                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        string[] tokens = Regex.Split(row, "\\s+");
                        if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                        {
                            string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
                            ports.Add(new Port
                            {
                                protocol = localAddress.Contains("1.1.1.1") ? $"{tokens[1]}v6" : $"{tokens[1]}v4",
                                port_number = localAddress.Split(':')[1],
                                process_name = tokens[1] == "UDP" ? LookupProcess(Convert.ToInt32(tokens[4])) : LookupProcess(Convert.ToInt32(tokens[5]))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return ports;
        });
    }

    public async Task<ServerInfo> GetServerInfoAsync()
    {
        return await Task.Run(() =>
        {
            var serverInfo = new ServerInfo
            {
                Manufacturer = GetWmiFirstPropertyValue("Win32_ComputerSystem", "Manufacturer"),
                Model = GetWmiFirstPropertyValue("Win32_ComputerSystem", "Model"),
                CPUInfo = GetWmiFirstPropertyValue("Win32_Processor", "Name"),
                InstalledMemory = $"{GetTotalInstalledMemory()} GB",
                LastSystemBootTime = GetLastBootUpTime(),
                OSVersion = Environment.OSVersion.ToString(),
                Name = Environment.MachineName
            };

            return serverInfo;
        });
    }

    public async Task<List<Hotfix>> GetHotfixesAsync()
    {
        var hotfixes = new List<Hotfix>();
        await Task.Run(() =>
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_QuickFixEngineering"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    hotfixes.Add(new Hotfix
                    {
                        HotfixID = obj["HotFixID"]?.ToString(),
                        Description = obj["Description"]?.ToString()
                        // Note: InstalledOn property often does not return a value; thus omitted here.
                    });
                }
            }
        });
        return hotfixes;
    }

    public async Task<List<LogicalDisk>> GetLogicalDisksAsync()
    {
        var disks = new List<LogicalDisk>();
        await Task.Run(() =>
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType = 3"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    disks.Add(new LogicalDisk
                    {
                        VolumeName = obj["VolumeName"]?.ToString(),
                        FileSystem = obj["FileSystem"]?.ToString(),
                        FreeSpaceGB = Convert.ToInt64(obj["FreeSpace"]) / (1024 * 1024 * 1024),
                        TotalSizeGB = Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024),
                    });
                }
            }
        });
        return disks;
    }

    public async Task<List<NetworkInterface>> GetNetworkInterfacesAsync()
    {
        return await Task.Run(() =>
        {
            var interfaces = new List<NetworkInterface>();

            // First, get all network adapters that are currently connected (NetConnectionStatus=2).
            using (var adapterSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2"))
            {
                foreach (ManagementObject adapter in adapterSearcher.Get())
                {
                    string adapterID = adapter["DeviceID"].ToString();

                    // For each network adapter, find the corresponding network configuration.
                    using (var configSearcher = new ManagementObjectSearcher($"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index={adapterID} AND IPEnabled=True"))
                    {
                        foreach (ManagementObject config in configSearcher.Get())
                        {
                            // Extract the IP addresses. They are returned as string arrays.
                            string[] ipAddresses = config["IPAddress"] as string[];

                            interfaces.Add(new NetworkInterface
                            {
                                Name = adapter["Name"]?.ToString(),
                                MACAddress = adapter["MACAddress"]?.ToString(),
                                IPAddresses = ipAddresses ?? new string[] { } // Ensure we have an array even if there are no IPs.
                            });
                        }
                    }
                }
            }

            return interfaces;
        });
    }

    public async Task<List<InstalledSoftware>> GetInstalledSoftwareAsync()
    {
        return await Task.Run(() =>
        {
            var softwareList = new List<InstalledSoftware>();
            string[] registryKeys = {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            foreach (var keyPath in registryKeys)
            {
                using (var key = Registry.LocalMachine.OpenSubKey(keyPath))
                {
                    if (key != null)
                    {
                        foreach (var subKeyName in key.GetSubKeyNames())
                        {
                            using (var subKey = key.OpenSubKey(subKeyName))
                            {
                                var displayName = subKey.GetValue("DisplayName") as string;
                                if (!string.IsNullOrEmpty(displayName)) // Ensure there's a name to display
                                {
                                    softwareList.Add(new InstalledSoftware
                                    {
                                        Name = displayName,
                                        Version = subKey.GetValue("DisplayVersion") as string ?? "Unknown"
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return softwareList;

        });

    }
        private string GetWmiFirstPropertyValue(string wmiClass, string property)
    {
        using (var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wmiClass}"))
        {
            foreach (var obj in searcher.Get())
            {
                // If we find a matching object, return its property value.
                // ToString() is called safely with null-conditional operator ?. and null-coalescing operator ??.
                return obj[property]?.ToString() ?? "Unknown";
            }
        }
        // If no objects matched the query, we reach this point and return a default value.
        return "Unknown";
    }

        private static string LookupProcess(int pid)
    {
        try { return Process.GetProcessById(pid).ProcessName; }
        catch (Exception) { return "-"; }
    }
    private double GetTotalInstalledMemory()
    {
        using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
        {
            long totalMemory = 0;
            foreach (var obj in searcher.Get())
            {
                totalMemory += Convert.ToInt64(obj["Capacity"]);
            }
            return totalMemory / 1024.0 / 1024.0 / 1024.0; // Convert bytes to GB
        }
    }

    private DateTime GetLastBootUpTime()
    {
        string lastBootUpTimeStr = GetWmiFirstPropertyValue("Win32_OperatingSystem", "LastBootUpTime");
        return ManagementDateTimeConverter.ToDateTime(lastBootUpTimeStr);
    }
}