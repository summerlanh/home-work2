using System;

namespace SchoolEF
{
    class Program
    {
        static void Main()
        {
            // Database accessor. This opens the database automatically
            var school = new SchoolEntities();


            foreach (var user in school.Users)
            {
                Console.WriteLine(user.UserName + "\n");

                foreach (var classMaster in user.ClassMasters)
                {
                    Console.WriteLine("ClassId: {0}\nClassName: {1}\nClassDescription: {2}\nClassPrice: {3}\n",
                        classMaster.ClassId, classMaster.ClassName, classMaster.ClassDescription, classMaster.ClassPrice);
                }
              
            }
            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
