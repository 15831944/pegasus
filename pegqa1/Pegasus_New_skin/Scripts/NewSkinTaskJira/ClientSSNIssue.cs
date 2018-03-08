using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientSSNIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientSSNIssue()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientSSNIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientSSNIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientSSNIssue", "Go to client page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientSSNIssue", "Verify title");
                VerifyTitle("Clients");

                executionLog.Log("ClientSSNIssue", "Open a client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientSSNIssue", "Click on 'Owner' tab");
                office_ClientsHelper.ClickForce("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientSSNIssue", "Enter the title of owner");
                office_ClientsHelper.TypeText("TitleOwner", "President");

                executionLog.Log("ClientSSNIssue", "Enter SSN");
                office_ClientsHelper.TypeText("SSN", "877656756");

                executionLog.Log("ClientSSNIssue", "Click on Save button");
                office_ClientsHelper.ClickElement("OwnerSave");

                executionLog.Log("ClientSSNIssue", "Wait for success message");
                office_ClientsHelper.WaitForText("Client data updated successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientSSNIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client SSN Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client SSN Issue", "Bug", "Medium", "Client Owner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client SSN Issue");
                        TakeScreenshot("ClientSSNIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientSSNIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientSSNIssue");
                        string id = loginHelper.getIssueID("Client SSN Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientSSNIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client SSN Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client SSN Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientSSNIssue");
                executionLog.WriteInExcel("Client SSN Issue", Status, JIRA, "Client Management");
            }
        }
    }
}