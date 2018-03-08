using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class DelateMasterLanguagesInBulk : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void delateMasterLanguagesInBulk()
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
            var delateMasterLanguagesInBulkHelper = new DelateMasterLanguagesInBulkHelper(GetWebDriver());


            var Name = "Testlag" + RandomNumber(1, 50);
            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Goto Languages");
                VisitCorp("languages");
                delateMasterLanguagesInBulkHelper.WaitForWorkAround(3000);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Click on create button");
                delateMasterLanguagesInBulkHelper.ClickElement("CreateBtn");
                delateMasterLanguagesInBulkHelper.WaitForWorkAround(3000);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Enter the langauage");
                delateMasterLanguagesInBulkHelper.TypeText("Name", Name);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Click on save btn");
                delateMasterLanguagesInBulkHelper.ClickElement("SaveBtn");
                delateMasterLanguagesInBulkHelper.WaitForWorkAround(3000);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Search the same launage");
                delateMasterLanguagesInBulkHelper.TypeText("Search", Name);
                delateMasterLanguagesInBulkHelper.WaitForWorkAround(3000);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Click on delete icon");
                delateMasterLanguagesInBulkHelper.ClickForce("Deletebtn");
                delateMasterLanguagesInBulkHelper.WaitForWorkAround(3000);

                executionLog.Log("DelateMasterLanguagesInBulkHelper", "Click on delete button");
                delateMasterLanguagesInBulkHelper.ClickElement("DeleteBtn");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DelateMasterLanguagesInBulkHelper");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delate Master Languages In BulkHelper");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delate Master Languages In Bulk", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delate Master Languages In BulkHelper");
                        TakeScreenshot("DelateMasterLanguagesInBulkHelper");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DelateMasterLanguagesInBulk.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DelateMasterLanguagesInBulk");
                        string id = loginHelper.getIssueID("Delate Master Languages In Bulk");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DelateMasterLanguagesInBulk.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delate Master Languages In Bulk"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delate Master Languages In Bulk");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DelateMasterLanguagesInBulk");
                executionLog.WriteInExcel("Delate Master Languages In Bulk", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
