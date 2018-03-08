using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ProcessorNameOnSavingMerchant : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void processorNameOnSavingMerchant()
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
            var processorNameOnSavingMerchantHelper = new ProcessorNameOnSavingMerchantHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            var CompanyName = "QALeadCompany" + RandomNumber(1, 100);

            try
            {

                executionLog.Log("ProcessorNameOnSavingMerchant", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifySelectedUserGroup", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                VisitOffice("leads");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Create button");
                processorNameOnSavingMerchantHelper.ClickElement("CreateBtn");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Enter the Company Name");
                processorNameOnSavingMerchantHelper.TypeText("CompanyNameTab", CompanyName);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Select the status");
                processorNameOnSavingMerchantHelper.SelectByText("Status", "New");

                executionLog.Log("ProcessorNameOnSavingMerchant", "Select the responsibility");
                processorNameOnSavingMerchantHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Next button");
                processorNameOnSavingMerchantHelper.ClickElement("NextBtn");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Business details tab");
                processorNameOnSavingMerchantHelper.ClickElement("BusinessDetailsTab");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Save button");
                processorNameOnSavingMerchantHelper.ClickElement("SaveBtn");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Business Banking Account tab");
                processorNameOnSavingMerchantHelper.ClickElement("BusinessBankAcc");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Enter the routing number");
                processorNameOnSavingMerchantHelper.TypeText("RoutingNumber", "889876765");

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Save button");
                processorNameOnSavingMerchantHelper.ClickElement("SaveBtn");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                VisitOffice("leads");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);
                executionLog.Log("ProcessorNameOnSavingMerchant", "Search the company name");
                processorNameOnSavingMerchantHelper.TypeText("CompanySearchField", CompanyName);
                processorNameOnSavingMerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on first lead element");
                processorNameOnSavingMerchantHelper.ClickElement("FirstCompanyName");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Company details tab");
                processorNameOnSavingMerchantHelper.ClickElement("BusinessDetailsTab");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Enter the processor name");
                processorNameOnSavingMerchantHelper.TypeText("ProcessorName", "First Data Omaha");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on Save button");
                processorNameOnSavingMerchantHelper.ClickElement("SaveBtn");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Verify the added processor name ");
                processorNameOnSavingMerchantHelper.verifyFieldText("//*[@id='LeadDetailIfyesProcessorName']", "First Data Omaha");

                VisitOffice("leads");
                processorNameOnSavingMerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Search the company name");
                processorNameOnSavingMerchantHelper.TypeText("CompanySearchField", CompanyName);
                processorNameOnSavingMerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on first check box");
                processorNameOnSavingMerchantHelper.ClickElement("FirstCheckBox");

                executionLog.Log("ProcessorNameOnSavingMerchant", "Click on delete button");
                processorNameOnSavingMerchantHelper.ClickElement("DeleteBtn");
                processorNameOnSavingMerchantHelper.AcceptAlert();
                processorNameOnSavingMerchantHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ProcessorNameOnSavingMerchant");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Processor Name On Saving Merchant");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Processor Name On Saving Merchant", "Bug", "Medium", "Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Processor Name On Saving Merchant");
                        TakeScreenshot("ProcessorNameOnSavingMerchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorNameOnSavingMerchant.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ProcessorNameOnSavingMerchant");
                        string id = loginHelper.getIssueID("Processor Name On Saving Merchant");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ProcessorNameOnSavingMerchant.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Processor Name On Saving Merchant"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Processor Name On Saving Merchant");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ProcessorNameOnSavingMerchant");
                executionLog.WriteInExcel("Processor Name On Saving Merchant", Status, JIRA, "Office Merchant data");
            }
        }
    }
}
