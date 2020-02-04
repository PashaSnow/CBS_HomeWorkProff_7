using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class MyClass
    {
        [Obsolete("Actual method", false)]
        public void CallMethod()
        {
            Console.WriteLine("This is call method");
        }

        [Obsolete("Error method", true)]
        public void NonCompileMethod()
        {
            Console.WriteLine("Ups");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var instance = new MyClass();
            instance.CallMethod();
            // instance.NonCompileMethod();

            // get all attributes
            Type type = instance.GetType();
            MethodInfo[] meth = type.GetMethods();
            for (int i = 0; i < meth.Length; i++)
            {
                var method = type.GetMethod(meth[i].Name);
                var attribute = method.GetCustomAttribute(typeof(ObsoleteAttribute), false);
                Console.WriteLine((attribute as ObsoleteAttribute).Message);
            }
        }
    }
}
