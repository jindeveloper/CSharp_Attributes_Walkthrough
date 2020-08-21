using CSharp_Attributes_Walkthrough.My_Custom_Attributes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace CSharp_Attributes_Walkthrough
{
    public class UnitTest_Csharp_Attributes
    {
        private readonly ITestOutputHelper _output;

        private readonly string assemblyFullName = "System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";

        public UnitTest_Csharp_Attributes(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void Test_GetAll_BuiltIn_Attributes()
        {
            var assembly = Assembly.Load(assemblyFullName);

            var attributes = assembly
                                .DefinedTypes
                                .Where(type =>
                                    type
                                    .IsSubclassOf(typeof(Attribute)));

            foreach (var attribute in attributes)
            {
                string attr = attribute
                                    .Name
                                    .Replace("Attribute", "");

                this._output
                    .WriteLine("Attribute: {0} and Usage: [{1}]"
                    , attribute.Name, attr);
            }
        }

        [Fact]
        public void Test_GetAll_AttributeTargets()
        {
            var targets = Enum.GetNames(typeof(AttributeTargets));

            foreach (var target in targets)
            {
                this._output.WriteLine($"AttributeTargets.{target}");
            }

        }

        /*
         *This test will read the Product-class at runtime to check for attributes. 
         *1. Check if [Serializable] has been read. 
         *2. Check if the product-class have two methods 
         *3. Check if each methods does have attributes. 
         *4. Check if the method GetProudctFullName is using the Obsolete attribute. 
         *5. Check if the method RunOnlyOnDebugMode is using the Conditional attribute.
         */
        [Fact]
        public void Test_Read_Attributes()
        {
            //get the Product-class
            var type = typeof(Product);

            //Get the attributes of the Product-class and we are expecting the [Serializable]
            var attribute = (SerializableAttribute)type.
                            GetCustomAttributes(typeof(SerializableAttribute), false).FirstOrDefault();

            Assert.NotNull(attribute);

            //Check if [Serializable] has been read.
            //Let's check if the type of the attribute is as expected
            Assert.IsType<SerializableAttribute>(attribute);

            //Let's get only those 2 methods that we have declared 
            //and ignore the special names (these are the auto-generated setter/getter)
            var methods = type.GetMethods(BindingFlags.Instance | 
                                          BindingFlags.Public | 
                                          BindingFlags.DeclaredOnly)
                               .Where(method => !method.IsSpecialName).ToArray();

            //Check if the product-class has two methods 
            //Let's check if the Product-class has two methods.
            Assert.True(methods.Length == 2);

            Assert.True(methods[0].Name == "GetProductFullName");
            Assert.True(methods[1].Name == "RunOnlyOnDebugMode");

            //Check if each methods does have attributes. 
            Assert.True(methods.All( method =>method.GetCustomAttributes(false).Length ==1));

            //Let's get the first method and its attribute. 
            var obsoleteAttribute = methods[0].GetCustomAttribute<ObsoleteAttribute>();

            // Check if the method GetProudctFullName is using the Obsolete attributes. 
            Assert.IsType<ObsoleteAttribute>(obsoleteAttribute);

            //Let's get the second method and its attribute. 
            var conditionalAttribute = methods[1].GetCustomAttribute<ConditionalAttribute>();

            //Check if the method RunOnlyOnDebugMode is using the Conditional attributes.
            Assert.IsType<ConditionalAttribute>(conditionalAttribute);
        }
    }
}
