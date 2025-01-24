/*
 * Problem: An object's behavior depends on its internal state, and you want to change its behavior at runtime based on state transitions.
 */
namespace ConsoleApp8.State.WithoutPattern
{
    public class Document
    {
        public string State { get; set; } // "Draft", "Submitted", "Approved"

        public void Publish()
        {
            if (State == "Draft")
            {
                // Publish logic
                State = "Submitted";
            }
            else if (State == "Submitted")
            {
                // Error: Cannot publish again
            } // ... messy state checks
        }
    }
}

namespace ConsoleApp8.State.WithPattern
{
    // State interface
    public interface IDocumentState
    {
        void Publish(Document document);
    }

// Concrete states
    public class DraftState : IDocumentState
    {
        public void Publish(Document document)
        {
            Console.WriteLine("Publishing document.");
            document.State = new SubmittedState();
        }
    }

    public class SubmittedState : IDocumentState
    {
        public void Publish(Document document)
        {
            Console.WriteLine("Document already submitted.");
        }
    }

    public class Document
    {
        public IDocumentState State { get; set; }

        public Document()
        {
            State = new DraftState(); // Initial state
        }

        public void Publish()
        {
            State.Publish(this);
        }
    }

// Usage
    class Consumer
    {
        void Run()
        {
            var doc = new Document();
            doc.Publish(); // Publishes
            doc.Publish(); // Already submitted
        }
    }
}