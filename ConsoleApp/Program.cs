//Создать директории c:\Otus\TestDir1 и c:\Otus\TestDir2 с помощью класса DirectoryInfo.
//В каждой директории создать несколько файлов File1...File10 с помощью класса File.
//В каждый файл записать его имя в кодировке UTF8. Учесть, что файл может быть удален, либо отсутствовать права на запись.
//Каждый файл дополнить текущей датой (значение DateTime.Now) любыми способами: синхронно и\или асинхронно.
//Прочитать все файлы и вывести на консоль: имя_файла: текст + дополнение.

using System.Text;

var directory1 = new DirectoryInfo("C:\\Otus\\TestDir1");
var directory2 = new DirectoryInfo("C:\\Otus\\TestDir2");

directory1.Create();
directory2.Create();

List<FileStream> files = new List<FileStream>();
for (int i = 0; i < 10; i++)
{
    files.Add(File.Create($"C:\\Otus\\TestDir1\\File{i}"));
    files.Add(File.Create($"C:\\Otus\\TestDir2\\File{i}"));
}

foreach (var fstream in files)
{
    byte[] buffer = Encoding.UTF8.GetBytes(Path.GetFileName(fstream.Name + " + "));
    if (!fstream.CanWrite)
        throw new ArgumentException($"Не могу записать в {fstream.Name}");
    fstream.Write(buffer, 0, buffer.Length);
}

foreach (var fstream in files)
{
    byte[] buffer = Encoding.UTF8.GetBytes(Path.GetFileName(DateTime.Now.ToString()));
    fstream.Write(buffer, 0, buffer.Length);
}

Console.WriteLine("Текст из всех файлов:");
foreach (var fstream in files)
{
    fstream.Seek(0, SeekOrigin.Begin);
    byte[] buffer = new byte[fstream.Length];
    fstream.Read(buffer, 0, buffer.Length);
    string textFromFile = Encoding.Default.GetString(buffer);
    Console.WriteLine(textFromFile);
    fstream.Close();
}