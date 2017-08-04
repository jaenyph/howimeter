using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace HowIMeter.Cli
{
    class Logger
    {
        public static ILog Current { get; private set; }

        static Logger()
        {
            Current = LogManager.GetLogger(typeof(Program));
        }
    }
}
