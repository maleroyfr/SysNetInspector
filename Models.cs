<<<<<<< HEAD
﻿public class ServerInfo
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string OSVersion { get; set; }
    public string CPUInfo { get; set; }
    public string InstalledMemory { get; set; }
    public DateTime LastSystemBootTime { get; set; }
}
public class InstalledSoftware
{
    public string Name { get; set; }
    public string Version { get; set; }
}
public class Hotfix
{
    public string HotfixID { get; set; }
    public string Description { get; set; }
    public string InstalledOn { get; set; } // Make sure this property is defined
}
public class LogicalDisk
{
    public string VolumeName { get; set; }
    public string FileSystem { get; set; }
    public long FreeSpaceGB { get; set; }
    public long TotalSizeGB { get; set; }
}
public class NetworkInterface
{
    public string Name { get; set; }
    public string MACAddress { get; set; }
    public string[] IPAddresses { get; set; }
}
public class Port
{
    public string port_number { get; set; }
    public string process_name { get; set; }
    public string protocol { get; set; }

    public string name => $"{process_name} ({protocol} port {port_number})";
}
=======
﻿public class ServerInfo
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string OSVersion { get; set; }
    public string CPUInfo { get; set; }
    public string InstalledMemory { get; set; }
    public DateTime LastSystemBootTime { get; set; }
}
public class InstalledSoftware
{
    public string Name { get; set; }
    public string Version { get; set; }
}
public class Hotfix
{
    public string HotfixID { get; set; }
    public string Description { get; set; }
    public string InstalledOn { get; set; } // Make sure this property is defined
}
public class LogicalDisk
{
    public string VolumeName { get; set; }
    public string FileSystem { get; set; }
    public long FreeSpaceGB { get; set; }
    public long TotalSizeGB { get; set; }
}
public class NetworkInterface
{
    public string Name { get; set; }
    public string MACAddress { get; set; }
    public string[] IPAddresses { get; set; }
}
public class Port
{
    public string port_number { get; set; }
    public string process_name { get; set; }
    public string protocol { get; set; }

    public string name => $"{process_name} ({protocol} port {port_number})";
}
>>>>>>> initial
