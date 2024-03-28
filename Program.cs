<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;

class Program
{
    private static readonly ServerInfoService service = new ServerInfoService();

    static async Task Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await DisplayServerInformation();
                    break;
                case "2":
                    await DisplayInstalledHotfixes();
                    break;
                case "3":
                    await DisplayLogicalDiskInformation();
                    break;
                case "4":
                    await DisplayNetworkInterfaceInformation();
                    break;
                case "5":
                    await DisplayInstalledApplications();
                    break;
                case "6":
                    await DisplayNetworkConnections();
                    break;
                case "7":
                    await ExportAllInformationToXML();
                    break;
                case "8":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. Display Server Information");
        Console.WriteLine("2. Display Installed Hotfixes");
        Console.WriteLine("3. Display Logical Disk Information");
        Console.WriteLine("4. Display Network Interface Information");
        Console.WriteLine("5. Display Installed Applications");
        Console.WriteLine("6. Display Network Connections");
        Console.WriteLine("7. Export All Information to XML");
        Console.WriteLine("8. Exit");
        Console.ResetColor();
        Console.Write("\nSelect an option: ");
    }

    static async Task DisplayServerInformation()
    {
        var serverInfo = await service.GetServerInfoAsync();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Server Information]");
        Console.WriteLine($"Name: {serverInfo.Name}");
        Console.WriteLine($"Manufacturer: {serverInfo.Manufacturer}");
        Console.WriteLine($"Model: {serverInfo.Model}");
        Console.WriteLine($"OS Version: {serverInfo.OSVersion}");
        Console.WriteLine($"CPU Info: {serverInfo.CPUInfo}");
        Console.WriteLine($"Installed Memory: {serverInfo.InstalledMemory}");
        Console.WriteLine($"Last System Boot Time: {serverInfo.LastSystemBootTime}");
        Console.ResetColor();
    }

    static async Task DisplayInstalledApplications()
    {
        var apps = await service.GetInstalledSoftwareAsync();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Installed Applications]");
        foreach (var app in apps)
        {
            Console.WriteLine($"Name: {app.Name}, Version: {app.Version}");
        }
        Console.ResetColor();
    }
    static async Task DisplayNetworkConnections()
    {
        var ports = await service.GetNetStatPortsAsync();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n[Network Connections]");
        foreach (var port in ports)
        {
            Console.WriteLine($"{port.name}");
        }
        Console.ResetColor();
    }

    static async Task DisplayInstalledHotfixes()
    {
        var hotfixes = await service.GetHotfixesAsync();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[Installed Hotfixes]");
        foreach (var hotfix in hotfixes)
        {
            Console.WriteLine($"ID: {hotfix.HotfixID}, Description: {hotfix.Description}");
        }
        Console.ResetColor();
    }

    static async Task DisplayLogicalDiskInformation()
    {
        var disks = await service.GetLogicalDisksAsync();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n[Logical Disks]");
        foreach (var disk in disks)
        {
            Console.WriteLine($"Volume: {disk.VolumeName}, FileSystem: {disk.FileSystem}, FreeSpaceGB: {disk.FreeSpaceGB}, TotalSizeGB: {disk.TotalSizeGB}");
        }
        Console.ResetColor();
    }

    static async Task DisplayNetworkInterfaceInformation()
    {
        var nics = await service.GetNetworkInterfacesAsync();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n[Network Interfaces]");
        foreach (var nic in nics)
        {
            // Check if there are any IP addresses to display
            string ipAddressesDisplay = nic.IPAddresses != null && nic.IPAddresses.Length > 0
                                        ? String.Join(", ", nic.IPAddresses)
                                        : "None";

            Console.WriteLine($"Name: {nic.Name}, MACAddress: {nic.MACAddress}, IPAddresses: {ipAddressesDisplay}");
        }
        Console.ResetColor();
    }

    static async Task ExportAllInformationToXML()
    {
        Console.WriteLine("Exporting all information to XML...");

        // Export Server Information
        var serverInfo = await service.GetServerInfoAsync();
        XmlExporter.ExportToXml(serverInfo, "ServerInfo.xml");
        Console.WriteLine("Server Information exported.");

        // Export Installed Hotfixes
        var hotfixes = await service.GetHotfixesAsync();
        XmlExporter.ExportToXml(hotfixes, "Hotfixes.xml");
        Console.WriteLine("Installed Hotfixes exported.");

        // Export Logical Disks Information
        var disks = await service.GetLogicalDisksAsync();
        XmlExporter.ExportToXml(disks, "LogicalDisks.xml");
        Console.WriteLine("Logical Disks Information exported.");

        // Export Network Interfaces Information
        var interfaces = await service.GetNetworkInterfacesAsync();
        XmlExporter.ExportToXml(interfaces, "NetworkInterfaces.xml");
        Console.WriteLine("Network Interfaces Information exported.");

        // Export Installed Applications
        var applications = await service.GetInstalledSoftwareAsync();
        XmlExporter.ExportToXml(applications, "InstalledApplications.xml");
        Console.WriteLine("Installed Applications exported.");

        // Export Network Connections
        var networkConnections = await service.GetNetStatPortsAsync();
        XmlExporter.ExportToXml(networkConnections, "NetworkConnections.xml");
        Console.WriteLine("Network Connections exported.");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All information has been successfully exported to XML.");
        Console.ResetColor();
    }

=======
﻿using System;
using System.Threading.Tasks;

class Program
{
    private static readonly ServerInfoService service = new ServerInfoService();

    static async Task Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await DisplayServerInformation();
                    break;
                case "2":
                    await DisplayInstalledHotfixes();
                    break;
                case "3":
                    await DisplayLogicalDiskInformation();
                    break;
                case "4":
                    await DisplayNetworkInterfaceInformation();
                    break;
                case "5":
                    await DisplayInstalledApplications();
                    break;
                case "6":
                    await DisplayNetworkConnections();
                    break;
                case "7":
                    await ExportAllInformationToXML();
                    break;
                case "8":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. Display Server Information");
        Console.WriteLine("2. Display Installed Hotfixes");
        Console.WriteLine("3. Display Logical Disk Information");
        Console.WriteLine("4. Display Network Interface Information");
        Console.WriteLine("5. Display Installed Applications");
        Console.WriteLine("6. Display Network Connections");
        Console.WriteLine("7. Export All Information to XML");
        Console.WriteLine("8. Exit");
        Console.ResetColor();
        Console.Write("\nSelect an option: ");
    }

    static async Task DisplayServerInformation()
    {
        var serverInfo = await service.GetServerInfoAsync();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Server Information]");
        Console.WriteLine($"Name: {serverInfo.Name}");
        Console.WriteLine($"Manufacturer: {serverInfo.Manufacturer}");
        Console.WriteLine($"Model: {serverInfo.Model}");
        Console.WriteLine($"OS Version: {serverInfo.OSVersion}");
        Console.WriteLine($"CPU Info: {serverInfo.CPUInfo}");
        Console.WriteLine($"Installed Memory: {serverInfo.InstalledMemory}");
        Console.WriteLine($"Last System Boot Time: {serverInfo.LastSystemBootTime}");
        Console.ResetColor();
    }

    static async Task DisplayInstalledApplications()
    {
        var apps = await service.GetInstalledSoftwareAsync();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[Installed Applications]");
        foreach (var app in apps)
        {
            Console.WriteLine($"Name: {app.Name}, Version: {app.Version}");
        }
        Console.ResetColor();
    }
    static async Task DisplayNetworkConnections()
    {
        var ports = await service.GetNetStatPortsAsync();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n[Network Connections]");
        foreach (var port in ports)
        {
            Console.WriteLine($"{port.name}");
        }
        Console.ResetColor();
    }

    static async Task DisplayInstalledHotfixes()
    {
        var hotfixes = await service.GetHotfixesAsync();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n[Installed Hotfixes]");
        foreach (var hotfix in hotfixes)
        {
            Console.WriteLine($"ID: {hotfix.HotfixID}, Description: {hotfix.Description}");
        }
        Console.ResetColor();
    }

    static async Task DisplayLogicalDiskInformation()
    {
        var disks = await service.GetLogicalDisksAsync();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n[Logical Disks]");
        foreach (var disk in disks)
        {
            Console.WriteLine($"Volume: {disk.VolumeName}, FileSystem: {disk.FileSystem}, FreeSpaceGB: {disk.FreeSpaceGB}, TotalSizeGB: {disk.TotalSizeGB}");
        }
        Console.ResetColor();
    }

    static async Task DisplayNetworkInterfaceInformation()
    {
        var nics = await service.GetNetworkInterfacesAsync();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n[Network Interfaces]");
        foreach (var nic in nics)
        {
            // Check if there are any IP addresses to display
            string ipAddressesDisplay = nic.IPAddresses != null && nic.IPAddresses.Length > 0
                                        ? String.Join(", ", nic.IPAddresses)
                                        : "None";

            Console.WriteLine($"Name: {nic.Name}, MACAddress: {nic.MACAddress}, IPAddresses: {ipAddressesDisplay}");
        }
        Console.ResetColor();
    }

    static async Task ExportAllInformationToXML()
    {
        Console.WriteLine("Exporting all information to XML...");

        // Export Server Information
        var serverInfo = await service.GetServerInfoAsync();
        XmlExporter.ExportToXml(serverInfo, "ServerInfo.xml");
        Console.WriteLine("Server Information exported.");

        // Export Installed Hotfixes
        var hotfixes = await service.GetHotfixesAsync();
        XmlExporter.ExportToXml(hotfixes, "Hotfixes.xml");
        Console.WriteLine("Installed Hotfixes exported.");

        // Export Logical Disks Information
        var disks = await service.GetLogicalDisksAsync();
        XmlExporter.ExportToXml(disks, "LogicalDisks.xml");
        Console.WriteLine("Logical Disks Information exported.");

        // Export Network Interfaces Information
        var interfaces = await service.GetNetworkInterfacesAsync();
        XmlExporter.ExportToXml(interfaces, "NetworkInterfaces.xml");
        Console.WriteLine("Network Interfaces Information exported.");

        // Export Installed Applications
        var applications = await service.GetInstalledSoftwareAsync();
        XmlExporter.ExportToXml(applications, "InstalledApplications.xml");
        Console.WriteLine("Installed Applications exported.");

        // Export Network Connections
        var networkConnections = await service.GetNetStatPortsAsync();
        XmlExporter.ExportToXml(networkConnections, "NetworkConnections.xml");
        Console.WriteLine("Network Connections exported.");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All information has been successfully exported to XML.");
        Console.ResetColor();
    }

>>>>>>> initial
}