using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssociationUserAdditionalInfo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void partnerAssociationUserAdditionalInfo()
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
                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Verify Page title");
                VerifyTitle("Dashboard");
                //agent_PartnerAssociationHelper.WaitForWorkAround(4000);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Redirect to create user page");
                VisitOffice("users/create");
                agent_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Select User type");
                agent_PartnerAssociationHelper.Select("SElectUserType", "Partner Association");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Click radio button");
                agent_PartnerAssociationHelper.ClickElement("SelectRadioNewBtn");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Click On ToolBox");
                agent_PartnerAssociationHelper.CheckAndClick("AddConctInfo");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                executionLog.Log("PartnerAssociationUserAdditionalInfo", "Enter User eAddress");
                agent_PartnerAssociationHelper.TypeText("UserEaDDRESS", "test@yopmail.com");
                agent_PartnerAssociationHelper.WaitForWorkAround(1000);

                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssociationUserAdditionalInfo");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Association User Additional Info");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Association User Additional Info", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Association User Additional Info");
                        TakeScreenshot("PartnerAssociationUserAdditionalInfo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationUserAdditionalInfo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssociationUserAdditionalInfo");
                        string id = loginHelper.getIssueID("Partner Association User Additional Info");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssociationUserAdditionalInfo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Association User Additional Info"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Association User Additional Info");
                //executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssociationUserAdditionalInfo");
                executionLog.WriteInExcel("Partner Association User Additional Info", Status, JIRA, "Agents Portal");
            }
        }
    }
}