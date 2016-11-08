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
            var files = new[] { "Post1_3h30_2_novembro.txt", "Post2_3h30_3_novembro.txt", "Post3_3h31_4_novembro.txt", "Post4_3h43_5_novembro.txt", "Post5_3h30_6_novembro.txt" };
            var passatempo = new IEnumerable<Pessoa>[files.Length];
            var postedDates = new DateTime[]
            {
                new DateTime(2016, 11, 2, 15, 30, 00), new DateTime(2016, 11, 3, 15, 30, 00) ,
                new DateTime(2016, 11, 4, 15, 31, 00), new DateTime(2016, 11, 5, 15, 43, 00),new DateTime(2016, 11, 6, 15, 30, 00)
            };
            FileParser parser = new FileParser();
            var responses = new[] { "oris", "longines", "Tissot", "Gucci", "Swatch" };

            for (int i = 0; i < files.Length; i++)
            {
                passatempo[i] = parser.ReadFile(files[i], postedDates[i]).Where(a => a.Response.ToLowerInvariant().Contains(responses[i].ToLowerInvariant())).ToList();

            }

            var names = new List<string>();

            foreach (Pessoa pessoa in passatempo[0])
            {

                if (passatempo[1].Any(a => a.Name.ToLowerInvariant() == pessoa.Name.ToLowerInvariant()) &&
                    passatempo[2].Any(a => a.Name.ToLowerInvariant() == pessoa.Name.ToLowerInvariant()) &&
                    passatempo[3].Any(a => a.Name.ToLowerInvariant() == pessoa.Name.ToLowerInvariant()) &&
                    passatempo[4].Any(a => a.Name.ToLowerInvariant() == pessoa.Name.ToLowerInvariant())
                    )
                {
                    names.Add(pessoa.Name);
                }
            }
            List<Pessoa> finalList = new List<Pessoa>();


            foreach (string name in names)
            {
                var participacoes = passatempo.Select(a => a.FirstOrDefault(b => b.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))).ToList();
                var timeSpent = participacoes.ToList().Aggregate(new TimeSpan(0), (p, v) => p.Add(v.TimeSpent));
                Pessoa pessoa = new Pessoa
                {
                    Name = name,
                    TimeSpent = timeSpent
                };
                finalList.Add(pessoa);
                
            }
            foreach (Pessoa pessoa in finalList.OrderBy(a=>a.TimeSpent))
            {
                Console.WriteLine($"O Participante {pessoa.Name} respondeu acertadamente todos os dias com um tempo total de {pessoa.TimeSpent}");
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
