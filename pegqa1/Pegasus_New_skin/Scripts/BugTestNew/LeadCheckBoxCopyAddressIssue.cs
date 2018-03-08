using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class LeadCheckBoxCopyAddressIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void leadCheckBoxCopyAddressIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            var address = "Copy Check" + RandomNumber(33, 555);
            String JIRA = "";
            String Status = "Pass";

            //      try
            //       {
            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Visit  Lead");
            VisitOffice("leads/create");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Enter First Name");
            office_LeadsHelper.TypeText("FirstNameLead", FName);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Enter Last Name");
            office_LeadsHelper.TypeText("LastName", LName);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Enter Company DBA");
            office_LeadsHelper.TypeText("CompanyName", CDBA);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on Assignments");
            office_LeadsHelper.ClickElement("Assignments");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Wait for element to be visible.");
            office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Select Status");
            office_LeadsHelper.SelectByText("LeadStatus", "New");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Select Responsibities");
            office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on Save");
            office_LeadsHelper.ClickElement("SaveLeadNewSkin");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Wait for Confirmation");
            office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Redirect at leads page.");
            VisitOffice("leads");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify page title as leads.");
            VerifyTitle("Leads");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on first lead.");
            office_LeadsHelper.ClickElement("Lead1");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on company details.");
            office_LeadsHelper.ClickElement("CompanyDetails");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Enter a valid zip code");
            office_LeadsHelper.TypeText("LeadZip" ,"60601");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Enter address line 1");
            office_LeadsHelper.TypeText("AddressLine1", address);
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on checkbox to copy address.");
            office_LeadsHelper.ClickForce("CopyAddress");
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on save button.");
            office_LeadsHelper.ClickElement("SaveLead");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Wait for lead updation success message.");
            office_LeadsHelper.WaitForText("Lead data updated successfully. .", 10);
            office_LeadsHelper.WaitForWorkAround(3000);

            office_LeadsHelper.ClickElement("CompanyDetails");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify address line 1 copied to mailing addresss line1");
            office_LeadsHelper.VerifyCheckBox(address);
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Redirect at leads page.");
            VisitOffice("leads");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify page title as leads.");
            VerifyTitle("Leads");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on First Lead To check");
            office_LeadsHelper.ClickElement("CheckDocToDel");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click on Delete button.");
            office_LeadsHelper.ClickElement("ClickDelLeadbutton");
            office_LeadsHelper.AcceptAlert();

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify Confirmation");
            office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Goto leads/recyclebin ");
            VisitOffice("leads/recyclebin");

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Click Restore");
            office_LeadsHelper.ClickElement("DeleteLeadPer");
            office_LeadsHelper.AcceptAlert();

            executionLog.Log("LeadCheckBoxCopyAddressIssue", "Verify Confirmation");
            office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);





        }
    } }
  /*      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadCheckBoxCopyAddressIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("LeadCheckBoxCopyAddressIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("LeadCheckBoxCopyAddressIssue", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("LeadCheckBoxCopyAddressIssue");
                        TakeScreenshot("LeadCheckBoxCopyAddressIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadCheckBoxCopyAddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadCheckBoxCopyAddressIssue");
                        string id = loginHelper.getIssueID("LeadCheckBoxCopyAddressIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadCheckBoxCopyAddressIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("LeadCheckBoxCopyAddressIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("LeadCheckBoxCopyAddressIssue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadCheckBoxCopyAddressIssue");
                executionLog.WriteInExcel("LeadCheckBoxCopyAddressIssue", Status, JIRA, "Lead Management");
            }
        }
    }
}*/