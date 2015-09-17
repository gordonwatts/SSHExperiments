using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sshStuff
{
    public class SSHConnection
    {
        private string _host;
        private string _username;

        SshClient _ssh = null;
        private ShellStream _stream;
        private string _prompt;

        public SSHConnection(string host, string username)
        {
            this._host = host;
            this._username = username;

            _ssh = new SshClient(_host, _username, Passwords.FetchPassword(_host, _username));
            _ssh.Connect();
            _stream = _ssh.CreateShellStream("commands", 240, 200, 132, 80, 240 * 200);

            // Next job, wait until we get a command prompt

            _stream.WriteLine("# this is a test");
            DumpTillFind(_stream, "# this is a test");
            _prompt = _stream.ReadLine();
        }

        /// <summary>
        /// Read the output until we see a particular line.
        /// </summary>
        /// <param name="_stream"></param>
        /// <param name="p"></param>
        private void DumpTillFind(ShellStream stream, string matchText, Action<string> ongo = null)
        {
            while (true)
            {
                var l = stream.ReadLine();
                if (l == null)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    if (l.Contains(matchText))
                    {
                        return;
                    }
                    else
                    {
                        if (ongo != null)
                        {
                            ongo(l);
                        }
                    }
                }
            }
        }

        public void ExecuteCommand(string command, Action<string> outputLines)
        {
            outputLines(string.Format("--> {0}", command));
            _stream.WriteLine(command);
            DumpTillFind(_stream, command);

            DumpTillFind(_stream, _prompt, outputLines);

        }

        /// <summary>
        /// Copy a file from the remote to the local machine
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public FileInfo CopyToLocal(string remoteFileName, string localFileName)
        {
            var scp = new ScpClient(_host, _username, Passwords.FetchPassword(_host, _username));
            scp.Connect();
            var lf = new FileInfo(localFileName);
            scp.Download(remoteFileName, lf);
            return lf;
        }
    }
}
