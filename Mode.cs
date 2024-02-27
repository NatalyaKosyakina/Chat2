using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat2
{
    abstract class Mode
    {
        protected Server _server;

        public void SetServer(Server server)
        {
            this._server = server;
        }

        public abstract void Handle1();
    }
}
