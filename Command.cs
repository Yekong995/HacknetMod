using Hacknet;
using Hacknet.Daemons.Helpers;
using System.Linq;
using System.Threading;

using WorldToolKit.Utils;
using WorldToolKit.Tools;
using System.Collections.Generic;
using Pathfinder.Port;

namespace WorldToolKit.Command
{
    public class CommandManager
    {
        // Command that can crack any port on any computer
        public static void PortHackCommand(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            if (computer == null)
            {
                os.write("Error: Computer not found!");
                return;
            }
            else if (args.Length < 2)
            {
                os.write("Usage: PortHack [port]");
                return;
            }

            int port = int.Parse(args[1]);
            port = computer.GetCodePortNumberFromDisplayPort(port);
            if (computer.isPortOpen(port))
            {
                os.write("Port " + port + " already opened!");
                return;
            }
            else
            {
                os.write("Hacking port " + args[1]);
                computer.hostileActionTaken();
                Thread.Sleep(2000);
                computer.openPort(port, os.thisComputer.ip);
                os.write("Port " + port + " opened!");
            }
        }

        // Command that can bypass any proxy on any computer
        public static void BypassCommand(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            if (computer == null)
            {
                os.write("Error: Computer not found!");
                return;
            }
            else if (computer.proxyActive == false)
            {
                os.write("Proxy already bypassed!");
                return;
            }

            os.write("Bypassing proxy...");
            computer.hostileActionTaken();
            Thread.Sleep(2000);
            computer.proxyActive = false;
            os.write("Proxy bypassed!");
        }


        // Command that can bypass any firewall on any computerS
        public static void FirewallCommand(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            if (computer == null)
            {
                os.write("Error: Computer not found!");
                return;
            }
            else if (!computer.firewall.solved == false)
            {
                os.write("Firewall already bypassed!");
                return;
            }

            os.write("Bypassing firewall...");
            computer.hostileActionTaken();
            Thread.Sleep(2000);
            computer.firewall.solved = true;
            os.write("Firewall bypassed!");
        }

        // Command that can send a message to any IRC on any computer
        public static void IRCCommand(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            IRCSystem irc = ComUtils.hasIRC(computer);
            if (irc == null)
            {
                os.write("Error: IRC not found!");
                return;
            }
            else if (args.Length < 2)
            {
                os.write("Usage: IRC [message]");
                return;
            }

            string username = os.SaveUserAccountName;
            irc.AddLog(username, string.Join(" ", args.Skip(1)));
        }

        // Command that can initialize a backdoor on any computer
        public static void InitializeBackdoor(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            bool permissions = computer.PlayerHasAdminPermissions();

            if (permissions == false)
            {
                os.write("Error: Insufficient permissions!");
                return;
            }

            string ip = computer.ip;
            string key_text = "Initializing backdoor on " + ip + " state=true";
            string key = WorldTools.GetHashString(key_text);

            List<int> path = computer.getFolderPath("sys");
            os.write("Initializing backdoor...");
            Thread.Sleep(500);
            computer.makeFile(ip, "backdoor.dll", key, path, true);
            os.write("Backdoor initialized!");
        }

        // Command that can load a backdoor on any computer
        public static void BackdoorCommand(OS os, string[] args)
        {
            Computer computer = ComUtils.GetComputer(os);
            string user_ip = os.thisComputer.ip;

            string ip = computer.ip;
            string key_text = "Initializing backdoor on " + ip + " state=true";
            string key = WorldTools.GetHashString(key_text);

            FileEntry file = computer.getFolderFromPath("sys").files.FirstOrDefault((FileEntry f) => f.name == "backdoor.dll");
            if (file == null)
            {
                os.write("Error: Backdoor not found!");
                return;
            }
            else if (file.data != key)
            {
                os.write("Error: Backdoor not initialized!");
                return;
            }

            os.write("Loading backdoor...");
            Thread.Sleep(500);
            computer.giveAdmin(user_ip);
            computer.userLoggedIn = true;
            os.write("Backdoor loaded!");
        }

        // Command that can kill the trace tracker on any computer
        public static void TrapKill(OS os, string[] args)
        {
            os.traceTracker.stop();
        }

        // Command that can reset the security level or the port on any computer
        public static void CrashCommand(OS os, string[] arg)
        {
            Computer computer = ComUtils.GetComputer(os);

            int needed_port = computer.portsNeededForCrack + 1;
            int security_level = computer.securityLevel;
            List<PortData> finded_ports = computer.GetAllPorts();

            bool at_least_one_open = false;
            foreach (PortData port in finded_ports)
            {
                if (port.Cracked == true)
                {
                    at_least_one_open = true;
                    break;
                }
            }

            if (computer == null)
            {
                os.write("Error: Computer no found!");
                return;
            } else if (at_least_one_open == false)
            {
                os.write("Error: Please crack at least one port!");
                return;
            }

            if (needed_port > finded_ports.Count())
            {
                Thread.Sleep(500);
                computer.portsNeededForCrack = finded_ports.Count() - 1;
                os.write("Successfully to reset the port");
            }
            else if (security_level == 5)
            {
                Thread.Sleep(500);
                computer.securityLevel = 4;
                os.write("Successfully to reset security level");
            }
            else
            {
                os.write("Error: Nothing need to crash");
                return;
            }
        }

    }
}
