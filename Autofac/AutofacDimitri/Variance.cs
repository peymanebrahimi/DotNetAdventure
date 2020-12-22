using System;

namespace AutofacDimitri
{
    class Variance
    {
        // Type T is declared covariant by using the out keyword.  
        public delegate T SampleGenericDelegate<out T>();

        public static void Run()
        {
            SampleGenericDelegate<String> dString = () => " ";

            // You can assign delegates to each other,  
            // because the type T is declared covariant.  
            SampleGenericDelegate<Object> dObject = dString;
        }


        public class First { }
        public class Second : First { }
        public delegate First SampleDelegate(Second a);
        public delegate R SampleGenericDelegate<A, R>(A a);


        // The return type is more derived.  
        public static Second ASecondRSecond(Second second)
        { return new Second(); }


        // The return type is more derived
        // and the argument type is less derived.  
        public static Second AFirstRSecond(First first)
        { return new Second(); }


        static void Main22(string[] args)
        {
            // Assigning a method with a more derived return type
            // and less derived argument type to a generic delegate.  
            // The implicit conversion is used.  
            SampleGenericDelegate<Second, First> dGenericConversion = AFirstRSecond;
        }
    }
}
