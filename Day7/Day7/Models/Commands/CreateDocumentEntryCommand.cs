using Day7.Interfaces;

namespace Day7.Models.Commands
{
    public class CreateDocumentEntryCommand : ICommand
    {
        private string FileName { get; }
        
        private int Size { get; }

        public CreateDocumentEntryCommand(string fileName, int size)
        {
            FileName = fileName;
            Size = size;
        }

        public void Execute(FileSystemExplorer fileSystemExplorer)
        {
            Document document = new Document(Size, FileName);
            
            fileSystemExplorer.CurrentDirectory.AddEntry(document);
        }
    }
}