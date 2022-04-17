// Used libraries / NuGet packages:
//      Mono.Options
//      System.Drawing.Common

using MakeSprite;
using Manifold.IO;
using Mono.Options;
using System;
using System.Collections.Generic;

// See https://aka.ms/new-console-template for more information

OperationMode opMode = 0;
bool showHelp = false;

var optionsSet = new OptionSet()
{
    { "m|mode=", $"the mode of operation for this tool",
        arg => opMode = (OperationMode)int.Parse(arg) },

    { "h|help", "show this message and exit",
        arg => showHelp = arg != null },
};

////////////////////////////////////////////////////////////////////
{
    List<string> extra;
    try
    {
        extra = optionsSet.Parse(args);
    }
    catch (OptionException e)
    {
        Console.Write("Error(s): ");
        Console.WriteLine(e.Message);
        return;
    }
}


if (showHelp)
{
    Console.WriteLine("Options:");
    optionsSet.WriteOptionDescriptions(Console.Out);
}

switch (opMode)
{
    case OperationMode.interactive:
        {
            Console.WriteLine("Interactive mode");
        }
        break;
    case OperationMode.directory:
        {
            Console.WriteLine("Directory mode");
        }
        break;
    case OperationMode.file:
        {
            Console.WriteLine("File mode.");
        }
        break;

    default:
        throw new NotImplementedException();
}