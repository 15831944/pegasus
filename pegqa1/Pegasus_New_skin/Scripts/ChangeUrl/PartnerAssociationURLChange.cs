using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void partnerAssociationURLChange()
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

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PartnerAssociationURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssociationURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAssociationURLChange", "Goto User Agents >> Partner Association");
                VisitOffice("partners/associations");

                executionLog.Log("PartnerAssociationURLChange", "Click On Partner Association");
                agent_PartnerAssociationHelper.ClickElement("ClickOnPartnerAssociation");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAssociationURLChange", "Change the url with the url number of another office");
                VisitOffice("partners/association/view/197");

                executionLog.Log("PartnerAssociationURLChange", "Verify Validation");
                agent_PartnerAssociationHelper.WaitForText("You don't have privilege.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association URL Change", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association URL Change");
                        TakeScreenshot("PartnerAssociationURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationURLChange");
                        string id = loginHelper.getIssueID("Partner Association URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationURLChange");
                executionLog.WriteInExcel("Partner Association URL Change", Status, JIRA, "Office Agents");
            }
        }
    }
}
