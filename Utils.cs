using Hacknet;
using Hacknet.Daemons.Helpers;

namespace WorldToolKit.Utils
{
    public class ComUtils
    {
        public static IRCSystem hasIRC(Computer c)
        {
            IRCSystem irc = null;
            foreach (Daemon daemon in c.daemons)
            {
                if (daemon is DLCHubServer)
                {
                    irc = ((DLCHubServer)daemon).IRCSystem;
                    break;
                }
                else if (daemon is IRCDaemon)
                {
                    irc = ((IRCDaemon)daemon).System;
                }
            }
            return irc;
        }

        public static string FolderName(OS os, string name)
        {
            Folder folder = Programs.getCurrentFolder(os);
            if (!folder.containsFile(name) && folder.searchForFolder(name) == null)
            {
                return name;
            }
            else
            {
                for (int i = 1; ; i++)
                {
                    string newName = name + "_" + i;
                    if (!folder.containsFile(newName) && folder.searchForFolder(newName) == null)
                    {
                        return newName;
                    }
                }
            }
        }

        public static Computer GetComputer(OS os)
        {
            return os.connectedComp ?? os.thisComputer;
        }
    }
}