using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {

        string sourceFolder = @"G:\InEnd";
        string targetFolder = @"D:\苹果备份\NEW";
        int count = 1;

        ProcessFilesRecursive(sourceFolder, targetFolder, ref count);

        Console.WriteLine("OK");
        Console.ReadLine();


    }


    static void ProcessFilesRecursive(string folderPath, string targetFolder, ref int count)
    {
        string[] movFiles = Directory.GetFiles(folderPath, "*.mov");
        //string[] jpgFiles = Directory.GetFiles(folderPath, "*.jpg");
        //string[] heicjpgFiles = Directory.GetFiles(folderPath, "*.heic");

        foreach (string movFile in movFiles)
        {
            string fileName = Path.GetFileName(movFile);
            string randomString = GenerateRandomString(10);
            string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".mov";
            string targetPath = Path.Combine(targetFolder, targetFileName);
            File.Copy(movFile, targetPath);

            Console.WriteLine("源文件：" + movFile);
            Console.WriteLine("目标文件：" + targetPath);
            Console.WriteLine("计数：" + count);
            count++;
        }

        //foreach (string jpgFile in jpgFiles)
        //{
        //    string fileName = Path.GetFileName(jpgFile);
        //    string randomString = GenerateRandomString(10);
        //    string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".jpg";
        //    string targetPath = Path.Combine(targetFolder, targetFileName);
        //    File.Copy(movFile, targetPath);

        //    Console.WriteLine("源文件：" + jpgFile);
        //    Console.WriteLine("目标文件：" + targetPath);
        //    Console.WriteLine("计数：" + count);
        //    count++;
        //}

        //foreach (string heicFile in heicjpgFiles)
        //{
        //    string fileName = Path.GetFileName(heicFile);
        //    string randomString = GenerateRandomString(10);
        //    string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + randomString + ".jpg";
        //    string targetPath = Path.Combine(targetFolder, targetFileName);
        //    File.Copy(movFile, targetPath);

        //    Console.WriteLine("源文件：" + heicFile);
        //    Console.WriteLine("目标文件：" + targetPath);
        //    Console.WriteLine("计数：" + count);
        //    count++;
        //}

        string[] subFolders = Directory.GetDirectories(folderPath);
        foreach (string subFolder in subFolders)
        {
            ProcessFilesRecursive(subFolder, targetFolder, ref count);
        }
    }
    private static string GenerateRandomString(int length)
    {
        Guid guid = Guid.NewGuid();
        string randomString = guid.ToString("N").Substring(0, length);
        return randomString;
    }
}

