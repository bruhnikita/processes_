using System.Diagnostics;

class ProcessCheker
{
    static void Main(string[] args)
    {
        Process[] allProcess = Process.GetProcesses();

        while (true)
        {
            Console.WriteLine("Введите действие:\n1 - убить процесс\n2 - запустить процесс\n3 - вывести все процессы\n4 - выйти");
            int key = int.Parse(Console.ReadLine());

            switch (key)
            {
                case 1:
                    Console.WriteLine("Введите id процесса: ");
                    int id = int.Parse(Console.ReadLine());

                    KillProcess(id);

                    break;

                case 2:
                    Console.WriteLine("Введите имя программы (\"notepad.exe\")");
                    string programName = Console.ReadLine();

                    StartProgram(programName);

                    break;

                case 3:
                    PrintAllProcesses(allProcess);

                    break;

                case 4:
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Попробуй снова.");
                    break;
            }
        }

    }

    static void KillProcess(int id)
    {
        Process processForKill = Process.GetProcessById(id);

        processForKill.Kill();

        Logger.Logging("Процесс убит.");
    }

    static void StartProgram(string programName)
    {
        Process.Start(programName);

        Logger.Logging("Процесс запущен.");
    }

    static void PrintAllProcesses(Process[] processes)
    {
        foreach (Process process in processes)
        {
            Console.WriteLine($"Name: {process.ProcessName}  ID: {process.Id}  memory footprint: {process.PagedMemorySize64 / 1024 / 1024} MB status: {process.Responding}");

            Logger.Logging("Выведены все запущенные процессы.");
        }
    }
}

class Logger
{
    public static void Logging(string message)
    {
        string logPath = "process_log.txt";

        using (StreamWriter sw = new StreamWriter(logPath, true))
        {
            sw.WriteLine($"{DateTime.Now} {message}");
        } 
    }
}