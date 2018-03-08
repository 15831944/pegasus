using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ExportUserToCsv : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void exportUserToCsv()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());

            var corpOffices_UsersHelper = new CorpOffices_UsersHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ExportUserToCsv", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ExportUserToCsv", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ExportUserToCsv", "Visit All User");
                VisitCorp("allusers");

                executionLog.Log("ExportUserToCsv", "Click on Export");
                corpOffices_UsersHelper.ClickElement("ClickOnExport");
                corpOffices_UsersHelper.WaitForWorkAround(3000);

                executionLog.Log("ExportUserToCsv", "Export as a CSV");
                corpOffices_UsersHelper.ClickElement("ExportToCSV");
                corpOffices_UsersHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ExportUserToCsv");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Export User To Csv");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Export User To Csv", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Export User To Csv");
                        TakeScreenshot("ExportUserToCsv");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportUserToCsv.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ExportUserToCsv");
                        string id = loginHelper.getIssueID("Export User To Csv");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportUserToCsv.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Export User To Csv"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Export User To Csv");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ExportUserToCsv");
                executionLog.WriteInExcel("Export User To Csv", Status, JIRA, "Corp Office");
            }
        }
    }
}