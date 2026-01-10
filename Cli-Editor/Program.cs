using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static async Task KeyReader(string pathh, string fileName)
    {
        try
        {
        menu:
            #region Variables

            var path = pathh;
            // acceptable Extension
            var Ext = new string[] { "html", "css", "txt", "js", "cpp" , "csv", "xml", "json", "log", "cs" };
            
            // get all files in Given path
            //var Files = Directory.GetFiles(path);
            await Loader(500);
            Console.Clear();
            
            
            DrawStatusBar(message: "Enter Mode: [O] OverWrite: [A] Rewrite  [Esc] Command: [Del] Exit: ", fileName: fileName );
            #endregion
            #region ValidateFileName


            if (!fileName.Equals("") && fileName.Contains("."))
                {
                    #region checkExtension
                    //check for extention
                    var isRightExt = false;
                    var fEx = Path.GetExtension(fileName).Substring(1);
                    foreach (var item in Ext)
                    {
                        if (fEx.Equals(item))
                        {
                            isRightExt = true;
                        }
                    }
                    if (!isRightExt)
                    {
                    #region WrongExtension
                    Console.ForegroundColor = ConsoleColor.Red;
                        DrawStatusBar("invalid Extension...");
                        await Loader(1000);
                        DrawStatusBar("Exiting...");
                        await Loader(500);
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    #endregion
                }
            }
            
            else
            {
                #region emptyFileOrMissingExtension
                Console.ForegroundColor = ConsoleColor.Red;
                DrawStatusBar("Missing Extention or File Name...");
                await Loader(1000);
                DrawStatusBar("Exiting...");
                await Loader(500);
                Console.ForegroundColor = ConsoleColor.White;
                return;
                #endregion
            }
            #endregion
           
            #endregion
                #region keysPreesFunc
            ConsoleKeyInfo keyInfo;
            
            while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        // Console.ReadKey(true);
                        keyInfo = Console.ReadKey(intercept: true);

                        if (keyInfo.Key.Equals(ConsoleKey.A))
                        {
                        #region OnKeyPressA
                        Console.Clear();
                            await Loader(500);
                            DrawStatusBar();
                            Console.WriteLine("Switched to Append Mode....");
                            Console.WriteLine("Write....");
                            DrawStatusBar(message: "Enter Mode: [O] OverWrite: [A] Rewrite [Esc] Command: [Del] Exit: ");
                            NewFile.TextMain(path, fileName);
                            goto menu;
                        #endregion
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.O))
                        {
                        #region OnKeyPressO
                        Console.Clear();
                            await Loader(500);
                            DrawStatusBar();
                            Console.WriteLine("Switched to OverWrite Mode....");
                            Console.WriteLine("Write....");
                            OverWrite.TextMain(path, fileName);
                            goto menu;
                        #endregion
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.Escape))
                        {
                        #region OnKeyPressEsc
                        await Loader(500);
                            Console.WriteLine("Switched to Command Mode");
                        Console.Clear();
                            DrawStatusBar(message: "Enter Mode: [D] Delete File [Del] Exit: ");

                            keyInfo = Console.ReadKey(intercept: true);

                            if (keyInfo.Key.Equals(ConsoleKey.D))
                            {
                                Delete.DltFile(path, fileName);
                                await Loader(500);
                            //goto menu;
                            return;
                            }
                            else if (keyInfo.Key.Equals(ConsoleKey.Delete))
                            {
                                await Loader(500);
                                goto menu;
                            }
                        #endregion
                    }
                    else if (keyInfo.Key.Equals(ConsoleKey.Delete))
                        {
                        #region OnKeyPressDlt
                        await Loader(500);
                        Console.Clear();
                        return;
                        #endregion
                    }
                }
                }
                    #endregion
                //
            
        }
        #region errorHandling
        catch (UnauthorizedAccessException A)
        {

            await Loader(500);
            Console.ForegroundColor = ConsoleColor.Red;
            DrawStatusBar("Error : ", A.Message);
            await Loader(500);
            DrawStatusBar("Exiting...");
            await Loader(500);
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }
        catch (Exception e)
        {
            await Loader(500);
            Console.ForegroundColor = ConsoleColor.Red;
            DrawStatusBar("Error : ", e.Message);
            await Loader(500);
            Console.ForegroundColor = ConsoleColor.White;
            await KeyReader(pathh, fileName);
        }
        #endregion
    }

    static async Task Main(string[] args)
    {


#if DEBUG

         await KeyReader(pathh: Environment.CurrentDirectory, "a.txt");


#else
        #region Relese


        try
        {
            #region -n filename
            if (args[0].Equals("-n") && !args[1].Equals(""))
            {
                Console.Title = $"ClI-Editor ~  {args[1]}";
                await KeyReader(pathh: Environment.CurrentDirectory, args[1]);

            }
            #endregion
            #region -d filename
            else if (args[0].Equals("-d") && !args[1].Equals(""))
            {
                DrawStatusBar(message: $"Do you want to delete {args[1]} ? [ Y - yes ] [ N - no ]", fileName: args[1]);
                string path = $"{Environment.CurrentDirectory}\\{args[1]}";
                if (File.Exists(path))
                {


                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        Program.DrawStatusBar(message: $"File deleted successfully");
                        return;

                    }
                }
                else
                {
                    Program.DrawStatusBar(message: $"Deleting Abort..");
                    return;
                }

            }
            #endregion
            #region -h
            else if (args[0].Equals("-h"))
            {

                await Loader(500);
                Program.DrawStatusBar(message: $"Cli - Editor : Author : Puneet");
        Console.WriteLine("======================================");
        Console.WriteLine("      Welcome to C# CLI Editor        ");
        Console.WriteLine("======================================");
                Console.WriteLine("Cli-Editor.exe [-n] [filename] ");
                Console.WriteLine("{Create Or Edit Existing Files}");
                Console.WriteLine("Cli-Editor.exe [-d] [filename] ");
                Console.WriteLine("{Delete Existing Files}");
                Console.WriteLine("Cli-Editor.exe [-r] [filename] [rename]");
                Console.WriteLine("{Delete Existing Files}");
                Console.WriteLine("Cli-Editor.exe [-h] ");
                Console.WriteLine("{Get Help}");
                //await Loader(500);
                Console.ReadKey();
                Console.Clear();
                return;

            }
            #endregion
            #region -r filename filenameToRename
            else if (args[0].Equals("-r") && !args[1].Equals("") && !args[2].Equals(""))
            {
                //DrawStatusBar(message: $"Do you want to rename {args[1]} to {args[2]} ? [ Y - yes ] [ N - no ]", fileName: args[1]);
                string path = $"{Environment.CurrentDirectory}";
                string source = $"{path}\\{args[1]}";
                string destination = $"{path}\\{args[2]}";
                if (args[1].Substring(args[1].IndexOf('.')).Equals(args[2].Substring(args[2].IndexOf('.'))))
                {
                    if (File.Exists(source))
                    {

                        if (File.Exists(destination))
                        {
                            File.Delete(destination);
                        }

                        File.Move(source, destination);
                    }

                }
                else
                {
                    Console.WriteLine("Wrong Extension");
                }

            }
            #endregion
             #region filename 
            else if (!args[0].Equals("") && args[0].Contains("."))
            {
                var Ext = new string[] { "html", "css", "txt", "js", "cpp" , "csv", "xml", "json", "log", "cs" };
                var fileName = args[0];
                if (!fileName.Equals("") && fileName.Contains("."))
                {
                    #region checkExtension
                    //check for extention
                    var isRightExt = false;
                    var fEx = Path.GetExtension(fileName).Substring(1);
                    foreach (var item in Ext)
                    {
                        if (fEx.Equals(item))
                        {
                            isRightExt = true;
                        }
                    }
                    if (!isRightExt)
                    {
                        #region WrongExtension
                        Console.ForegroundColor = ConsoleColor.Red;
                        DrawStatusBar("invalid Extension...");
                        await Loader(1000);
                        DrawStatusBar("Exiting...");
                        await Loader(500);
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                        #endregion
                    }
                }
                else
                {
                    #region emptyFileOrMissingExtension
                    Console.ForegroundColor = ConsoleColor.Red;
                    DrawStatusBar("Missing Extention or File Name...");
                    await Loader(1000);
                    DrawStatusBar("Exiting...");
                    await Loader(500);
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                    #endregion
                }
                NewFile.TextMain(Environment.CurrentDirectory, fileName);
                //await  KeyReader(Environment.CurrentDirectory, fileName);
            }
            #endregion


            #endregion
        }
        #region no Args
        catch (IndexOutOfRangeException e)
        {
           
           
                await Loader(500);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error No File Given" , e.Message);
                Console.WriteLine("Use Cli-Editor.exe [-h]");
                Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
#endregion
#endif
    }
    public static void DrawStatusBar(string fileName = " ", string message = "" , long fileSize = 0)
    {
        var row = Console.WindowHeight - 1; // Row for status bar

        // fileName = fileName.Equals(" ") ? " " : $" file Name : {fileName} ";

        Console.SetCursorPosition(0, row); // Move cursor to the bottom line
        // Clear the whole line
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, row);
        Console.WriteLine(fileName);
        Console.Write(message , "/t " , fileSize);
        Console.SetCursorPosition(0, 0);
    }

    public static async Task Loader(int delay = 0, string message = "")
    {
        Console.WriteLine("|");
        await Task.Delay(delay);
        Console.Clear();
        Console.WriteLine("/");
        await Task.Delay(delay);
        Console.Clear();
        Console.WriteLine("\\");
        await Task.Delay(delay);
        Console.Clear();
        Console.WriteLine("|");
        await Task.Delay(delay);
        Console.Clear();
    }
}