using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        private static string currentDirectory;
        private static List<string> deletedFiles;

        static void Main(string[] args)
        {
            currentDirectory = Directory.GetCurrentDirectory();
            deletedFiles = new List<string>();

            Console.WriteLine("Вы уверены, что хотите удалить все пустые файлы с расширением '.md'? (Y/N)");
            string userChoice = Console.ReadLine();

            if (userChoice.ToLower() == "y")
            {
                DeleteEmptyFiles(currentDirectory);
                Console.WriteLine("Удаление завершено.\n\nУдаленные файлы:\n" + string.Join("\n", deletedFiles));
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        private static void DeleteEmptyFiles(string directory)
        {
            foreach (string file in Directory.GetFiles(directory, "*.md"))
            {
                if (IsFileEmpty(file))
                {
                    File.Delete(file);
                    deletedFiles.Add(file);
                }
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                DeleteEmptyFiles(subDirectory);

                if (Directory.GetFiles(subDirectory).Length == 0 && Directory.GetDirectories(subDirectory).Length == 0)
                {
                    Directory.Delete(subDirectory);
                }
            }
        }

        private static bool IsFileEmpty(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length == 0;
        }
    }
}