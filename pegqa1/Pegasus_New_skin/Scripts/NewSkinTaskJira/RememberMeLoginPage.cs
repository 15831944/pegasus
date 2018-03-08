using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RememberMeLoginPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void rememberMeLoginPage()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();


            try
            {
                executionLog.Log("RememberMeLoginPage", "EnterUserName");
                office_LeadsHelper.TypeText("EnterUserName", "AslamKhan");

                executionLog.Log("RememberMeLoginPage", "EnterPassword");
                office_LeadsHelper.TypeText("EnterPassword", "1qaz!QAZ");

                executionLog.Log("RememberMeLoginPage", "Click on Remmenbar me");
                office_LeadsHelper.ClickElement("ClickOnRememberMe");

                executionLog.Log("RememberMeLoginPage", "Click on login button");
                office_LeadsHelper.ClickElement("LoginBtnRem");

                executionLog.Log("RememberMeLoginPage", "Redirect To leads");
                VisitOffice("leads");
               
                executionLog.Log("RememberMeLoginPage", "Logout");
                VisitOffice("logout");
               
                executionLog.Log("RememberMeLoginPage", "Redirect To lead");
                VisitOffice("leads");
              
                executionLog.Log("RememberMeLoginPage", "Enter user name.");
                office_LeadsHelper.TypeText("EnterUserName", "AslamKhan");

                executionLog.Log("RememberMeLoginPage", "EnterPassword");
                office_LeadsHelper.TypeText("EnterPassword", "1qaz!QAZ");

                executionLog.Log("RememberMeLoginPage", "Click on remember me");
                office_LeadsHelper.ClickElement("ClickOnRememberMe");

                executionLog.Log("RememberMeLoginPage", "Click on login button");
                office_LeadsHelper.ClickElement("LoginBtnRem");
                office_LeadsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RememberMeLoginPage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Remember Me Login Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Remember Me Login Page", "Bug", "Medium", "Login page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Remember Me Login Page");
                        TakeScreenshot("RememberMeLoginPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RememberMeLoginPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RememberMeLoginPage");
                        string id = loginHelper.getIssueID("Remember Me Login Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RememberMeLoginPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Remember Me Login Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Remember Me Login Page");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RememberMeLoginPage");
                executionLog.WriteInExcel("Remember Me Login Page", Status, JIRA,"Office");
            }
        }
    }
}
		