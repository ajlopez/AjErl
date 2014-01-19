namespace AjErl.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("AjErl alfa 0.0.1");

            Lexer lexer = new Lexer(Console.In);
            Parser parser = new Parser(lexer);
            Machine machine = new Machine();

            while (true)
                try 
                {
                    ProcessExpression(parser, machine.RootContext);
                }
                catch (Exception ex) 
                {
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                }
        }

        private static void ProcessExpression(Parser parser, Context context) 
        {
            IExpression expression = parser.ParseExpression();
            object result = Machine.ExpandDelayedCall(expression.Evaluate(context));

            if (result == null)
                return;

            Console.Write("> ");
            Console.WriteLine(result);
        }
    }
}
