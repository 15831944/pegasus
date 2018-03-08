using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UserVerifyCountryCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void userVerifyCountryCorp()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            try
            {
                executionLog.Log("UserVerifyCountryCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("UserVerifyCountryCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("UserVerifyCountryCorp", "Click on Menu Icon");
                corpOffice_OfficeHelper.ClickElement("MenuIcon");

                executionLog.Log("UserVerifyCountryCorp", "Click on Agent in Topmenu");
                corpOffice_OfficeHelper.MouseOverAndWait("OfficeTabCorp", 5);

                executionLog.Log("UserVerifyCountryCorp", "Click on Office button");
                corpOffice_OfficeHelper.ClickJS("OfficeBtn");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("UserVerifyCountryCorp", "click on Create Button");
                corpOffice_OfficeHelper.ClickElement("Create");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("UserVerifyCountryCorp", "Verify Page title");
                VerifyTitle("Create an Office");

                executionLog.Log("UserVerifyCountryCorp", "Select Mailing Country");
                corpOffice_OfficeHelper.Select("SelectCountryCorp", "Canada");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UserVerifyCountryCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("User Verify Country Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("User Verify Country Corp", "Bug", "Medium", "Corp office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("User Verify Country Corp");
                        TakeScreenshot("UserVerifyCountryCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserVerifyCountryCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UserVerifyCountryCorp");
                        string id = loginHelper.getIssueID("User Verify Country Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UserVerifyCountryCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("User Verify Country Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("User Verify Country Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UserVerifyCountryCorp");
                executionLog.WriteInExcel("User Verify Country Corp", Status, JIRA, "Corp office");
            }
        }
    }
}
