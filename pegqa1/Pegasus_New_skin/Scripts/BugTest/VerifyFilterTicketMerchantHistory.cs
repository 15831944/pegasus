using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyFilterTicketMerchantHistory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTest")]
        public void verifyFilterTicketMerchantHistory()
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
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyFilterTicketMerchantHistory", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyFilterTicketMerchantHistory", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyFilterTicketMerchantHistory", "Redirect to merchants page.");
                VisitCorp("merchants");

                executionLog.Log("VerifyFilterTicketMerchantHistory", "Click On Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyFilterTicketMerchantHistory", "Select tickets in merchant history.");
                corp_MerchantHelper.SelectByText("SelectTicketInMerchnatHistory", "Tickets");
                corp_MerchantHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyFilterTicketMerchantHistory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Filter Ticket Merchant History");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Filter Ticket Merchant History", "Bug", "Medium", "merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Filter Ticket Merchant History");
                        TakeScreenshot("VerifyFilterTicketMerchantHistory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyFilterTicketMerchantHistory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyFilterTicketMerchantHistory");
                        string id = loginHelper.getIssueID("Verify Filter Ticket Merchant History");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyFilterTicketMerchantHistory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Filter Ticket Merchant History"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Filter Ticket Merchant History");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyFilterTicketMerchantHistory");
                executionLog.WriteInExcel("Verify Filter Ticket Merchant History", Status, JIRA, "Corp merchant.");
            }
        }
    }
}
