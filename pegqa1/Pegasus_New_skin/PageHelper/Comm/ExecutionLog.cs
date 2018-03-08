using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.PageHelper.Comm
{
    class ExecutionLog
    {
        public void Log(String filename, String text)
        {
            try
            {
                // Create file 
                string Currentpath = Directory.GetCurrentDirectory();
                String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
                String a = ab[0];
                String fPath = a + "ExecutionLog\\" + filename + ".txt";
                if (!File.Exists(fPath))
                {
                    FileStream aFile = new FileStream(fPath, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile);
                    sw.WriteLine(text);

                    sw.Close();
                    aFile.Close();
                }
                else if (File.Exists(fPath))
                {
                    FileStream aFile1 = new FileStream(fPath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(aFile1);
                    sw.WriteLine(text);

                    sw.Close();
                    aFile1.Close();
                }
            }
            catch (Exception e)
            {
                //Catch exception if any
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void Count(String filename, String text)
        {
            try
            {
                // Create file 
                string Currentpath = Directory.GetCurrentDirectory();
                String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
                String a = ab[0];
                String fPath = a + "ExecutionLog\\" + filename + ".txt";
                if (!File.Exists(fPath))
                {
                    using (StreamWriter sw = File.CreateText(fPath))
                    {
                        sw.WriteLine(text);
                    }
                }
                else if (File.Exists(fPath))
                {
                    using (StreamWriter sw = File.CreateText(fPath))
                    {
                        sw.WriteLine(text);
                    }
                }
            }
            catch (Exception e)
            {
                //Catch exception if any
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public String readLastLine(String filename)
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "ExecutionLog\\" + filename + ".txt";
            string line = "";
            if (File.Exists(fPath))
            {
                var lines = File.ReadLines(fPath);
                line = lines.Last();
            }
            return line;
        }

        public String GetAllTextFile(string filename)
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "ExecutionLog\\" + filename + ".txt";
            return File.ReadAllText(fPath);
        }

        public void DeleteFile(string filename)
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "ExecutionLog\\" + filename + ".txt";
            if (File.Exists(fPath))
            {
                File.Delete(fPath);
            }
        }

        public void WriteInExcel(string TC, string Status, String JIRA_ID, String Module)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string filePath = "Execution_Result_File.csv";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.AppendAllText(filePath, "Test Name, Status, Date, Time, JIRA ID, Module" + "\n");
            }
            string delimiter = ",";
            string[][] output = new string[][]{
            new string[]{TC,Status,dateTime.ToString("dd/MM/yyyy"),DateTime.Now.ToString("h:mm:ss tt"),JIRA_ID,Module} /*add the values that you want inside a csv file. Mostly this function can be used in a foreach loop.*/
            };
            int length = output.GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));
            File.AppendAllText(filePath, sb.ToString());
        }

        /*
        public void WriteInExcel(string TC, string Status, String JIRA_ID,String Module)
        {
            Spreadsheet document = new Spreadsheet();
            Worksheet Sheet = null;
            if (!File.Exists("Execution_Result_File.csv"))
            {
                // Create new Spreadsheet
                Sheet = document.Workbook.Worksheets.Add("Report");
                Sheet.Cell("A1").Value = "Test Name";
                Sheet.Columns[0].Width = 250;
                Sheet.Cell("B1").Value = "Status";
                Sheet.Columns[1].Width = 250;
                Sheet.Cell("C1").Value = "Date";
                Sheet.Columns[2].Width = 10;
                Sheet.Cell("D1").Value = "Time";
                Sheet.Columns[3].Width = 100;
                Sheet.Cell("E1").Value = "JIRA ID";
                Sheet.Columns[4].Width = 10;
                Sheet.Cell("F1").Value = "Module";
                Sheet.Columns[5].Width = 10;
                document.SaveAs("Execution_Result_File.csv");
                document.Close();
                string Currentpath = Directory.GetCurrentDirectory();
                String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
                String a = ab[0];
                Console.WriteLine(a);
                String fPath = a + "ExecutionLog\\ReportCounter.txt";
                if (!File.Exists(fPath))
                {
                    Console.WriteLine(fPath);
                    using (StreamWriter sw = File.CreateText(fPath))
                    {
                        sw.WriteLine("2");
                        sw.Close();
                    }
                }

            }

            document.LoadFromFile("Execution_Result_File.csv");
            // Get worksheet by name
            Sheet = document.Workbook.Worksheets.ByName("Report");


            // add new worksheet
            Sheet = document.Workbook.Worksheets[0];

            Random rnd = new Random();

            String counter = readLastLine("ReportCounter");
            Count("ReportCounter", (Int16.Parse(counter) + 1).ToString());
            if (counter == "")
            {
                counter = "2";
            }

            int count = Int16.Parse(counter);
            // Append Data to file
            DateTime dateTime = DateTime.UtcNow.Date;
            if (File.Exists("Execution_Result_File.csv"))
            {
                Sheet.Cell("A" + count).Value = TC;
                Sheet.Cell("B" + count).Value = Status;
                Sheet.Cell("C" + count).Value = dateTime.ToString("dd/MM/yyyy");
                Sheet.Cell("D" + count).Value = DateTime.Now.ToString("h:mm:ss tt");
                Sheet.Cell("E" + count).Value = JIRA_ID;
                Sheet.Cell("F" + count).Value = Module;
            }
            // Save document
            document.SaveAs("Execution_Result_File.csv");

            // Close Spreadsheet
            document.Close();
        } 
         */
    }
}
