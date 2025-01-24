/*
 * Problem: You have an algorithm with a fixed structure, but some steps within the algorithm can vary.
 */
namespace ConsoleApp8.TemplateMethod.WithoutPattern
{
    class SpecificProcessorA { }
    class SpecificProcessorB { }
    
    public class DataProcessor
    {
        public void ProcessData(string data)
        {
            // Step 1: Validate data (common to all processors)
            if (!ValidateData(data)) 
            {
                throw new InvalidDataException();
            }

            // Step 2: Specific processing (varies)
            if (this is SpecificProcessorA) 
            {
                // Process data specifically for A
            } 
            else if (this is SpecificProcessorB) 
            {
                // Process data specifically for B
            }

            // Step 3: Generate report (common to all processors)
            GenerateReport(); 
        }

        private bool ValidateData(string data) { /* ... */ return true;}
        private void GenerateReport() { /* ... */ }
    }
}

namespace ConsoleApp8.TemplateMethod.WithPatten
{
    public abstract class DataProcessor
    {
        public void ProcessData(string data)
        {
            if (!ValidateData(data)) 
            {
                throw new InvalidDataException();
            }

            ProcessSpecificData(data); // Abstract method

            GenerateReport(); 
        }

        protected abstract void ProcessSpecificData(string data); 

        private bool ValidateData(string data) { /* ... */ return true;}
        private void GenerateReport() { /* ... */ }
    }

    public class SpecificProcessorA : DataProcessor
    {
        protected override void ProcessSpecificData(string data) 
        { 
            // Process data specifically for A 
        }
    }

    public class SpecificProcessorB : DataProcessor
    {
        protected override void ProcessSpecificData(string data) 
        { 
            // Process data specifically for B 
        }
    }
}