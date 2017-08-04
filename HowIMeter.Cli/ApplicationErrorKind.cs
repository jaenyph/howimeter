using System;
using System.Collections.Generic;
using System.Text;

namespace HowIMeter.Cli
{
    enum ApplicationErrorKind
    {
        NoError = 0,
        GeneralError = -1,
        InvalidCliArgument = -2,
        InvalidUri
    }
}
