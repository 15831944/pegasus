using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminCorpDocumentURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Temp")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminCorpDocumentURLChange()
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
            var officeAdmin_CorporateHelper = new OfficeAdmin_CorporateHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminCorpDocumentURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCorpDocumentURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminCorpDocumentURLChange", "Goto User Admin >> Corporate");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpDocumentURLChange", "Select Activity >> Document");
                officeAdmin_CorporateHelper.Select("ActivityType", "Documents");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpDocumentURLChange", "Click On Document ");
                officeAdmin_CorporateHelper.ClickViaJavaScript("//table[@id='list1']/tbody/tr[2]/td[8]");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpDocumentURLChange", "Change the url with the url number of another office");
                VisitOffice("viewactivity/document/41");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpDocumentURLChange", "Verify Validation");
                officeAdmin_CorporateHelper.VerifyPageText("You don't have privileges to view this office activity.");
                officeAdmin_CorporateHelper.WaitForWorkAround(4000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCorpDocumentURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Corp Document URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Corp Document URL Change", "Bug", "Medium", "Corporate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Corp Document URL Change");
                        TakeScreenshot("AdminCorpDocumentURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpDocumentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCorpDocumentURLChange");
                        string id = loginHelper.getIssueID("Admin Corp Document URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpDocumentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Corp Document URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Corp Document URL Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminCorpDocumentURLChange");
                executionLog.WriteInExcel("Admin Corp Document URL Change", Status, JIRA, "Corp Profile.");
            }
        }
    }
}
