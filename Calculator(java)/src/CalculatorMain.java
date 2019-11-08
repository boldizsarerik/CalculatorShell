import shell.Command;
import shell.Shell;

class Calculator extends Shell
{
    private double number;

    protected void Init() 
    {
        super.init(); // meghívjuk a szülőjét (a szülőosztály init metódusát)
        //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); // nem tizedesvesszővel kezeli a valós számokat, hanem tizedesponttal
        number = 0.0;
        format("%f%n", number);
    }

    public Calculator () 
    {
        Init();
        addCommand(new Command("exit")
        {
            public boolean execute(String... strings)
            {
                if(strings.length != 0)
                {
                    return false;
                }
                format("%f%n", number);
                exit(0);
                return true;
            }
        }); //csak egy helyen kell példányosítani az exit parancsot, ezért névtelen osztállyal oldjuk meg 
        addCommand(new Command("+")
        {
            public boolean execute(String... strings)
            {
                if(strings.length != 1)
                {
                    return false;
                }
                number += Double.parseDouble(strings[0]);
                format("%f%n", number);
                return true;
            }
        });
        addCommand(new Command("-")
        {
             public boolean execute(String... strings)
             {
                 if(strings.length != 1)
                 {
                     return false;
                 }
                 number -= Double.parseDouble(strings[0]);
                 format("%f%n", number);
                 return true;
             }
         });
    }
}

public class CalculatorMain
    {
        public static void main(String[] args) 
        {
            //Shell sh = new Calculator();
            Shell sh = Loader.load();
            sh.readEvalPrint();
        }
    }