using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FieldDisError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void fieldDisError()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldGroupingTemplateHelper = new Office_FieldGroupingTemplateHelper(GetWebDriver());

            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("FieldDisError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FieldDisError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("FieldDisError", "Go to Create template page");
                VisitOffice("field_grouping_templates");

                executionLog.Log("FieldDisError", "Verify title");
                VerifyTitle("Field Grouping Templates");

                executionLog.Log("FieldDisError", "Click on 'Create' button");
                office_FieldGroupingTemplateHelper.ClickElement("TemplateCreate");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDisError", "Enter TemplateName");
                office_FieldGroupingTemplateHelper.TypeText("TemplateName", "DemoTemplate");

                executionLog.Log("FieldDisError", "Select Module ");
                office_FieldGroupingTemplateHelper.SelectByText("TemplaceModule", "Clients");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(4000);

                executionLog.Log("FieldDisError", "Select Tab field ");
                office_FieldGroupingTemplateHelper.SelectByText("TemplateTab", "Company Details");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("FieldDisError", "Select section Field");
                office_FieldGroupingTemplateHelper.SelectByText("TemplateField", "Company Address");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("FieldDisError", "Select the Sub section");
                office_FieldGroupingTemplateHelper.SelectByText("SectionField", "Company Location Address");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDisError", "Select Field as Business DBA Name");
                office_FieldGroupingTemplateHelper.SelectByText("SelectFieldADDField", "Address Line 2");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDisError", "Click on Add Button");
                office_FieldGroupingTemplateHelper.ClickElement("TemplateAdd");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(4000);

                executionLog.Log("FieldDisError", "Click on Save button");
                office_FieldGroupingTemplateHelper.ClickElement("OfficeSave");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDisError", "Click on Delete icon");
                office_FieldGroupingTemplateHelper.ClickElement("TemplateDelete");
                office_FieldGroupingTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDisError", "Accept alert");
                office_FieldGroupingTemplateHelper.AcceptAlert();

                executionLog.Log("FieldDisError", "Verify error not displayed");
                office_FieldGroupingTemplateHelper.VerifyTextNotPresent("An Internal Error Has Occurred");

                executionLog.Log("FieldDisError", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FieldDisError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Field Dis Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Field Dis Error", "Bug", "Medium", "Dictionary page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Field Dis Error");
                        TakeScreenshot("FieldDisError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDisError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FieldDisError");
                        string id = loginHelper.getIssueID("Field Dis Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDisError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Field Dis Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Field Dis Error");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FieldDisError");
                executionLog.WriteInExcel("Field Dis Error", Status, JIRA, "Field Management");
            }
        }
    }
}