using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaveLocation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void saveLocation()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            int rand = RandomNumber(10, 1000);



            try
            {
                executionLog.Log("SaveLocation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaveLocation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SaveLocation", "Go to Client page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveLocation", "Open a client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveLocation", "Click on Company detials");
                office_ClientsHelper.ClickElement("CompanyDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveLocation", "Enter the Dba name");
                office_ClientsHelper.TypeText("ClientDBAName", "test@001");

                executionLog.Log("SaveLocation", "Enter the ferderal tax id");
                office_ClientsHelper.TypeText("FederalTaxID", "776666533");

                executionLog.Log("SaveLocation", "CLick on Save button");
                office_ClientsHelper.ClickElement("CDSave");

                executionLog.Log("SaveLocation", "Wait for text");
                office_ClientsHelper.WaitForText("Client data updated successfully.", 10);

                executionLog.Log("SaveLocation", "Go to Client page");
                VisitOffice("clients");

                executionLog.Log("SaveLocation", "Verify title");
                VerifyTitle();

                executionLog.Log("SaveLocation", "Open a client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveLocation", "Click on Company detials");
                office_ClientsHelper.ClickElement("CompanyDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveLocation", "Verify the text");
                office_ClientsHelper.VerifyPageText("More Company Details");

                executionLog.Log("SaveLocation", "Logout from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaveLocation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Save Location");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Save Location", "Bug", "Medium", "Clients page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Save Location");
                        TakeScreenshot("SaveLocation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveLocation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaveLocation");
                        string id = loginHelper.getIssueID("Save Location");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveLocation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Save Location"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Save Location");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaveLocation");
                executionLog.WriteInExcel("Save Location", Status, JIRA, "Client Management");
            }
        }
    }
}