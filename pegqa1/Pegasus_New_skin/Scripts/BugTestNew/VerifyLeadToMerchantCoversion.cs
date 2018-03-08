using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyLeadToMerchantCoversion : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("BugTestNew")]
        public void verifyLeadToMerchantCoversion()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            var Company = "My Company" + RandomNumber(1, 999);
            var LocDub = "//button[text()='Create Duplicate']";

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyLeadToMerchantCoversion", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Redirect To create lead page");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Enter First Name ");
                office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Enter Company Nmae");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Select Lead Status");
                office_LeadsHelper.Select("LeadStatus", "New");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Select LeadResponsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Save");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(5000);

                if (office_LeadsHelper.IsElementPresent(LocDub))
                {
                    office_LeadsHelper.Click(LocDub);
                }

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickConvert");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Meeting check box");
                office_LeadsHelper.ClickElement("MeetingChkBx");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Tasks check box");
                office_LeadsHelper.ClickElement("TasksChkBx");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Emails check box");
                office_LeadsHelper.ClickElement("EmailChkBx");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on Calls check box");
                office_LeadsHelper.ClickElement("CallsChkBx");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Yes Move To Recycle Bin");
                office_LeadsHelper.ClickElement("ClickYes");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click Convert Save Lead");
                office_LeadsHelper.ClickElement("ConvertSaveLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Verify 500 Error not occurred");
                Assert.IsFalse(GetWebDriver().PageSource.Contains("Internal Server Error"));

                executionLog.Log("VerifyLeadToMerchantCoversion", "Redirect To create lead page");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Search lead in recycle bin");
                office_LeadsHelper.TypeText("SearchLeadRbin", Company);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Select All responsibity");
                office_LeadsHelper.SelectDropDownByText("//*[@id='gs_owner']", "All");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyLeadToMerchantCoversion", "Click on delete icon");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyLeadToMerchantCoversion", "Accept alert message");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("VerifyLeadToMerchantCoversion", "Verify  delete message");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLeadToMerchantCoversion");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Lead To Merchant Coversion");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Lead To Merchant Coversion", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Lead To Merchant Coversion");
                        TakeScreenshot("VerifyLeadToMerchantCoversion");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadToMerchantCoversion.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLeadToMerchantCoversion");
                        string id = loginHelper.getIssueID("Verify Lead To Merchant Coversion");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadToMerchantCoversion.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Lead To Merchant Coversion"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Lead To Merchant Coversion");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLeadToMerchantCoversion");
                executionLog.WriteInExcel("Verify Lead To Merchant Coversion", Status, JIRA, "Leads Management");
            }
        }
    }
}