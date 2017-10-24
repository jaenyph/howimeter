using System.ComponentModel.DataAnnotations;

namespace HowIMeter.Cli
{
    enum ApplicationErrorKind
    {
        NoError = 0,
        GeneralError = -1,
        [Display(Description = "Invalid arguments")]
        InvalidCliArgument = -2,
        [Display(Description = "The provided uri is invalid")]
        InvalidUri
    }
}
