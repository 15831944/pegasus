using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyClientsAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyClientsAdvanceFilerColumnOrder()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle();

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of company name column.");
                office_ClientsHelper.IsElementPresent("HeadCompany");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of contact column.");
                office_ClientsHelper.IsElementPresent("HeadContact");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of phone column.");
                office_ClientsHelper.IsElementPresent("HeadPhone");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of email column.");
                office_ClientsHelper.IsElementPresent("HeadEmail");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click on advance filter.");
                office_ClientsHelper.ClickElement("AdvanceFilter");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select company in displayed columns.");
                office_ClientsHelper.SelectByText("DisplayedCols", "Company");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                office_ClientsHelper.ClickElement("RemoveCols");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select contact in displayed columns.");
                office_ClientsHelper.SelectByText("DisplayedCols", "Contact Name");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ClientsHelper.ClickElement("RemoveCols");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select phone in displayed columns.");
                office_ClientsHelper.SelectByText("DisplayedCols", "Phone");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ClientsHelper.ClickElement("RemoveCols");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select email in displayed columns.");
                office_ClientsHelper.SelectByText("DisplayedCols", "E-Mail");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ClientsHelper.ClickElement("RemoveCols");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click on apply button.");
                office_ClientsHelper.ClickElement("Apply");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify company name not present on page.");
                office_ClientsHelper.IsElementNotPresent("HeadCompany");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify contact name not present on page.");
                office_ClientsHelper.IsElementNotPresent("HeadContact");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify phone not present on page.");
                office_ClientsHelper.IsElementNotPresent("HeadPhone");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify email not present on page.");
                office_ClientsHelper.IsElementNotPresent("HeadEmail");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of phone column.");
                office_ClientsHelper.IsElementPresent("HeadPhone4");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify default position of email column.");
                office_ClientsHelper.IsElementPresent("HeadEmail5");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Redirect at merchants page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click on advance filter.");
                office_ClientsHelper.ClickElement("AdvanceFilter");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select phone in displayed column.");
                office_ClientsHelper.SelectByText("DisplayedCols", "Phone");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                office_ClientsHelper.ClickElement("MoveUp");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                office_ClientsHelper.ClickElement("MoveUp");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Select email in displayed column.");
                office_ClientsHelper.SelectByText("DisplayedCols", "E-Mail");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Move email 1 step down.");
                office_ClientsHelper.ClickElement("MoveDown");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Click on apply button.");
                office_ClientsHelper.ClickElement("Apply");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify changed position of phone column.");
                office_ClientsHelper.IsElementPresent("HeadPhone2");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Verify changed position of email column.");
                office_ClientsHelper.IsElementPresent("HeadEmail5");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Clients Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Clients Advance Filer Column Order", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Clients Advance Filer Column Order");
                        TakeScreenshot("VerifyClientsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyClientsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Clients Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Clients Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Clients Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyClientsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Clients Advance Filer Column Order", Status, JIRA, "Opportunities Management");
            }
        }
    }
}