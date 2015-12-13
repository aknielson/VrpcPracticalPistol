using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DomainClasses;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new VrpcPracticalPistolContext();
            foreach (var member in db.Members)
            {
                Console.WriteLine(member.FirstName);
            }

            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 1).Date });
            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 2).Date });
            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 3).Date });
            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 4).Date });
            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 5).Date });
            db.Matches.Add(new Match { Date = DateTime.Now.AddDays(-7 * 6).Date });

            db.SaveChanges();
            Console.ReadLine();
        }
    }
}
