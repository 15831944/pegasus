using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OfficeUserExportToExcel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void officeUserExportToExcel()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffices_UsersHelper = new CorpOffices_UsersHelper(GetWebDriver());

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OfficeUserExportToCSV", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("OfficeUserExportToExcel", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OfficeUserExportToExcel", "Redirect to Office");
                VisitCorp("allusers");

                executionLog.Log("OfficeUserExportToExcel", "Verify Page title");
                VerifyTitle("All Users");
                
                executionLog.Log("OfficeUserExportToExcel", "Click on Export");
                corpOffices_UsersHelper.ClickElement("ClickOnExport");
                corpOffices_UsersHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeUserExportToExcel", "Click on EXport TO Excel");
                corpOffices_UsersHelper.ClickElement("ExporttoExcel");
                
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeUserExportToExcel");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office User Export To Excel");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office User Export To Excel", "Bug", "Medium", "Export to excel page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office User Export To Excel");
                        TakeScreenshot("OfficeUserExportToExcel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeUserExportToExcel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeUserExportToExcel");
                        string id = loginHelper.getIssueID("Office User Export To Excel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeUserExportToExcel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office User Export To Excel"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office User Export To Excel");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OfficeUserExportToExcel");
                executionLog.WriteInExcel("Office User Export To Excel", Status, JIRA, "Corp Office");
            }
        }
    }
}

