using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SysPRNIssue1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void sysPRNIssue1()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeCodesHelper = new CorpOffice_OfficeCodesHelper(GetWebDriver());

            try
            {
                executionLog.Log("SysPRNIssue1", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SysPRNIssue1", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SysPRNIssue1", "Go to office code management");
                VisitCorp("office_codes_management");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(4000);

                executionLog.Log("SysPRNIssue1", "Verify title");
                VerifyTitle("Office Codes Management");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(1000);

                executionLog.Log("SysPRNIssue1", "Select Sysprins");
                corpOffice_OfficeCodesHelper.SelectByText("SelectSys", "Apple 597884189");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(5000);

                executionLog.Log("SysPRNIssue1", "Enter value1");
                corpOffice_OfficeCodesHelper.TypeText("Value1", GetRandomNumber().ToString());
                corpOffice_OfficeCodesHelper.WaitForWorkAround(2000);


                executionLog.Log("SysPRNIssue1", "Click on Add button");
                corpOffice_OfficeCodesHelper.ClickElement("AddAnother");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(2000);

                executionLog.Log("SysPRNIssue1", "Click on Second primary option");
                corpOffice_OfficeCodesHelper.ClickElement("SecOp");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(2000);

                executionLog.Log("SysPRNIssue1", "Click on Save button");
                corpOffice_OfficeCodesHelper.ClickElement("RMPSave");
                corpOffice_OfficeCodesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SysPRNIssue1");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sys PRN Issue 1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sys PRN Issue 1", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sys PRN Issue 1");
                        TakeScreenshot("SysPRNIssue1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SysPRNIssue1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SysPRNIssue1");
                        string id = loginHelper.getIssueID("Sys PRN Issue 1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SysPRNIssue1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sys PRN Issue 1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sys PRN Issue 1");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SysPRNIssue1");
                executionLog.WriteInExcel("Sys PRN Issue 1", Status, JIRA, "Corp Office");
            }
        }
    }
}