using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet
{
    class Program
    {
        static void Main(string[] args)
        {
            var pessoas = new List<Pessoa>();
            var startDate = DateTime.Now;

            var inputString = "";
            while (inputString != "exit")
            {
                var pessoa = new Pessoa {Times = new List<TimeSpan>()};
                Console.WriteLine("Insert Name");
                pessoa.Name = Console.ReadLine();
                string inputTime = "";
                var loopTime = "";
                while (loopTime != "ExitTime")
                {
                    Console.WriteLine("Insert Time");
                    inputTime = Console.ReadLine();
                    var splitTime = inputTime.Split(':');

                    pessoa.Times.Add(startDate.AddMinutes(int.Parse(splitTime.First())).AddSeconds(int.Parse(splitTime.Last())) - startDate);
                    Console.WriteLine("Type ExitTime to Next Person or Press Enter to continue");
                    loopTime = Console.ReadLine();
                }
                pessoas.Add(pessoa);
                Console.WriteLine("Type exit or Press Enter to continue");
                inputString = Console.ReadLine();


            }
            foreach (Pessoa pessoa in pessoas)
            {
                TimeSpan x = new TimeSpan(pessoa.Times.Sum(a => a.Ticks));
                Console.WriteLine($"Pessoa = {pessoa.Name} time = {x} ");
            }

            Console.ReadLine();





        }
    }
}
