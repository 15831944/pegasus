using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Iframe : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void iframe()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects5
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var integration_IframeAppsHelper = new Integration_IframeAppsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var IframeTab = "Test" + RandomNumber(99, 99999);
            var email = "Test" + RandomNumber(99, 99999) + "tester";
            var idsc = "1" + RandomNumber(1, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("Iframe", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("Iframe", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("Iframe", "Create Iframe");
                VisitOffice("iframes/create");

                executionLog.Log("Iframe", "Click Save");
                integration_IframeAppsHelper.ClickElement("Save");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("Iframe", "Click Cancel");
                integration_IframeAppsHelper.ClickElement("ClickCancelIframe");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("Iframe", "Verify text on page.");
                integration_IframeAppsHelper.VerifyText("IframeAPPvERIFY", "Iframe Apps");

                executionLog.Log("Iframe", "Redirect at create Iframe page.");
                VisitOffice("iframes/create");

                executionLog.Log("Iframe", "Iframe Tab Name");
                integration_IframeAppsHelper.TypeText("TabName", IframeTab);

                executionLog.Log("Iframe", "Enter Usre Name");
                integration_IframeAppsHelper.TypeText("UserNameInputFieldName", email);

                executionLog.Log("Iframe", "Enter Password");
                integration_IframeAppsHelper.TypeText("PasswordInputFieldNmae", "1234567");

                executionLog.Log("Iframe", "Login Iframe");
                integration_IframeAppsHelper.TypeText("LoginURL", "https://www.facebook.com/?_rdr=p");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("Iframe", "Click Save");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("Iframe", "Wait for Confirmation");
                integration_IframeAppsHelper.WaitForText("Iframe created successfully.", 10);

                executionLog.Log("Iframe", "Search Iframe By Name");
                integration_IframeAppsHelper.TypeText("SearchTabName", IframeTab);
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("Iframe", "Click On Edit");
                integration_IframeAppsHelper.ClickElement("Edit");
                integration_IframeAppsHelper.WaitForWorkAround(3000);

                executionLog.Log("Iframe", "Click Save");
                integration_IframeAppsHelper.ClickElement("Save");

                executionLog.Log("Iframe", "Wait for Confirmation");
                integration_IframeAppsHelper.WaitForText("Iframe updated Successfully.", 10);

                executionLog.Log("Iframe", "Redirect To URL");
                VisitOffice("iframes");

                executionLog.Log("Iframe", "Verify title");
                VerifyTitle("Iframe Apps");

                executionLog.Log("Iframe", "Enter Name to search");
                integration_IframeAppsHelper.TypeText("SearchTabName", IframeTab);
                integration_IframeAppsHelper.WaitForWorkAround(2000);

                executionLog.Log("Iframe", "cLICK Delete btn  ");
                integration_IframeAppsHelper.ClickElement("ClickOnDelete");

                executionLog.Log("Iframe", "Accept alert message. ");
                integration_IframeAppsHelper.AcceptAlert();

                executionLog.Log("Iframe", "Wait for delete message. ");
                integration_IframeAppsHelper.WaitForText("Iframe deleted successfully.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Iframe");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Iframe");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Iframe", "Bug", "Medium", "IFrame page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Iframe");
                        TakeScreenshot("Iframe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Iframe");
                        string id = loginHelper.getIssueID("Iframe");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Iframe"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Iframe");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Iframe");
                executionLog.WriteInExcel("Iframe", Status, JIRA, "Iframe Management");
            }
        }
    }
}