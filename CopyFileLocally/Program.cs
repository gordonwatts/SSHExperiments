using sshStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFileLocally
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new SSHConnection("tev01.phys.washington.edu", "gwatts");
            var f = c.CopyToLocal("./wget-log", "wget.log");
            Console.WriteLine(f.FullName);
        }
    }
}
