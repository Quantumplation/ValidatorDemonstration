using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorDemo.Data;

namespace ValidatorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Everything allowed: ");
            DoAs(new SimpleAgent());
            Console.WriteLine("Everything denied: ");
            DoAs(new DenyAllAgent());
            Console.WriteLine("Floors all denied: ");
            DoAs(new DenyFloorsAgent());
            Console.WriteLine("Everything filtered: ");
            DoAs(new FilterAllAgent());
            Console.WriteLine("Everything with even Id filtered: ");
            DoAs(new FilterEvenAgent());
            HasPermissionsValidator.HasPermissionsTo = new HashSet<int> { 1, 5, 6 };
            Console.WriteLine("Only persistables 1, 5, and 6 allowed: ");
            DoAs(new PermissionsAgent());
            Console.WriteLine("Everything with even Id filtered, buildings have name fuzzed: ");
            DoAs(new OddFuzzAgent());
            Console.WriteLine("Only Id 1 and 5: ");
            DoAs(new ExplicitAgent());

            /* Output:
                Everything allowed:
                1 Metrotech
                3 floors
                 - 5
                 - 6
                 - 7

                Everything denied:
                EX: DENIED

                Floors all denied:
                1 Metrotech
                EX: DENIED

                Everything filtered:
                Building not found

                Everything with even Id filtered:
                1 Metrotech
                2 floors
                 - 5
                 - 7

                Only persistables 1, 5, and 6 allowed:
                Building not found

                Everything with even Id filtered, buildings have name fuzzed:
                Fuzzed Name
                2 floors
                 - 5
                 - 7

                Only Id 1 and 5:
                1 Metrotech
                1 floors
                 - 5
             */
        }

        public static void DoAs(Agent a)
        {
            try
            {
                var uow = new UnitOfWork(a);
                var b = uow.Get<Building>(1).SingleOrDefault();
                if (b == null)
                {
                    Console.WriteLine("Building not found");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine(b.Name);
                Console.WriteLine(b.Floors.Count + " floors");
                foreach (var item in b.Floors)
                    Console.WriteLine(" - " + item.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
