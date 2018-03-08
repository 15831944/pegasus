using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ValidationForFileTypeOfficeLogo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void validationForFileTypeOfficeLogo()
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
            var office_MyProfileHelper = new Office_MyProfileHelper(GetWebDriver());


            try
            {
                executionLog.Log("ValidationForFileTypeOfficeLogo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ValidationForFileTypeOfficeLogo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ValidationForFileTypeOfficeLogo", "Navigate to on edit profile page");
                VisitOffice("editProfile");

                string path = GetPathToFile() + "2.pdf";
                executionLog.Log("ValidationForFileTypeOfficeLogo", "Upload Invalid File");
                office_MyProfileHelper.UploadFile("//*[@id='EmployeeProfileImage']", path);

                executionLog.Log("ValidationForFileTypeOfficeLogo", "Verify Alert text");
                office_MyProfileHelper.VerifyAlertText("please select a valid file!");
                office_MyProfileHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ValidationForFileTypeOfficeLogo");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Validation For File Type Office Logo");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Validation For File Type Office Logo", "Bug", "Medium", "My Profile page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Validation For File Type Office Logo");
                        TakeScreenshot("ValidationForFileTypeOfficeLogo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForFileTypeOfficeLogo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ValidationForFileTypeOfficeLogo");
                        string id = loginHelper.getIssueID("Validation For File Type Office Logo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ValidationForFileTypeOfficeLogo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Validation For File Type Office Logo"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Validation For File Type Office Logo");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ValidationForFileTypeOfficeLogo");
                executionLog.WriteInExcel("Validation For File Type Office Logo", Status, JIRA,"My Profile");
            }
        }
    }
}