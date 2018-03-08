using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminCorpEmailURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminCorpEmailURLChange()
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
                executionLog.Log("AdminCorpEmailURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCorpEmailURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminCorpEmailURLChange", "Goto User Admin >> Corporate");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpEmailURLChange", "Select Activity >> Email");
                officeAdmin_CorporateHelper.Select("ActivityType", "E-Mails");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpEmailURLChange", "Click On Email 1 ");
                officeAdmin_CorporateHelper.PressEnter("ClickEmail1");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpEmailURLChange", "Change the url with the url number of another office");
                VisitOffice("viewactivity/e-mail/57");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpEmailURLChange", "Verify Validation");
                officeAdmin_CorporateHelper.WaitForText("You don't have privileges to view this office activity.", 10);
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCorpEmailURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Corp Email URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Corp Email URL Change", "Bug", "Medium", "Corporate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Corp Email URL Change");
                        TakeScreenshot("AdminCorpEmailURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpEmailURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCorpEmailURLChange");
                        string id = loginHelper.getIssueID("Admin Corp Email URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpEmailURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Corp Email URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Corp Email URL Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminCorpEmailURLChange");
                executionLog.WriteInExcel("Admin Corp Email URL Change", Status, JIRA, "My Corp");
            }
        }
    }
}
