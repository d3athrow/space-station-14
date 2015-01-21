﻿using System;
using System.Windows.Forms;
using SS13_Shared.Minidump;

namespace SS13
{
    public class Program
    {
        /************************************************************************/
        /* program starts here                                                  */
        /************************************************************************/
        private static bool fullDump = false;

        [STAThread]
        private static void Main()
        {
            var args = Environment.GetCommandLineArgs();
            //Process command-line args
            processArgs(args);
            //Register minidump dumper only if the app isn't being debugged. No use filling up hard drives with shite
            if (!System.Diagnostics.Debugger.IsAttached)
                MiniDump.Register("crashdump-" + Guid.NewGuid().ToString("N") + ".dmp",
                                  fullDump
                                      ? MiniDump.MINIDUMP_TYPE.MiniDumpWithFullMemory
                                      : MiniDump.MINIDUMP_TYPE.MiniDumpNormal);
            Application.Run(new MainWindow());
        }
        
        private static void processArgs(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--fulldump":
                        fullDump = true;
                        break;
                }
            }
        }
    }
}