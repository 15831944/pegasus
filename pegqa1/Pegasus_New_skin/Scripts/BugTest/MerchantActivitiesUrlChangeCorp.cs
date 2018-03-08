using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantActivitiesUrlChangeCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void merchantActivitiesUrlChangeCorp()
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
                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Create Merchant");
                VisitCorp("merchants");

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Enter Clinet To Search");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", "Chy Company");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Click On Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Click On any item in History");
                corp_MerchantHelper.scrollToElement("ClickOnActivityAny");
                corp_MerchantHelper.ClickElement("ClickOnActivityAny");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Go to details of Any Other Merchant");
                VisitCorp("merchants/document/view/726964");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantActivitiesUrlChangeCorp", "Verify message.");
                corp_MerchantHelper.VerifyPageText("You don't have privileges to view this merchant activity.");
                corp_MerchantHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchantActivitiesUrlChangeCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchant Activities Url Change Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchant Activities Url Change Corp", "Bug", "Medium", "Corp Merchant", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchant Activities Url Change Corp");
                        TakeScreenshot("MerchantActivitiesUrlChangeCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantActivitiesUrlChangeCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchantActivitiesUrlChangeCorp");
                        string id = loginHelper.getIssueID("Merchant Activities Url Change Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantActivitiesUrlChangeCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchant Activities Url Change Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchant Activities Url Change Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchantActivitiesUrlChangeCorp");
                executionLog.WriteInExcel("Merchant Activities Url Change Corp", Status, JIRA, "Corp Merchants");
            }
        }
    }
}
