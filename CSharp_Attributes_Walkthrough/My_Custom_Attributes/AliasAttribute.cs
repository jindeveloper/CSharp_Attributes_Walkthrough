using System;

namespace CSharp_Attributes_Walkthrough.My_Custom_Attributes
{
    /// <summary>
    /// This class is a custom attribute class. 
    /// Moreover, it is using the AttributeUsage attribute to annotate
    /// that this attribute is applicable only to class, struct and property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        /// <summary>
        /// These parameters will become mandatory once have you decided to use this attribute.
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="color"></param>
        public AliasAttribute(string alias, ConsoleColor color)
        {
            this.Alias = alias;
            this.Color = color;
        }

        #region Positional-Parameters
        public string Alias { get; private set; }
        public ConsoleColor Color { get; private set; }
        #endregion 

        //Added an optional-parameter
        public string AlternativeName { get; set; }
    }
}


