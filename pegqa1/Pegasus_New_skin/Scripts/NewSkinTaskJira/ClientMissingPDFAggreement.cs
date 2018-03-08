using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientMissingPDFAggreement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientMissingPDFAggreement()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());
            var executionLog = new ExecutionLog();
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientMissingPDFAggreement", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientMissingPDFAggreement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientMissingPDFAggreement", "Redirect at client page.");
                VisitOffice("clients");

                executionLog.Log("ClientMissingPDFAggreement", "Verify Title");
                VerifyTitle();

                executionLog.Log("ClientMissingPDFAggreement", "Open client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("ClientMissingPDFAggreement", "Verify title");
                VerifyTitle("- Details");

                executionLog.Log("ClientMissingPDFAggreement", "Click on PDF tab");
                office_ClientsHelper.ClickElement("PDFTab");

                executionLog.Log("ClientMissingPDFAggreement", "Verify title");
                VerifyTitle("- Pdfs");

                executionLog.Log("ClientMissingPDFAggreement", "Verify Aggreement availabe");
                office_ClientsHelper.verifyElementVisible("TextCardServiceAggreement");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientMissingPDFAggreement");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Clint Missing PDF Aggreement");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Missing PDF Aggreement", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Missing PDF Aggreement");
                        TakeScreenshot("ClientMissingPDFAggreement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMissingPDFAggreement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientMissingPDFAggreement");
                        string id = loginHelper.getIssueID("Client Missing PDF Aggreement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMissingPDFAggreement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Cleint Missing PDF Aggreement"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Missing PDF Aggreement");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientMissingPDFAggreement");
                executionLog.WriteInExcel("Client Missing PDF Aggreement", Status, JIRA, "Client Management");
            }
        }
    }
}

