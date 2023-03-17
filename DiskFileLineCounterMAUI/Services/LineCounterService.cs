using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskFileLineCounterMAUI.Services
{
    public class LineCounterService
    {
        public static async Task<int> CountLinesAsync(string fileLocation, IProgress<int> progress = null)
        {
            int activeLineCount = 0;
            try
            {
                using (var streamReader = new StreamReader(fileLocation))
                {
                    while (!streamReader.EndOfStream)
                    {
                        await streamReader.ReadLineAsync();
                        activeLineCount++;
                        if (progress != null)
                        {
                            progress.Report(activeLineCount);
                        }
                    }
                }
                return activeLineCount;
            }
            catch (ArgumentNullException anEx)
            {
                throw anEx;
            }
            catch (FileNotFoundException fnfEx)
            {
                throw fnfEx;
            }
            catch (DirectoryNotFoundException dnfEx)
            {
                throw dnfEx;
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
                return -1;
            }
        }
    }
}
