using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess.Samples
{
    class Program
    {
        public static IDictionary<string, Action> Samples { get; } = new Dictionary<string, Action>()
        {
            { "Basic", BasicSample.Sample.Run },
            { "ChildTable", ChildTableSample.Sample.Run },
            { "DataContext", DataContextSample.Sample.Run },            
        };

        static void Main(string[] args)
        {
            var proceed = true;
            do
            {
                Console.Write("> ");
                var command = Console.ReadLine();
                var commandParts = command.Split(' ');
                var commandName = commandParts[0];
                var commandParameters = new string[commandParts.Length - 1];
                Array.Copy(commandParts, 1, commandParameters, 0, commandParts.Length - 1);

                switch (commandName)
                {
                    case "list":
                        foreach (var key in Samples.Keys)
                            InfoLine($" - {key}");
                        break;
                    case "run":
                        if (commandParameters.Length == 0)        
                            ErrorMessage("Parameter missed. You should use follow command: run SampleName");                        
                        else if (Samples.ContainsKey(commandParameters[0]))
                        {
                            InfoLine($"Gremlins {commandParameters[0]} Sample");
                            Samples[commandParameters[0]].Invoke();
                        }
                        else
                            ErrorMessage($"No Sample with name {commandParameters[0]} found.");
                        break;
                    case "exit":
                        proceed = false;
                        break;
                    default:
                        Console.WriteLine("Gremlins.DataAccess Sample Library");
                        Console.WriteLine("Commands:");
                        Console.WriteLine("list - get list of available samples.");
                        Console.WriteLine("run sampleName - run sample with specified sample name.");
                        Console.WriteLine("exit - close app");
                        break;
                }
            }
            while (proceed);
        }

        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void InfoLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
