/*
 * Problem: You need to represent part-whole hierarchies of objects, where individual objects and compositions of objects are treated uniformly. Think of a file system with files and folders.
 */
namespace ConsoleApp8.Composite.WithoutPattern
{
    public class File
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }

    public class Folder
    {
        public string Name { get; set; }
        public List<File> Files { get; set; }
        public List<Folder> Subfolders { get; set; }

        public int GetTotalSize()
        {
            int totalSize = 0;
            foreach (var file in Files)
            {
                totalSize += file.Size;
            }
            foreach (var folder in Subfolders)
            {
                totalSize += folder.GetTotalSize(); // Recursive call
            }
            return totalSize;
        }
    }
}

namespace ConsoleApp8.Composite.WithPattern
{
    // Component interface
    public interface IFileSystemComponent
    {
        string Name { get; }
        int GetSize();
    }

// Leaf (File)
    public class File : IFileSystemComponent
    {
        public string Name { get; }
        public int Size { get; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public int GetSize() => Size;
    }

// Composite (Folder)
    public class Folder : IFileSystemComponent
    {
        public string Name { get; }
        private List<IFileSystemComponent> _components = new List<IFileSystemComponent>();

        public Folder(string name)
        {
            Name = name;
        }

        public void Add(IFileSystemComponent component) => _components.Add(component);
        public void Remove(IFileSystemComponent component) => _components.Remove(component);

        public int GetSize() => _components.Sum(c => c.GetSize());
    }

// Usage
    class Consumer
    {
        void Run()
        {
            var root = new Folder("Root");
            root.Add(new File("file1.txt", 10));
            var subfolder = new Folder("Subfolder");
            subfolder.Add(new File("file2.txt", 20));
            root.Add(subfolder);

            Console.WriteLine(root.GetSize()); // Output: 30
        }
    }
}