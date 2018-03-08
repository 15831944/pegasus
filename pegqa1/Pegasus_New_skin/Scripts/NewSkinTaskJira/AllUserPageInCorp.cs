using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AllUserPageInCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void allUserPageInCorp()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffices_UsersHelper = new CorpOffices_UsersHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AllUserPageInCorp", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AllUserPageInCorp", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AllUserPageInCorp", "Redirect at all users page.");
                VisitCorp("allusers");

                executionLog.Log("AllUserPageInCorp", "Select User Type");
                corpOffices_UsersHelper.Select("UserType", "Employee");
                corpOffices_UsersHelper.WaitForWorkAround(3000);

                executionLog.Log("AllUserPageInCorp", "Select User Type");
                corpOffices_UsersHelper.Select("UserType", "1099 Sales Agent");
                corpOffices_UsersHelper.WaitForWorkAround(3000);

                executionLog.Log("AllUserPageInCorp", "Select User Type");
                corpOffices_UsersHelper.Select("UserType", "Client");
                corpOffices_UsersHelper.WaitForWorkAround(3000);

                executionLog.Log("AllUserPageInCorp", "Select User Type");
                corpOffices_UsersHelper.Select("UserType", "Partner");
                corpOffices_UsersHelper.WaitForWorkAround(3000);
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AllUserPageInCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("All User Page In Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("All User Page In Corp", "Bug", "Medium", "All User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("All User Page In Corp");
                        TakeScreenshot("AllUserPageInCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AllUserPageInCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AllUserPageInCorp");
                        string id = loginHelper.getIssueID("All User Page In Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AllUserPageInCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("All User Page In Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("All User Page In Corp");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AllUserPageInCorp");
                executionLog.WriteInExcel("All User Page In Corp", Status, JIRA, "Corp Users");
            }
        }
    }
}
