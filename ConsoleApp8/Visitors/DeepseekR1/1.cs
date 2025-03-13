/*
 * When to Use Visitor Pattern
When you have a stable object structure but frequently changing operations.
When you want to avoid polluting classes with unrelated operations.
When you need to perform operations across a hierarchy of objects.
 */

namespace ConsoleApp1Visitor.DeepseekR1
{
    public interface IDocumentVisitor
    {
        void Visit(WordDocument doc);
        void Visit(ExcelDocument doc);
        void Visit(PdfDocument doc);
    }

    public class PrintDocumentVisitor : IDocumentVisitor
    {
        public void Visit(WordDocument doc)
        {
            // Visit Word document
            Console.WriteLine("Printing Word document");
        }

        public void Visit(ExcelDocument doc)
        {
            Console.WriteLine("Printing Excel document");
        }

        public void Visit(PdfDocument doc)
        {
            Console.WriteLine("Printing PDF document");
        }
    }
    
    public class ExportDocumentVisitor : IDocumentVisitor
    {
        public void Visit(WordDocument doc)
        {
            // Export Word document
            Console.WriteLine("Exporting Word document");
        }

        public void Visit(ExcelDocument doc)
        {
            Console.WriteLine("Exporting Excel document");
        }

        public void Visit(PdfDocument doc)
        {
            Console.WriteLine("Exporting PDF document");
        }
    }
    public interface IDocument
    {
        public void Accept(IDocumentVisitor visitor);
    }
    public class WordDocument: IDocument
    {
        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
        
    }
    public class ExcelDocument: IDocument
    {
        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
        
    }
    public class PdfDocument: IDocument
    {
        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }


    class Consumer
    {
        void Run()
        {
            List<IDocument> documents = new List<IDocument>
            {
                new WordDocument(),
                new ExcelDocument(),
                new PdfDocument()
            };
            
            IDocumentVisitor printVisitor = new PrintDocumentVisitor();
            IDocumentVisitor exportVisitor = new ExportDocumentVisitor();
            
            foreach (var doc in documents)
            {
                doc.Accept(printVisitor);
                doc.Accept(exportVisitor);
            }
        }
    }
}