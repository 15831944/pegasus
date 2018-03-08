using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ConvertLeadMoveToRecycleBin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void convertLeadMoveToRecycleBin()
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

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Redirect To create lead page");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Enter First Name ");
                office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", "Tester");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Enter Company Nmae");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Select Lead Status");
                office_LeadsHelper.Select("LeadStatus", "New");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Select LeadResponsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Click on Save");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(5000);

                if (office_LeadsHelper.IsElementPresent(LocDub))
                {
                    office_LeadsHelper.Click(LocDub);
                }

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickConvert");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Yes Move To Recycle Bin");
                office_LeadsHelper.ClickElement("ClickYes");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Click Convert Save Lead");
                office_LeadsHelper.ClickElement("ConvertSaveLead");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Verify  messge");
                office_LeadsHelper.WaitForText("Lead is converted and moved to recyclebin.", 10);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Redirect To create lead page");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Search lead in recycle bin");
                office_LeadsHelper.TypeText("SearchLeadRbin", Company);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Select All responsibity");
                office_LeadsHelper.SelectDropDownByText("//*[@id='gs_owner']", "All");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Click on delete icon");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Accept alert message");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("ConvertLeadMoveToRecycleBin", "Verify  delete message");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ConvertLeadMoveToRecycleBin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Convert Lead Move To Recycle Bin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Convert Lead Move To Recycle Bin", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Convert Lead Move To Recycle Bin");
                        TakeScreenshot("ConvertLeadMoveToRecycleBin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ConvertLeadMoveToRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ConvertLeadMoveToRecycleBin");
                        string id = loginHelper.getIssueID("Convert Lead Move To Recycle Bin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ConvertLeadMoveToRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Convert Lead Move To Recycle Bin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Convert Lead Move To Recycle Bin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ConvertLeadMoveToRecycleBin");
                executionLog.WriteInExcel("Convert Lead Move To Recycle Bin", Status, JIRA, "Leads Management");
            }
        }
    }
}