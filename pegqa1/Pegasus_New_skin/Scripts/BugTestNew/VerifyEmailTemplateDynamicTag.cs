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
    public class VerifyEmailTemplateDynamicTag : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyEmailTemplateDynamicTag()
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
            var system_EmailTemplatesHelper = new System_EmailTemplatesHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEmailTemplateDynamicTag", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEmailTemplateDynamicTag", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEmailTemplateDynamicTag", "Redirect To Email templates page");
                VisitOffice("email_templates");
                system_EmailTemplatesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailTemplateDynamicTag", "Edit First Email Template");
                system_EmailTemplatesHelper.ClickElement("EditEmailTemp1");
                system_EmailTemplatesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailTemplateDynamicTag", "Click on Email Parameters link");
                system_EmailTemplatesHelper.ClickElement("EmailParametersLink");
                system_EmailTemplatesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailTemplateDynamicTag", "Verify Tags contain curly braces");
                system_EmailTemplatesHelper.VerifyText("Constant1", "{");
                system_EmailTemplatesHelper.VerifyText("Constant1", "}");
                system_EmailTemplatesHelper.VerifyText("Constant2", "{");
                system_EmailTemplatesHelper.VerifyText("Constant2", "}");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmailTemplateDynamicTag");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Email Template Dynamic Tag");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Email Template Dynamic Tag", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Email Template Dynamic Tag");
                        TakeScreenshot("VerifyEmailTemplateDynamicTag");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailTemplateDynamicTag.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmailTemplateDynamicTag");
                        string id = loginHelper.getIssueID("Verify Email Template Dynamic Tag");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailTemplateDynamicTag.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Email Template Dynamic Tag"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Email Template Dynamic Tag");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEmailTemplateDynamicTag");
                executionLog.WriteInExcel("Verify Email Template Dynamic Tag", Status, JIRA, "Office Email Templates");
            }
            
        }
    }
}
