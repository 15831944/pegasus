using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateDuplicateAvatarError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createDuplicateAvatarError()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            var AvatarName = "Avatar" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //Initializing the objects
            ExecutionLog executionLog = new ExecutionLog();
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            var corpSystem_AvtarsHelper = new CorpSystem_AvtarsHelper(GetWebDriver());

            try
            {

                executionLog.Log("createDuplicateAvatarError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("createDuplicateAvatarError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("createDuplicateAvatarError", "Go to Avatar page");
                VisitCorp("avatars");

                executionLog.Log("createDuplicateAvatarError", "Verify title");
                VerifyTitle("Avatars");

                executionLog.Log("createDuplicateAvatarError", "Verify created avatar is available");
                bool available = corpSystem_AvtarsHelper.verifyAvatarAvailable(AvatarName);
                bool flag = true;
                executionLog.Log("createDuplicateAvatarError", "Create avatar is not available.");

                executionLog.Log("createDuplicateAvatarError", "Go to create Avatar page");
                VisitCorp("avatars/create");

                executionLog.Log("createDuplicateAvatarError", "Verify Title");
                VerifyTitle("Create an Avatar");

                executionLog.Log("createDuplicateAvatarError", "Enter avatar name");
                corpSystem_AvtarsHelper.TypeText("AvatarName", AvatarName);

                executionLog.Log("createDuplicateAvatarError", "Select User type");
                corpSystem_AvtarsHelper.SelectByText("AvatarType", "Employee");

                executionLog.Log("createDuplicateAvatarError", "Select Status");
                corpSystem_AvtarsHelper.SelectByText("Status", "Active");

                executionLog.Log("createDuplicateAvatarError", "Click on Save button");
                corpSystem_AvtarsHelper.ClickElement("Save");

                if (available)
                {
                    executionLog.Log("createDuplicateAvatarError", "Verify ERROR");
                    corpSystem_AvtarsHelper.VerifyPageText("Avatar exists with this name.");

                    flag = false;
                }
                else
                {
                    executionLog.Log("createDuplicateAvatarError", "Verify title");
                    VerifyTitle("Avatars");

                    executionLog.Log("createDuplicateAvatarError", "Avatar is available");
                    Assert.IsTrue(corpSystem_AvtarsHelper.verifyAvatarAvailable(AvatarName));
                }

                if (flag)
                {
                    executionLog.Log("createDuplicateAvatarError", "Go to create Avatar page");
                    VisitCorp("avatars/create");

                    executionLog.Log("createDuplicateAvatarError", "Verify Title");
                    VerifyTitle("Create an Avatar");

                    executionLog.Log("createDuplicateAvatarError", "Enter avatar name");
                    corpSystem_AvtarsHelper.TypeText("AvatarName", AvatarName);

                    executionLog.Log("createDuplicateAvatarError", "Select User type");
                    corpSystem_AvtarsHelper.SelectByText("AvatarType", "Employee");

                    executionLog.Log("createDuplicateAvatarError", "Select Status");
                    corpSystem_AvtarsHelper.SelectByText("Status", "Active");

                    executionLog.Log("createDuplicateAvatarError", "Click on Save button");
                    corpSystem_AvtarsHelper.ClickElement("Save");

                    executionLog.Log("createDuplicateAvatarError", "Verify ERROR");
                    corpSystem_AvtarsHelper.WaitForText("Avatar exists with this name.", 10);

                }
            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateDuplicateAvatarError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Duplicate Avatar Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Duplicate Avatar Error", "Bug", "Medium", "Avatar page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Duplicate Avatar Error");
                        TakeScreenshot("CreateDuplicateAvatarError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDuplicateAvatarError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateDuplicateAvatarError");
                        string id = loginHelper.getIssueID("Create Duplicate Avatar Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDuplicateAvatarError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Duplicate Avatar Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Duplicate Avatar Error");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateDuplicateAvatarError");
                executionLog.WriteInExcel("Create Duplicate Avatar Error", Status, JIRA, "Corporate Avatar");
            }
        }
    }
}