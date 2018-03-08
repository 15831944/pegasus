using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AttachMultpleFilesInEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void attachMultpleFilesInEmail()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("AttachMultpleFilesInEmail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AttachMultpleFilesInEmail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AttachMultpleFilesInEmail", "Activeities >> Email");
                VisitOffice("mails/compose");

                var File = GetPathToFile() + "index.jpg";
                Console.WriteLine("file is jpg " + File);
                executionLog.Log("AttachMultpleFilesInEmail", "Upload a file");
                officeActivities_EmailHelper.UploadFile("//*[@id='EmailFiles']", File);
                officeActivities_EmailHelper.WaitForWorkAround(5000);

                executionLog.Log("AttachMultpleFilesInEmail", "Verify file name");
                officeActivities_EmailHelper.VerifyText("VefifyUploadedFileName", "index.jpg");
                officeActivities_EmailHelper.WaitForWorkAround(2000);

                var File2 = GetPathToFile() + "Up.jpg";
                executionLog.Log("AttachMultpleFilesInEmail", "Upload a file");
                officeActivities_EmailHelper.UploadFile("//*[@id='EmailFiles_F1']", File2);
                officeActivities_EmailHelper.WaitForWorkAround(5000);

                executionLog.Log("AttachMultpleFilesInEmail", "Verify file name");
                officeActivities_EmailHelper.VerifyText("VerifyUploaded2Pdf", "Up.jpg");
                officeActivities_EmailHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AttachMultpleFilesInEmail");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Attach Multple Files In Email");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Attach Multple Files In Email", "Bug", "Medium", "Activity email", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Attach Multple Files In Email");
                        TakeScreenshot("AttachMultpleFilesInEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AttachMultpleFilesInEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AttachMultpleFilesInEmail");
                        string id = loginHelper.getIssueID("Attach Multple Files In Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AttachMultpleFilesInEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Attach Multple Files In Email"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Attach Multple Files In Email");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AttachMultpleFilesInEmail");
                executionLog.WriteInExcel("Attach Multple Files In Email", Status, JIRA, "Office Activities");
            }
        }
    }
}