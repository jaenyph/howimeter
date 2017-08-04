namespace HowIMeter.Cli.Commands
{
    internal interface ICommand
    {
        ApplicationErrorKind Run();
    }
}