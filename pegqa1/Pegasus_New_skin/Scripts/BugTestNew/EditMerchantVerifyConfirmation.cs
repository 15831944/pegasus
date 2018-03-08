using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditMerchantVerifyConfirmation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editMerchantVerifyConfirmation()
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
            var corp_MasterData_MerchantType = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditMerchantVerifyConfirmation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditMerchantVerifyConfirmation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditMerchantVerifyConfirmation", "Go To Corp Master Data >> Merchant Type");
                VisitCorp("masterdata/merchant_types");
                corp_MasterData_MerchantType.WaitForWorkAround(2000);

                executionLog.Log("EditMerchantVerifyConfirmation", "Click on Edit Icon");
                corp_MasterData_MerchantType.ClickElement("EditMerchnatType");

                executionLog.Log("EditMerchantVerifyConfirmation", "Click on Save button");
                corp_MasterData_MerchantType.ClickElement("Save");
                corp_MasterData_MerchantType.WaitForWorkAround(2000);

                executionLog.Log("EditMerchantVerifyConfirmation", "Verify Confirmation");
                corp_MasterData_MerchantType.VerifyPageText("The merchant type is successfully updated!!");
                corp_MasterData_MerchantType.WaitForWorkAround(2000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditMerchantVerifyConfirmation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Merchant Verify Confirmation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Merchant Verify Confirmation", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Merchant Verify Confirmation");
                        TakeScreenshot("EditMerchantVerifyConfirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMerchantVerifyConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditMerchantVerifyConfirmation");
                        string id = loginHelper.getIssueID("Edit Merchant Verify Confirmation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMerchantVerifyConfirmation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Merchant Verify Confirmation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Merchant Verify Confirmation");
              //  executionLog.DeleteFile("Error");

                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditMerchantVerifyConfirmation");
                executionLog.WriteInExcel("Edit Merchant Verify Confirmation", Status, JIRA, "Corp MasterData");
            }
        }
    }
}

