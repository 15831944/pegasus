using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PartnerAssociationCallThisPhone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void partnerAssociationCallThisPhone()
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
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("PartnerAssociationCallThisPhone", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("PartnerAssociationCallThisPhone", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAssociationCallThisPhone", "Goto Opportinuties");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationCallThisPhone", "Open Partner Association");
                agents_PartnerAssociationHelper.ClickElement("ClickOnPartnerAssociation");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationCallThisPhone", "Edit Partner Association");
                agents_PartnerAssociationHelper.clickJS("EditPartnerAssociation");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationCallThisPhone", "Enter Phone Number.");
                agents_PartnerAssociationHelper.TypeText("PhoneNum", "8533327453");

                executionLog.Log("PartnerAssociationCallThisPhone", "Click On Save Button");
                agents_PartnerAssociationHelper.clickJS("AssSave");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationCallThisPhone", "Click on Phone Icon.");
                agents_PartnerAssociationHelper.clickJS("PhoneIcon");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationCallThisPhone");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association Call This Phone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association Call This Phone", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association Call This Phone");
                        TakeScreenshot("PartnerAssociationCallThisPhone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationCallThisPhone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationCallThisPhone");
                        string id = loginHelper.getIssueID("Partner Association Call This Phone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationCallThisPhone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association Call This Phone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association Call This Phone");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationCallThisPhone");
                executionLog.WriteInExcel("Partner Association Call This Phone", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
