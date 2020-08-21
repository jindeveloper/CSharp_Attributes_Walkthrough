using CSharp_Attributes_Walkthrough.My_Custom_Attributes;
using System;

namespace Implementing_Csharp_Attributes_101
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer { Firstname = "Jin Vincent" , LastName = "Necesario" };
          
            var aliasAttributeType = customer.GetType();

            var attribute = aliasAttributeType.GetCustomAttributes(typeof(AliasAttribute), false);

            Console.ForegroundColor = ((AliasAttribute)attribute[0]).Color;

            Console.WriteLine(customer.ToString());

            Console.ReadLine();
        }
    }
}
