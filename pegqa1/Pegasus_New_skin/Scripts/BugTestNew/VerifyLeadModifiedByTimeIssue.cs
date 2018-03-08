using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyLeadModifiedByTimeIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("All")]
        [TestCategory("Temp")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyLeadModifiedByTimeIssue()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("LeadLabelAccountManagerBlankSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Redirected at Leads page");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Get lead updation time and date.");
                string aa = office_LeadsHelper.GetText("//table[@id='list1']/tbody/tr[2]/td[20]");
                Console.WriteLine(aa);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Open a lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Click on Company details tab");
                office_LeadsHelper.ClickElement("CompanyDetails");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Search lead to be updated.");
                office_LeadsHelper.SelectByText("Salutaion", "Mr");
                //office_LeadsHelper.WaitForWorkAround(4000);

                //executionLog.Log("LeadLabelAccountManagerBlankSave", "Edit the searched lead.");
                //office_LeadsHelper.ClickElement("EditLeadPartner");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Click on save button.");
                office_LeadsHelper.ClickElement("SaveCD");
                office_LeadsHelper.WaitForWorkAround(3000);
                //office_LeadsHelper.WaitForText("Lead saved successfully", 10);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Redirect at home page by clicking home tab.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                //executionLog.Log("LeadLabelAccountManagerBlankSave", "search lead we have updated.");
                //office_LeadsHelper.TypeText("ActivitySubject", "aslam comp");
                //office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Get updated date and time.");
                string bb = office_LeadsHelper.GetText("//table[@id='list1']/tbody/tr[2]/td[20]");
                Console.WriteLine(bb);

                executionLog.Log("LeadLabelAccountManagerBlankSave", "Verify time before and after updation are not same.");
                Console.WriteLine("AA before update: " + aa.Trim().ToLower() + " AND bb after update " + bb.Trim().ToLower());
                Assert.AreNotEqual(aa.Trim().ToLower(), bb.Trim().ToLower());

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadLabelAccountManagerBlankSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Label Account Manager Blank Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Label Account Manager Blank Save", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Label Account Manager Blank Save");
                        TakeScreenshot("LeadLabelAccountManagerBlankSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadLabelAccountManagerBlankSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadLabelAccountManagerBlankSave");
                        string id = loginHelper.getIssueID("Lead Label Account Manager Blank Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadLabelAccountManagerBlankSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Label Account Manager Blank Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Label Account Manager Blank Save");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadLabelAccountManagerBlankSave");
                executionLog.WriteInExcel("Lead Label Account Manager Blank Save", Status, JIRA, "Leads Management");
            }
        }
    }
}
