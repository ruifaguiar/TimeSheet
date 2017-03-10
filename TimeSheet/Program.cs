using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TimeSheet
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new[] { "Post1_11h55_6_Março", "Post2_10h55_7_Março", "Post3_16h00_7_Março" };
            var passatempo = new IEnumerable<Pessoa>[files.Length];
            var postedDates = new DateTime[]
            {
                new DateTime(2017, 03, 6, 10, 55, 00), new DateTime(2017, 3, 7, 10, 55, 00) ,
                new DateTime(2017, 3, 7, 16, 00, 00)
            };

            var rootPath = "./GoodAfter";
            var parser = new FileParser();
            var responses = new[] { "Diana", "Audrey Hepburn", "Madonna"};

            for (var i = 0; i < files.Length; i++)
            {
                passatempo[i] = parser.ReadFile(Path.Combine(rootPath,files[i]), postedDates[i]).Where(a => a.Response.ToLowerInvariant().Contains(responses[i].ToLowerInvariant())).ToList();

            }

            var names = new List<string>();

            foreach (var pessoa in passatempo[0])
            {

                if (passatempo[1].Any(a => string.Equals(a.Name, pessoa.Name, StringComparison.InvariantCultureIgnoreCase)) &&
                    passatempo[2].Any(a => string.Equals(a.Name, pessoa.Name, StringComparison.InvariantCultureIgnoreCase))
                    )
                {
                    names.Add(pessoa.Name);
                }
            }
            var finalList = new List<Pessoa>();


            foreach (string name in names)
            {
                var participacoes = passatempo.Select(a => a.FirstOrDefault(b => b.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))).ToList();
                var timeSpent = participacoes.ToList().Aggregate(new TimeSpan(0), (p, v) => p.Add(v.TimeSpent));
                var pessoa = new Pessoa
                {
                    Name = name,
                    TimeSpent = timeSpent
                };
                finalList.Add(pessoa);
                
            }
            foreach (var pessoa in finalList.OrderBy(a=>a.TimeSpent))
            {
                Console.WriteLine($"O Participante {pessoa.Name} respondeu acertadamente todos os dias com um tempo total de {pessoa.TimeSpent}");
            }
            

            Console.ReadLine();

        }
    }
}
