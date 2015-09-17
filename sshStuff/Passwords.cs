using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sshStuff
{
    /// <summary>
    /// Use the windows passwords to fetch things for SSH log ins.
    /// </summary>
    static class Passwords
    {
        /// <summary>
        /// Get a password for a particular host and username. Throw if it isn't there.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string FetchPassword (string host, string username)
        {
            var cs = new CredentialSet(host);
            var thePasswordInfo = cs.Load().Where(c => c.Username == username).FirstOrDefault();
            if (thePasswordInfo == null)
            {
                throw new ArgumentException("Must enter password");
            }

            return thePasswordInfo.Password;
        }
    }
}
