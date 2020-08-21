using System;
using System.Linq;

namespace CSharp_Attributes_Walkthrough.My_Custom_Attributes
{
    [Alias("Filipino_Customers", ConsoleColor.Yellow)]
    public class Customer
    {
        [Alias("Fname", ConsoleColor.White, AlternativeName = "Customer_FirstName")]
        public string Firstname { get; set; }

        [Alias("Lname", ConsoleColor.White, AlternativeName = "Customer_LastName")]
        public string LastName { get; set; }

        public override string ToString()
        {
            //get the current running instance.
            Type instanceType = this.GetType(); 

            //get the namespace of the running instance.
            string current_namespace = (instanceType.Namespace) ?? "";

            //get the alias.
            string alias = (this.GetType().GetCustomAttributes(false).FirstOrDefault() as AliasAttribute)?.Alias;

            return $"{current_namespace}.{alias}";
        }
    }
}
