using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ViewMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void viewMerchant()
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

            // Variable random
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("ViewMerchant", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ViewMerchant", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ViewMerchant", "Click on Merchants tab.");
                VisitCorp("merchants");

                executionLog.Log("ViewMerchant", "Click on any merchant.");
                corp_MerchantHelper.ClickElement("CickonMercahnt");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ViewMerchant", "Verify text history on page.");
                corp_MerchantHelper.VerifyPageText("Merchant History");
                corp_MerchantHelper.WaitForWorkAround(3000);

            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ViewMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("View Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("View Merchant", "Bug", "Medium", "Corp Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("View Merchant");
                        TakeScreenshot("ViewMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ViewMerchant");
                        string id = loginHelper.getIssueID("View Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("View Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("View Merchant");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ViewMerchant");
                executionLog.WriteInExcel("View Merchant", Status, JIRA, "Corp Merchant");
            }
        }
    }
}