using sshStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteLS
{
    class Program
    {
        /// <summary>
        /// Attempt to do an ls of our home directory on the tev machines.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var c = new SSHConnection("tev01.phys.washington.edu", "gwatts");
            c.ExecuteCommand("ls", l => Console.WriteLine(l));
        }
    }
}
