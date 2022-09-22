using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethekwini_ChatBot_NetworkPing.Services
{
    public class PingTcpNetworkService
    {
        public async void PingNetWork(string network)
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            var path = Directory.GetCurrentDirectory() + "\\Scripts\\tcping64.exe";
            pProcess.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Scripts\\tcping64.exe";

            pProcess.StartInfo.Arguments = network;

            pProcess.StartInfo.UseShellExecute = false;

            //Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;

            pProcess.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\Scripts";
            pProcess.Start();

            string strOutput = pProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(strOutput);

            pProcess.WaitForExit();
            await LogResults(strOutput);
        }

        public async Task LogResults(string line)
        {
            string fileName = Directory.GetCurrentDirectory() + "\\Logfiles\\LogResults.txt";
            if (!File.Exists(fileName))
            {
                using (File.Create(Directory.GetCurrentDirectory() + "\\Logfiles\\LogResults.txt"));
            }
            var lineCount = File.ReadLines(Directory.GetCurrentDirectory() + "\\Logfiles\\LogResults.txt").Count();
            if (lineCount < 5000)
            {
                using StreamWriter file = new(Directory.GetCurrentDirectory() + "\\Logfiles\\LogResults.txt", append: true);
                await file.WriteLineAsync("Ping Time: " + Convert.ToString(DateTime.Now));
                await file.WriteLineAsync(line);
            }
            else
            {
                using StreamWriter file = new(Directory.GetCurrentDirectory() + "\\Logfiles\\LogResults2.txt", append: true);
                await file.WriteLineAsync("Ping Time: "+Convert.ToString(DateTime.Now));
                await file.WriteLineAsync(line);
            }
        
        }
    }
}
