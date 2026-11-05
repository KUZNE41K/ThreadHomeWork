using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadHomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() =>
            {
                Console.WriteLine("[Поток 1] Запуск потока.");

                Console.WriteLine("[Поток 1] Запуск Notepad.");

                Process notepad = Process.Start("notepad.exe");

                Console.WriteLine("[Поток 1] Ожидание 3 секунды...");

                Thread.Sleep(3000);

                Console.WriteLine("[Поток 1] Закрытие Notepad.");

                notepad.Kill();
                notepad.WaitForExit();

                Console.WriteLine("[Поток 1] Поток завершён.");
            });

            // Создаём второй поток
            Thread thread2 = new Thread(() =>
            {
                Console.WriteLine("[Поток 2] Запуск потока.");

                Console.WriteLine("[Поток 2] Запуск cmd.exe.");

                Process cmd = new Process();

                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.Arguments = "/c dir";

                // Перенаправляем вывод команды в нашу консоль
                cmd.StartInfo.UseShellExecute = false; // напрямую указываем, что не используем оболочку PowerShell
                cmd.StartInfo.RedirectStandardOutput = true; // перенаправляем стандартный вывод
                cmd.StartInfo.CreateNoWindow = true; // не создаём новое окно для cmd

                cmd.Start();

                Console.WriteLine("[Поток 2] Результат команды dir:");

                string result = cmd.StandardOutput.ReadToEnd();

                Console.WriteLine(result);

                cmd.WaitForExit();

                Console.WriteLine("[Поток 2] Поток завершён.");
            });

            // Запускаем оба потока
            thread1.Start();
            thread2.Start();

            // Ожидаем завершения обоих потоков
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Главный поток завершён.");
        }
    }
}
