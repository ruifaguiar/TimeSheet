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
            var files = new[] { "Post1_3h30_2_novembro.txt", "Post2_3h30_3_novembro.txt", "Post3_3h31_4_novembro.txt", "Post4_3h43_5_novembro.txt" };
            var passatempo = new IEnumerable<Pessoa>[files.Length];
            var postedDate = new DateTime(2016, 11, 2, 15, 30, 00);
            FileParser parser = new FileParser();

            for (int i = 0; i < files.Length; i++)
            {
                var peopleFromTwo = parser.ReadFile(files[i]);
                passatempo[i] = peopleFromTwo.Where(a =>
                         a.Response.ToLowerInvariant().Contains("oris") ||
                         a.Response.ToLowerInvariant().Contains("orís"));
            }



            for (int i = 0; i < files.Length; i++)
            {
                foreach (Pessoa pessoa in passatempo[i])
                {
                    Console.WriteLine(
                        $" {pessoa.Name} com a resposta {pessoa.Response} demorou {pessoa.TimePosted - postedDate}");
                }
            }

            Console.ReadLine();
        }

        //public void Calculate()
        //{
        //    var pessoas = new List<Pessoa>();
        //    var startDate = DateTime.Now;

        //    var inputString = "";
        //    while (inputString != "exit")
        //    {
        //        var pessoa = new Pessoa { Time = new TimeSpan() };
        //        Console.WriteLine("Insert Name");
        //        pessoa.Name = Console.ReadLine();
        //        string inputTime = "";
        //        var loopTime = "";
        //        while (loopTime != "ExitTime")
        //        {
        //            Console.WriteLine("Insert Time");
        //            inputTime = Console.ReadLine();
        //            var splitTime = inputTime.Split(':');

        //           // pessoa.Times.Add(startDate.AddMinutes(int.Parse(splitTime.First())).AddSeconds(int.Parse(splitTime.Last())) - startDate);
        //            Console.WriteLine("Type ExitTime to Next Person or Press Enter to continue");
        //            loopTime = Console.ReadLine();
        //        }
        //        pessoas.Add(pessoa);
        //        Console.WriteLine("Type exit or Press Enter to continue");
        //        inputString = Console.ReadLine();


        //    }
        //    foreach (Pessoa pessoa in pessoas)
        //    {

        //        Console.WriteLine($"Pessoa = {pessoa.Name} time = {pessoa.Time} ");
        //    }

        //    Console.ReadLine();

        //}
    }
}
