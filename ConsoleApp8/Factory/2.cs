/*
 * Problem: You need to create objects of different classes, but you don't want to specify the exact class in your client code.
 * 
 */

namespace ConsoleApp8.Factory
{
    public class Document
    {
        // ...
    }

    public class PdfDocument : Document { /* ... */ }
    public class WordDocument : Document { /* ... */ }
}
namespace ConsoleApp8.Factory.WithoutPattern
{
    public class DocumentManager
    {
        public Document CreateDocument(string type)
        {
            if (type == "pdf") return new PdfDocument();
            if (type == "word") return new WordDocument();
            throw new ArgumentException();
        }
    }
}

namespace ConsoleApp8.Factory.WithPattern
{
    public abstract class DocumentFactory
    {
        public abstract Document CreateDocument();
    }

    public class PdfDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument() => new PdfDocument();
    }

    public class WordDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument() => new WordDocument();
    }

    public class DocumentManager
    {
        public Document CreateDocument(DocumentFactory factory) => factory.CreateDocument();
    }

// Usage
    class Consumer
    {
        void Run()
        {
            var manager = new DocumentManager();
            Document doc = manager.CreateDocument(new PdfDocumentFactory());
        }
    }
}