using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssoSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void partnerAssoSave()
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

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssoSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssoSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAssoSave", "Redirect  to Create parnter association page");
                VisitOffice("partners/association/create");

                executionLog.Log("PartnerAssoSave", "Verify title");
                VerifyTitle("Create a Partner Association");

                executionLog.Log("PartnerAssoSave", "Click on Save button");
                agents_PartnerAssociationHelper.ClickElement("Save");

                executionLog.Log("PartnerAssoSave", "Verify the validation for birthday");
                agents_PartnerAssociationHelper.WaitForText("Age should be greater than 18.", 5);

                executionLog.Log("PartnerAssoSave", "Enter the date of birth");
                agents_PartnerAssociationHelper.TypeText("PartnerAssoBirthday", "01/02/1990");

                executionLog.Log("PartnerAssoSave", "Click on Save button");
                agents_PartnerAssociationHelper.ClickElement("Save");

                executionLog.Log("PartnerAssoSave", "Verify the validation for the field's ");
                agents_PartnerAssociationHelper.WaitForText("This field is required.", 5);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssoSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Asso Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Asso Save", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Asso Save");
                        TakeScreenshot("PartnerAssoSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssoSave");
                        string id = loginHelper.getIssueID("Partner Asso Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Asso Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Asso Save");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssoSave");
                executionLog.WriteInExcel("Partner Asso Save", Status, JIRA, "Partner Portal");
            }
        }
    }
}