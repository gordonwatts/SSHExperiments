using sshStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSetEnv
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new SSHConnection("tev01.phys.washington.edu", "gwatts");
            c.ExecuteCommand("export JUNK=hi", l => Console.WriteLine(l));
            c.ExecuteCommand("echo This is $JUNK", l => Console.WriteLine(l));
        }
    }
}
