using System;
using System.Threading;

namespace ShellTest
{
    public class ExitCommand : ShellDLL.Command 
    {
        Calculator calculator;

        public ExitCommand(Calculator cal) : base("exit")   // string keyword) : base(keyword) // szülőosztály konstruktora a base(), vagyis ezt jelenti a base ()
        {
            calculator = cal;
        }

        public override bool Execute(params string[] args)
        {
            // throw new NotImplementedException();

            if (args.Length > 0)
            { 
                return false;
            }

            // new Calculator().Exit(0); // béna megoldás, nem jó
            calculator.Exit(0);
            return true;
        }
    }

    public class PlusCommand : ShellDLL.Command
    {
        Calculator cal;

        public PlusCommand(Calculator cal) : base("+")
        {
            this.cal = cal;
        }

        public override bool Execute(params string[] args)
        {
            if(args.Length != 1)
            {
                return false;
            }
            double number = Convert.ToDouble(args[0]);
            cal.Number += number;
            cal.Format("{0}", cal.Number);

            return true;
        }
    }

    public class MinusCommand : ShellDLL.Command
    {
        Calculator cal;

        public MinusCommand(Calculator cal) : base("-")
        {
            this.cal = cal;
        }

        public override bool Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                return false;
            }
            double number = Convert.ToDouble(args[0]);
            cal.Number -= number; //itt a cal.Number az sh.Number-t jelenti (azzal egyezik meg)
            cal.Format("{0}", cal.Number);
            //Console.WriteLine("lefutaminus");
            return true;
        }
    } 

    public class Calculator : ShellDLL.Shell
    {
        public double Number { get; set; }
        
        protected override void Init()
        {
            base.Init(); // meghívjuk a szülőjét (szülőosztály Init metódusát), vagyis a Shell osztályét
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); // nem tizedesvesszővel kezeli a valós számokat, hanem tizedesponttal
            Number = 0.0;
            Format("{0}", Number);
        }
        
        public Calculator ()
	    {
            AddCommand(new ExitCommand(this));
            AddCommand(new PlusCommand(this));
            AddCommand(new MinusCommand(this));
	    }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ShellDLL.Shell sh = new Calculator();
            sh.ReadEvalPrint();
        }
    }
}