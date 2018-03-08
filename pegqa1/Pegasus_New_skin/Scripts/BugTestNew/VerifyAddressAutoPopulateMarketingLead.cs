using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyAddressAutoPopulateMarketingLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyAddressAutoPopulateMarketingLead()
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
                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Redirect at All Leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Open a lead");
                office_LeadsHelper.ClickElement("FirstLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Go to Marketing tab");
                office_LeadsHelper.ClickElement("MarketingTab");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Select Did Merchant Choose Our Office >> Yes");
                office_LeadsHelper.SelectByText("MrchntChsOffice", "Yes");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Select Previous Merchant Account >> Yes");
                office_LeadsHelper.SelectByText("PreMrchntAcc", "Yes");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Enter Zip Code in Previous Processor Zip");
                office_LeadsHelper.TypeText("PreProcZip", "20001");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Verify City populated");
                office_LeadsHelper.VerifyTextBoxValue("PreProcCity", "Washington");

                executionLog.Log("VerifyAddressAutoPopulateMarketingLead", "Verify State populated");
                office_LeadsHelper.verifyselectedoptn("PreProcState", "DC");
                Console.WriteLine("City anf State Populated");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddressAutoPopulateMarketingLead");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Address Auto Populate Marketing Lead");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Address Auto Populate Marketing Lead", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Address Auto Populate Marketing Lead");
                        TakeScreenshot("VerifyAddressAutoPopulateMarketingLead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddressAutoPopulateMarketingLead.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddressAutoPopulateMarketingLead");
                        string id = loginHelper.getIssueID("Verify Address Auto Populate Marketing Lead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddressAutoPopulateMarketingLead.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Address Auto Populate Marketing Lead"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Address Auto Populate Marketing Lead");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddressAutoPopulateMarketingLead");
                executionLog.WriteInExcel("Verify Address Auto Populate Marketing Lead", Status, JIRA, "Leads Marketing");
            }
        }
    }
}