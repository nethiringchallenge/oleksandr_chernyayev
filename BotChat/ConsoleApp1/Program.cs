using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           var reader = new FileReader(args);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Привет. Как дела на плюке? ");
            while (true)
            {
                string input = Console.ReadLine();
                CommandBase command = CommandBase.CreateCommand(input);
                Console.WriteLine(command.GetResult());
            }
        }
    }

    public class CommandReader
    {
        
    }

    public abstract class CommandBase
    {
        public abstract string GetResult();

        public static CommandBase CreateCommand(string input)
        {
            string[] args = input.Split(':');
            if(args.Length != 2) { return new ErrorCommand(); }

            string str = args[0].Trim().ToLower();
            if (str == "calculate")
            {
                return new CalculateCommand(args[1].Trim().ToLower());    
            } else if (str == "strategy")
            {
                return new StrategyCommand(args[1].Trim().ToLower());
            }
            return new ErrorCommand();
        }
    }

    public class ErrorCommand: CommandBase
    {
        public override string GetResult()
        {
            return "У тебя в голове мозги или кю?!";
        }
    }

    public class CalculateCommand: CommandBase
    {
        private int res = -1;
        public CalculateCommand(string input)
        {
            string[] values = input.Split('+');
            if (values.Length < 2)
            {
                
            }
        }

        public override string GetResult()
        {
            if (false)
            {
                return "У тебя в голове мозги или кю?!";
            }
            return string.Format("Я тебя полюбил — я тебя научу: % результат %");
        }
    }

    public class StrategyCommand : CommandBase
    {
        private string[] SuportedSymbols = new[] {"rand", "upseq", "downseq"};
        private string Strategy;

        public StrategyCommand(string input)
        {
            if (SuportedSymbols.Any(str => input == str))
            {
                Strategy = input;
            }
        }
        public override string GetResult()
        {
            if (string.IsNullOrEmpty(Strategy))
            {
                return "У тебя в голове мозги или кю?!";
            }
            return $"Как советовать, так все чатлане. Использую: %{Strategy}%";
        }
    }

    public class FileReader {
        private string FileNameAttribute = "-f";
        private string FilePath;
        public Dictionary<int, string> Answers = new Dictionary<int, string>(10000000);

        public FileReader(string[] args)
        {
            for (int index = 0; index < args.Length; index++){
                if (args[index] == FileNameAttribute && index < args.Length - 1) {
                    FilePath = args[index + 1];
                }
            }

            if (string.IsNullOrEmpty(FilePath))
            {
                throw new ArgumentNullException("FilePath Should not be null");
            }

            try
            {
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    int index = 0;
                    while (!stream.EndOfStream)
                    {
                        string tmp = stream.ReadLine();
                        if (tmp.StartsWith(">"))
                        {
                            Answers[++index] = tmp;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new IOException("FilePath wrong");
            }
            
        }
        
    }

}
