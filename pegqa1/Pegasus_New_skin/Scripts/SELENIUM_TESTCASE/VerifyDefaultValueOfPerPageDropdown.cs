using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyDefaultValueOfPerPageDropdown : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void verifyDefaultValueOfPerPageDropdown()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                office_LeadsHelper.WaitForWorkAround(6000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Goto Leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(6000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Open Advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Select Result Page per 10.");
                office_LeadsHelper.SelectByText("ResultsPerPage", "10");

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Click on apply button.");
                office_LeadsHelper.ClickJS("Apply");

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Open Advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Open Reset button.");
                office_LeadsHelper.ClickElement("ResetAdvFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Open Advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("verifyDefaultValueOfPerPageDropdown", "Verify default value 100 in Result Per page dropdown.");
                office_LeadsHelper.VerifySelectedOption("//*[@id='pageresults']", "100");

            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("verifyDefaultValueOfPerPageDropdown");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("verify Default Value Of PerPageDropdown");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("verify Default Value Of PerPage Dropdown", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("verify Default Value Of PerPage Dropdown");
                        TakeScreenshot("verifyDefaultValueOfPerPageDropdown");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\verifyDefaultValueOfPerPageDropdown.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("verifyDefaultValueOfPerPageDropdown");
                        string id = loginHelper.getIssueID("verify Default Value Of Per Page Dropdown");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\verifyDefaultValueOfPerPageDropdown.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("verify Default Value Of PerPage Dropdown"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("verify Default Value Of Per Page Dropdown");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("verify Default Value Of PerPage Dropdown");
                executionLog.WriteInExcel("verify Default Value Of Per Page Dropdown", Status, JIRA, "Office Lead");
            }
        }
    }
}
