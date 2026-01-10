using System.IO;



static class Delete
{
   public  static void DltFile(string path,string fileName)
    {
        ConsoleKeyInfo keyInfo;
        path += "\\" + fileName;
        Program.DrawStatusBar(message: $"Do you want to delete {fileName} ? [ Y - yes ] [ N - no ]", fileName: fileName);
        if (File.Exists(path))
        {
        keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key.Equals(ConsoleKey.Y) )
            {

                if (File.Exists(path))
                {
                    File.Delete(path);
                    Program.DrawStatusBar(message: $"File deleted successfully");
                    return;
                    
                }
            }
            else if (keyInfo.KeyChar.Equals(ConsoleKey.Y))
            {
                Program.DrawStatusBar(message: $"Deleting Abort..");
                return;
            }

            
        }
        else
        {
            Console.WriteLine("File dosent Exist..");
            return;
        }
        
    }
}



