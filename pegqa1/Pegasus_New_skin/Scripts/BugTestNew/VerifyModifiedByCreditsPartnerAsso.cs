using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyModifiedByCreditsPartnerAsso : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyModifiedByCreditsPartnerAsso()
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
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            //Variable
            var name = "TestAgent" + GetRandomNumber();
            var username1 = "testingasso" + RandomNumber(111,99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Goto Partner Association page");
                VisitOffice("partners/associations");

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Click on first edit icon");
                agent_PartnerAssociationHelper.ClickElement("ClickOnEditicon");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Select eAddress type as work");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Enter Username");
                agent_PartnerAssociationHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyingPartnerAssociationModiifedCredentials", "Select Avatar");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Click on Save");
                agent_PartnerAssociationHelper.ClickElement("Save");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByCreditsPartnerAsso.", "Verify modified by Credits");
                agent_PartnerAssociationHelper.VerifyText("ModifiedOn", "By Howard Tang");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyModifiedByCreditsPartnerAsso.");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Modified By Credits Partner Asso.");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Modified By Credits Partner Asso.", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Modified By Credits Partner Asso.");
                        TakeScreenshot("VerifyModifiedByCreditsPartnerAsso.");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByCreditsPartnerAsso..png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyModifiedByCreditsPartnerAsso.");
                        string id = loginHelper.getIssueID("Verify Modified By Credits Partner Asso.");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByCreditsPartnerAsso..png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Modified By Credits Partner Asso."), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Modified By Credits Partner Asso.");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyModifiedByCreditsPartnerAsso.");
                executionLog.WriteInExcel("Verify Modified By Credits Partner Asso.", Status, JIRA, "Agents Portal");
            }
        }
    }
}