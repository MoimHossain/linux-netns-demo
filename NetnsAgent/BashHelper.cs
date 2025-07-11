

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NetnsAgent
{
    public static class BashHelper
    {
        public static string Bash(this string cmd)
        {
            try
            {
                Console.WriteLine($"[LOG] Executing bash command: {cmd}");
                var escapedArgs = cmd.Replace("\"", "\\\"");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{escapedArgs}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                var result = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    var errorMessage = $"Command failed with exit code {process.ExitCode}. Error: {error}";
                    Console.WriteLine($"[ERROR] {errorMessage}");
                    throw new InvalidOperationException(errorMessage);
                }

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"[WARNING] Command succeeded but produced stderr output: {error}");
                }

                Console.WriteLine($"[LOG] Command executed successfully, output length: {result.Length} characters");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception occurred while executing bash command '{cmd}': {ex.Message}");
                throw;
            }
        }
    }
}
