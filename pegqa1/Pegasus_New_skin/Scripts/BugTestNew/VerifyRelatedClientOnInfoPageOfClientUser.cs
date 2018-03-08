using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyRelatedClientOnInfoPageOfClientUser : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTestNew")]
        public void verifyRelatedClientOnInfoPageOfClientUser()
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
            var office_ClientUserHelper = new Office_ClientUserHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Go to create clients page.");
                VisitOffice("users/client_users");
                office_ClientUserHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Enter Client User to be searched");
                //office_ClientUserHelper.TypeText("SearchClientUser", "Alfred Garry");

                executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Select client status");
                office_ClientUserHelper.ClickElement("ClientUser1");
                office_ClientUserHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRelatedClientOnInfoPageOfClientUser", "Verify Related Client present");
                office_ClientUserHelper.VerifyPageText("Related Client");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyRelatedClientOnInfoPageOfClientUser");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Related Client On Info Page Of Client User");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Related Client On Info Page Of Client User", "Bug", "Medium", "Client User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Related Client On Info Page Of Client User");
                        TakeScreenshot("VerifyRelatedClientOnInfoPageOfClientUser");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRelatedClientOnInfoPageOfClientUser.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyRelatedClientOnInfoPageOfClientUser");
                        string id = loginHelper.getIssueID("Verify Related Client On Info Page Of Client User");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRelatedClientOnInfoPageOfClientUser.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Related Client On Info Page Of Client User"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Related Client On Info Page Of Client User");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyRelatedClientOnInfoPageOfClientUser");
                executionLog.WriteInExcel("Verify Related Client On Info Page Of Client User", Status, JIRA, "Client User Management");
            }
        }
    }
}