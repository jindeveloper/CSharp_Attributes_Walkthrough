using System;
using System.Diagnostics;

namespace CSharp_Attributes_Walkthrough.My_Custom_Attributes
{
    [Serializable]
    public class Product
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [Obsolete("This method is already obselete. Use the ProductFullName instead.")]
        public string GetProductFullName()
        {
            return $"{this.Name} {this.Code}";
        }

        [Conditional("DEBUG")]
        public void RunOnlyOnDebugMode()
        {

        }
    }
}
