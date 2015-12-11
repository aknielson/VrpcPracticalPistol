using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

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
            Console.ReadLine();
        }
    }
}
