using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyAllOptionInStatusOnAllOffices : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyAllOptionInStatusOnAllOffices()
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
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyAllOptionInStatusOnAllOffices", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyAllOptionInStatusOnAllOffices", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyAllOptionInStatusOnAllOffices", "Redirect to office page.");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAllOptionInStatusOnAllOffices", "Click Status drop down");
                corpOffice_OfficeHelper.ClickElement("SelectStatus");

                executionLog.Log("VerifyAllOptionInStatusOnAllOffices", "Verify 'All' option is present");
                corpOffice_OfficeHelper.IsElementVisible("//option[contains(text(),'All')]");
                Console.WriteLine("All option is present");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAllOptionInStatusOnAllOffices");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify All Option In Status On All Offices");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify All Option In Status On All Offices", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify All Option In Status On All Offices");
                        TakeScreenshot("VerifyAllOptionInStatusOnAllOffices");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAllOptionInStatusOnAllOffices.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAllOptionInStatusOnAllOffices");
                        string id = loginHelper.getIssueID("Verify All Option In Status On All Offices");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAllOptionInStatusOnAllOffices.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify All Option In Status On All Offices"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify All Option In Status On All Offices");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAllOptionInStatusOnAllOffices");
                executionLog.WriteInExcel("Verify All Option In Status On All Offices", Status, JIRA, "Corp Offices");
            }
        }
    }
}