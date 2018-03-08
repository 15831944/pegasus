using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FirstDataNorthPDF : DriverTestCase
    {
       [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void firstDataNorthPDF()
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
                executionLog.Log("FirstDataNorthPDF", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("FirstDataNorthPDF", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("FirstDataNorthPDF", "Go to client page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);
               
                executionLog.Log("FirstDataNorthPDF", "Verify title");
                VerifyTitle("Clients");

                executionLog.Log("FirstDataNorthPDF", "Open a client");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("FirstDataNorthPDF", "Click on 'PDF' tab");
                office_ClientsHelper.ClickElement("PDFTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("FirstDataNorthPDF", "Verify First Data North Schedule A PDF is available");
                office_ClientsHelper.verifyElementVisible("NorthPDF");

                executionLog.Log("FirstDataNorthPDF", "Click on First Data North Schedule A PDF");
                office_ClientsHelper.ClickElement("NorthPDF");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("FirstDataNorthPDF", "Verify no error page displayed");
                office_ClientsHelper.VerifyTextNotPresent("Couldn't find matching pdf template");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FirstDataNorthPDF");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("First Data North PDF");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("First Data North PDF", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("First Data North PDF");
                        TakeScreenshot("FirstDataNorthPDF");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FirstDataNorthPDF.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FirstDataNorthPDF");
                        string id = loginHelper.getIssueID("First Data North PDF");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FirstDataNorthPDF.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("First Data North PDF"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("First Data North PDF");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FirstDataNorthPDF");
                executionLog.WriteInExcel("First Data North PDF", Status, JIRA, "Client Management");
            }
        }
    }
}