using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientSharePDF : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientSharePDF()
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
                executionLog.Log("ClientSharePDF", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientSharePDF", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientSharePDF", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("ClientSharePDF", "Verify title");
                VerifyTitle();

                executionLog.Log("ClientSharePDF", "Open a client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("ClientSharePDF", "Verify title");
                VerifyTitle(" Details");

                executionLog.Log("ClientSharePDF", "Click on 'PDF' tab");
                office_ClientsHelper.ClickElement("PDFTab");

                executionLog.Log("ClientSharePDF", "Verify title");
                VerifyTitle(" Pdfs");

                executionLog.Log("ClientSharePDF", "Verify share link available");
                office_ClientsHelper.verifyElementVisible("PDFShare");

                executionLog.Log("ClientSharePDF", "Click on Share link");
                office_ClientsHelper.ClickElement("PDFShare");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientSharePDF", "Verify no error page displayed");
                office_ClientsHelper.VerifyTextNotPresent("500 server error. ");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientSharePDF");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Share Pdf");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Share Pdf", "Bug", "Medium", "Client pdf page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Share Pdf");
                        TakeScreenshot("ClientSharePDF");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientSharePDF.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientSharePDF");
                        string id = loginHelper.getIssueID("Client Share Pdf");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientSharePDF.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Share Pdf"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Share Pdf");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientSharePDF");
                executionLog.WriteInExcel("Client Share Pdf", Status, JIRA, "Client Management");
            }
        }
    }
}