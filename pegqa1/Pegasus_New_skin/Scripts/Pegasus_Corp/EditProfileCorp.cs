using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditProfileCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editProfileCorp()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_ProfileHelper = new Corp_ProfileHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditProfileCorp", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditProfileCorp", "Verify page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditProfileCorp", "Redirect to corp Profile page");
                VisitCorp("myprofile");

                executionLog.Log("EditProfileCorp", "Verify page title");
                VerifyTitle("My Profile");
                
                executionLog.Log("EditProfileCorp", "Click on Edit Profile");
                corp_ProfileHelper.ClickElement("EditProfile");

                executionLog.Log("EditProfileCorp", "Verify page title");
                VerifyTitle("Edit Profile");
                
                executionLog.Log("EditProfileCorp", "Enter First Name");
                corp_ProfileHelper.TypeText("FirstName", "Sel");

                executionLog.Log("EditProfileCorp", "Enter Middle name");
                corp_ProfileHelper.TypeText("MiddleName", "");

                executionLog.Log("EditProfileCorp", "Enter Last name");
                corp_ProfileHelper.TypeText("LastrName", "Enium");

                executionLog.Log("EditProfileCorp", "Enter Phone number");
                corp_ProfileHelper.TypeText("PhoneNumber", "(678) 578-4350");

                executionLog.Log("EditProfileCorp", "Click on save button");
                corp_ProfileHelper.ClickElement("Save");
                corp_ProfileHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditProfileCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Profile Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Profile Corp", "Bug", "Medium", "Profile Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Profile Corp");
                        TakeScreenshot("EditProfileCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProfileCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditProfileCorp");
                        string id = loginHelper.getIssueID("Edit Profile Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditProfileCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Profile Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Profile Corp");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditProfileCorp");
                executionLog.WriteInExcel("Edit Profile Corp", Status, JIRA, "Corp Profile");
            }
        }
    }
}
