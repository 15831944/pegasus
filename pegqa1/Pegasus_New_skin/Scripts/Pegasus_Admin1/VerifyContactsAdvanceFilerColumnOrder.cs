using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyContactsAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyContactsAdvanceFilerColumnOrder()
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
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Contacts");

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of company name column.");
                office_ContactsHelper.IsElementPresent("HeadCompany");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of contact column.");
                office_ContactsHelper.IsElementPresent("HeadContact");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of phone column.");
                office_ContactsHelper.IsElementPresent("HeadPhone");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of email column.");
                office_ContactsHelper.IsElementPresent("HeadEmail");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select company in displayed columns.");
                office_ContactsHelper.SelectByText("DisplayedCols", "Company");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                office_ContactsHelper.ClickElement("RemoveCols");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select contact in displayed columns.");
                office_ContactsHelper.SelectByText("DisplayedCols", "Contact Type");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ContactsHelper.ClickElement("RemoveCols");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select phone in displayed columns.");
                office_ContactsHelper.SelectByText("DisplayedCols", "Phone");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ContactsHelper.ClickElement("RemoveCols");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select email in displayed columns.");
                office_ContactsHelper.SelectByText("DisplayedCols", "E-Mail");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                office_ContactsHelper.ClickElement("RemoveCols");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify company name not present on page.");
                office_ContactsHelper.IsElementNotPresent("HeadCompany");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify contact name not present on page.");
                office_ContactsHelper.IsElementNotPresent("HeadContact");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify phone not present on page.");
                office_ContactsHelper.IsElementNotPresent("HeadPhone");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify email not present on page.");
                office_ContactsHelper.IsElementNotPresent("HeadEmail");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Redirect at clients page.");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify page title as clients");
                VerifyTitle("Contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of phone column.");
                office_ContactsHelper.IsElementPresent("HeadPhone5");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify default position of email column.");
                office_ContactsHelper.IsElementPresent("HeadEmail4");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Redirect at merchants page.");
                VisitOffice("contacts");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click on advance filter.");
                office_ContactsHelper.ClickElement("AdvanceFilter");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select phone in displayed column.");
                office_ContactsHelper.SelectByText("DisplayedCols", "Phone");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                office_ContactsHelper.ClickElement("MoveUp");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                office_ContactsHelper.ClickElement("MoveUp");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Select email in displayed column.");
                office_ContactsHelper.SelectByText("DisplayedCols", "E-Mail");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Move email 1 step down.");
                office_ContactsHelper.ClickElement("MoveDown");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Click on apply button.");
                office_ContactsHelper.ClickElement("Apply");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify changed position of phone column.");
                office_ContactsHelper.IsElementPresent("HeadPhone2");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Verify changed position of email column.");
                office_ContactsHelper.IsElementPresent("HeadEmail5");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Contacts Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Contacts Advance Filer Column Order", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Contacts Advance Filer Column Order");
                        TakeScreenshot("VerifyContactsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Contacts Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Contacts Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Contacts Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Contacts Advance Filer Column Order", Status, JIRA, "Opportunities Management");
            }
        }
    }
}