using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyUserAddedEmailNotRequired : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        public void verifyUserAddedEmailNotRequired()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");


            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            // Variables
            var name = "User" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyUserAddedEmailNotRequired", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Go to Create user page");
                VisitOffice("users/create");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Verify title");
                VerifyTitle("Create User");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Select parnter association as type");
                office_UserHelper.SelectByText("Usertype", "Employee");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Click on Create new");
                office_UserHelper.ClickElement("CreateNew");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Expand the Additional Contact Info link");
                office_UserHelper.ClickElement("AddiInfo");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Click on add email.");
                office_UserHelper.ClickElement("AddEmail");

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Verify added email not required.");
                office_UserHelper.VerifyNotRequired();

                executionLog.Log("VerifyUserAddedEmailNotRequired", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyUserAddedEmailNotRequired");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify User Added Email Not Required");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify User Added Email Not Required", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify User Added Email Not Required");
                        TakeScreenshot("VerifyUserAddedEmailNotRequired");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserAddedEmailNotRequired.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyUserAddedEmailNotRequired");
                        string id = loginHelper.getIssueID("Verify User Added Email Not Required");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyUserAddedEmailNotRequired.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify User Added Email Not Required"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify User Added Email Not Required");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyUserAddedEmailNotRequired");
                executionLog.WriteInExcel("Verify User Added Email Not Required", Status, JIRA, "Office Admin");
            }
        }
    }
}