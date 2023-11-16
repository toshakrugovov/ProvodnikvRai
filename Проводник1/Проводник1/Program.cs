using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt7
{
    public class StrelMenu
    {
        public static int strelka(int min, int max)
        {
            int pos = 3;
            ConsoleKeyInfo key;
            do
            {
                Console.SetCursorPosition(0, pos);
                Console.WriteLine(">");

                key = Console.ReadKey();
                Console.SetCursorPosition(0, pos);
                Console.WriteLine(" ");

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (pos != min)
                        {
                            pos--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (pos != max)
                        {
                            pos++;
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            pos = -1;
                        }
                        return pos;

                }


            } while (key.Key != ConsoleKey.Enter);
            return pos;
        }
    }
    public static class DOP
    {
        public static void ShowDirectoryInfo(string a)
        {
            while (true)
            {
                Console.Clear();
                string[] papka = Directory.GetDirectories(a);
                string[] file1 = Directory.GetFiles(a);
                List<string> allFiles = new List<string>();

                for (int i = 0; i < papka.Length; i++)
                {
                    allFiles.Add(papka[i]);
                }
                for (int i = 0; i < file1.Length; i++)
                {
                    allFiles.Add(file1[i]);
                }


                foreach (string papa in papka)
                {
                    var createDate = Directory.GetCreationTime(papa);


                    Console.Write("  " + papa);

                    Console.SetCursorPosition(40, Console.CursorTop);
                    Console.WriteLine("      Дата создания: " + createDate);
                }
                foreach (string file in file1)
                {
                    var createDate = Directory.GetCreationTime(file);
                    Console.Write("  " + file);
                    Console.SetCursorPosition(40, Console.CursorTop);
                    Console.Write("      Дата создания: " + createDate + "\n");
                }


                int pos = StrelMenu.strelka(1, papka.Length + file1.Length);
                if (pos == -1)
                {
                    return;
                }
                else
                {
                    try
                    {
                        ShowDirectoryInfo(allFiles[pos]);

                    }
                    catch (IOException)
                    {
                        Process.Start(new ProcessStartInfo { FileName = allFiles[pos], UseShellExecute = true });
                    }
                }
            }
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            do
            {
                DriveInfo[] info = DriveInfo.GetDrives();

                Console.Clear();
                Console.SetCursorPosition(50, 0);
                Console.WriteLine("Этот компьютер\n");
                Console.SetCursorPosition(30, 1);
                Console.WriteLine("=====================================================");
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("Пространства: ");


                foreach (var drive in info)
                {
                    Console.WriteLine
                    ("   " + drive.Name + " " + drive.TotalSize / 1073741824 + "GB " + "Осталось: " + drive.AvailableFreeSpace / 1073741824 + "GB");
                }

                int pos = StrelMenu.strelka(3, info.Length + 2);
                if (pos == -1)
                {
                    return;
                }
                else
                {
                    DOP.ShowDirectoryInfo(info[pos - 3].RootDirectory.FullName);
                }
            } while (true);
        }
    }
}