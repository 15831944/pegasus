using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientOwnerTabIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientOwnerTabIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            ExecutionLog executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            //VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientOwnerTabIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientOwnerTabIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientOwnerTabIssue", "Redirect To client page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientOwnerTabIssue", "Verify title");
                VerifyTitle();

                executionLog.Log("ClientOwnerTabIssue", "Open client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("ClientOwnerTabIssue", "Verify title");
                VerifyTitle(" Details");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientOwnerTabIssue", "Click on Owner tab");
                office_ClientsHelper.ClickElement("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientOwnerTabIssue", "Verify Title");
                VerifyTitle("Owners");

                executionLog.Log("ClientOwnerTabIssue", "Veirfy identification state dropdown is working");
                office_ClientsHelper.SelectByText("BankCorruptState", "AK");

                executionLog.Log("ClientOwnerTabIssue", "Veirfy Bankruptcy state dropdown is working");
                office_ClientsHelper.SelectByText("BankruptyState", "AL");

                executionLog.Log("ClientOwnerTabIssue", "Veirfy Business state dropdown is working");
                office_ClientsHelper.SelectByText("BusinessState", "AR");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientOwnerTabIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Owner Tab Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Owner Tab Issue", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Owner Tab Issue");
                        TakeScreenshot("ClientOwnerTabIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientOwnerTabIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientOwnerTabIssue");
                        string id = loginHelper.getIssueID("Client Owner Tab Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientOwnerTabIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Owner Tab Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Owner Tab Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientOwnerTabIssue");
                executionLog.WriteInExcel("Client Owner Tab Issue", Status, JIRA, "Client Management");
            }
        }
    }
}