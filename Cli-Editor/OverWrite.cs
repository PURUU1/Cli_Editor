//using System.IO;



//static class OverWrite
//{
//    public static void TextMain(string path, string fileName)
//    {

//        string? line;
//        string? str = null;
//        Console.Clear();

//        Program.DrawStatusBar(message: "Enter Mode: [A] Append: [Esc] Command: [Del] Exit : Ctrl+z and Enter to exit [Append] ", fileName: fileName);
//        path += "\\" + fileName;
//        if (File.Exists(path))
//        {

//            string fileContents = File.ReadAllText(path);
//            Console.Write(fileContents);

//        }

//        //
//        while ((line = Console.ReadLine()) != null)
//        {


//            str += Environment.NewLine;
//            str += line;
//        }
//        // string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
//        //string specificFolder = Path.Combine(appDataPath, "YourAppName");
//        //Directory.CreateDirectory(specificFolder); // Create the folder if it doesn't exist
//        //string filePath = Path.Combine(specificFolder, "data.txt");
//        //File.WriteAllText(filePath, "Hello, world!");

//        //using (StreamWriter writer = new StreamWriter($"C:\\Users\\acer\\source\\repos\\App\\Cli-Editor\\NewFolder\\{fileName}", true))
//        //{
//        //    writer.WriteLine(str);

//        //}

//        using (StreamWriter writer = new StreamWriter(path))
//        {
//            writer.WriteLine(str);

//        }
//        Console.Clear();
//    }





//}

using System.IO;
using Terminal.Gui;



static class OverWrite
{
    public static void TextMain(string path, string fileName)
    {


        path += "\\" + fileName;
        FileInfo fileInfo = new FileInfo(path);
        Program.DrawStatusBar(message: "Enter Mode: [A] Append: [Esc] Command: [Del] Exit : Ctrl+z and Enter to exit [Append] ", fileName: fileName);
        if (File.Exists(path) || !File.Exists(path))
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }

            
        
            string fileContents = File.ReadAllText(path);
            string myEditableContent = fileContents;
            Application.Init();
            var top = Application.Top;
            var blackBgWhiteText = Application.Driver.MakeAttribute(Color.White, Color.Black);
            Colors.Base = new ColorScheme()
            {
                Normal = blackBgWhiteText,
                Focus = Application.Driver.MakeAttribute(Color.White, Color.Black), // Selection color
                HotNormal = blackBgWhiteText,
                HotFocus = blackBgWhiteText
            };
            var win = new Window($"Cli-Editor ~ {fileName}")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

                Border = new Border { BorderStyle = BorderStyle.None },

            };
            Program.DrawStatusBar(message: "Enter Mode: [A] Append: [Esc] Command: [Del] Exit : Ctrl+z and Enter to exit [Append] ", fileName: fileName);
            var editor = new TextView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

                

            };

            var statusBar = new StatusBar(new StatusItem[] {
    new StatusItem(Key.Z | Key.CtrlMask, "~Ctrl+Z~ Save & Exit \n ~Ctrl+Esc~ to Exit Without Saving ", () => {
        File.WriteAllText(path, editor.Text.ToString());
        Application.RequestStop();
    })
});
            editor.KeyDown += (args) => {
                // Check for Ctrl + Z
                if (args.KeyEvent.Key == (Key.Z | Key.CtrlMask))
                {
                   
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.WriteLine(editor.Text.ToString());

                     }
                Application.RequestStop();
                    args.Handled = true;
                }
                else if (args.KeyEvent.Key == (Key.Esc))
                {
                    //File.WriteAllText(path, editor.Text.ToString());
                    Application.RequestStop();
                    args.Handled = true;
                }
            };
            win.Add(editor);
            top.Add(win, statusBar);
            //Console.Title = $"Press Ctrl + Z to SAVE and Exit ~";
            Application.Run();
            Application.Shutdown();


            Console.Clear();
        }



    }

}

