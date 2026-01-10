using System.IO;
using Terminal.Gui;



static class NewFile
{
    public static void TextMain(string path, string fileName)
    {


        path += "\\" + fileName;
        //FileInfo fileInfo = new FileInfo(path);
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
            //Program.DrawStatusBar(message: "Enter Mode: [A] Append: [Esc] Command: [Del] Exit : Ctrl+z and Enter to exit [Append] ", fileName: fileName);
            var editor = new TextView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),

                Text = myEditableContent

            };
            var Content = editor.Text.ToArray();
            int l = 0;
            foreach(var item in Content)
            {
                if (!item.Equals(13) && !item.Equals(10) && !item.Equals(32))
                {
                    l++;
                }
                //Content.Contains<uint>(97)) { 

                
            }

            int? c = editor.Text.ToString().Split('\n').Length;

            FileInfo fileInfo = new FileInfo(path);
            long fileSizeInBytes = fileInfo.Length;
            double fileSizeInkb = fileSizeInBytes / 1024;
            double fileSizeInMB = (double)fileSizeInBytes / (1024 * 1024);
            //double fileSizeInGB = (double)fileSizeInBytes / (1024 * 1024 * 1024);


            var statusBar = new StatusBar(new StatusItem[] {
    new     StatusItem(Key.A | Key.CtrlMask, "~Ctrl+A~ Save ", () => {
            File.WriteAllText(path, editor.Text.ToString());
            //Application.RequestStop();
    }),
    new     StatusItem(Key.Q | Key.CtrlMask, "~Ctrl+Q~ Save & Exit", () => {
            File.WriteAllText(path, editor.Text.ToString());
            Application.RequestStop();
    }),
    new StatusItem(Key.Esc, "~Esc~ Exit Without Saving", () => {
        Application.RequestStop();
    }),
    new StatusItem(Key.Tab, $"~[WC:{l}]~ ~[C:{c}]~", () => {
       
        //Application.RequestStop();
    }),
    new StatusItem(Key.Tab, $"~[Size: {fileSizeInkb:F2} KB]~", () => {
       
        //Application.RequestStop();
    })
});
            Console.Title = $"Cli-Editor ~ Editing: {fileName} ~ ";
            int Lpos = Console.CursorLeft;
            int Tpos = Console.CursorTop;
            
            editor.KeyDown += (args) =>
            {

                
                if (args.KeyEvent.Key == (Key.Q | Key.CtrlMask))
                {
                    l = editor.Text.Length;
                    File.WriteAllText(path, editor.Text.ToString());
                    Application.RequestStop();
                    args.Handled = true;
                }
                else if (args.KeyEvent.Key == (Key.A | Key.CtrlMask))
                {
                    l = editor.Text.Length;
                    File.WriteAllText(path, editor.Text.ToString());
                    //Console.SetCursorPosition(Lpos, Tpos);
                    args.Handled = true;
                }
                else if (args.KeyEvent.Key == (Key.Esc))
                {
                    //File.WriteAllText(path, editor.Text.ToString());
                    Application.RequestStop();
                    args.Handled = true;
                }
            };
            Console.Title = $"Cli-Editor ~ Editing: {fileName} ~ ";
            editor.WordWrap = true;
                //Console.SetCursorPosition(Lpos, Tpos);
            win.Add(editor);
            top.Add(win, statusBar);
            //Console.Title = $"Press Ctrl + Z to SAVE and Exit ~";
            Application.Run();
            Application.Shutdown();


            Console.Clear();
        }

    }


    }



