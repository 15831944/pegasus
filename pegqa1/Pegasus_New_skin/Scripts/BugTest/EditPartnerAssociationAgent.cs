using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPartnerAssociationAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editPartnerAssociationAgent()
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
            var username1 = "employeeuser" + RandomNumber(111,99999);
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditPartnerAssociationAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPartnerAssociationAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditPartnerAssociationAgent", "Goto Partner Association");
                VisitOffice("partners/associations");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Click on first check box.");
                agent_PartnerAssociationHelper.ClickElement("ClickOnEditicon");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Clixk on edit icon");
                agent_PartnerAssociationHelper.ClickElement("LastName");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Enter TwitterURL");
                agent_PartnerAssociationHelper.TypeText("TwitterURL", "Twiter.com");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Enter LinkedIn");
                var LinkedIn = "LinkedIn" + GetRandomNumber() + ".com";
                agent_PartnerAssociationHelper.TypeText("LinkedInUrl", LinkedIn);
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Enter FaceBook");
                var Facebook = "Facebook" + GetRandomNumber() + ".com";
                agent_PartnerAssociationHelper.TypeText("FaceBookUrl", Facebook);
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Select eAddressLebel");
                agent_PartnerAssociationHelper.Select("eAddressLebel", "Work");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Enter Username");
                agent_PartnerAssociationHelper.TypeText("UserName", username1);

                executionLog.Log("EditPartnerAssociationAgent", "Select avatar");
                agent_PartnerAssociationHelper.ClickElement("ClickOnAvatar");

                executionLog.Log("EditPartnerAssociationAgent", "Click Save");
                agent_PartnerAssociationHelper.ClickElement("Save");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Wait for element present.");
                agent_PartnerAssociationHelper.WaitForText("Partner Associations", 10);

                executionLog.Log("EditPartnerAssociationAgent", "Verify LinkedIn");
                agent_PartnerAssociationHelper.VerifyText("VerifyTwitter", LinkedIn);
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("EditPartnerAssociationAgent", "Verify FaceBook");
                agent_PartnerAssociationHelper.VerifyText("VerifyFaceBook", Facebook);
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPartnerAssociationAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Partner Association Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Partner Association Agent", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Partner Association Agent");
                        TakeScreenshot("EditPartnerAssociationAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPartnerAssociationAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPartnerAssociationAgent");
                        string id = loginHelper.getIssueID("Edit Partner Association Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPartnerAssociationAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Partner Association Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Partner Association Agent");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPartnerAssociationAgent");
                executionLog.WriteInExcel("Edit Partner Association Agent", Status, JIRA, "Agents Portal");
            }
        }
    }
}