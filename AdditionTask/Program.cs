using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionTask
{
    class Employeers
    { }

    [AccessLevel(Leavel.LowControl)]
    class Manager : Employeers
    { }
    [AccessLevel(Leavel.MediumControl)]
    class Programer : Employeers
    { }
    [AccessLevel(Leavel.HighControl)]
    class Director : Employeers
    { }
    class Program
    {
        static void Main(string[] args)
        {

            Employeers[] emp = new Employeers[] { new Manager(), new Director(), new Programer() };

            foreach (Employeers item in emp)
            {
                Type type = item.GetType();
                var access = type.GetCustomAttributes(false);
                foreach (AccessLevelAttribute accessLeavel in access)
                {
                    Console.WriteLine(accessLeavel.Access);
                }
            }
        }
    }
}
