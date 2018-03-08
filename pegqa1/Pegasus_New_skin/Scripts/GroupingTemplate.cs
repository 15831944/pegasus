using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class GroupingTemplate : DriverTestCase
    {
        //add fields shown in this template
        void setFields(GroupingTemplateHelper helper,string tab, string[] list, ExecutionLog log)
        {
            log.Log("GroupingTemplate", "add fields shown in this template");
            helper.Select("AdditionalTab", tab);
            helper.WaitForWorkAround(1500);
            for (int i =0; i < list.Length; i++)
            {
                log.Log("GroupingTemplate", "select field name");
                helper.Select("AdditionalValue", list[i]);
                helper.WaitForWorkAround(1500);
                log.Log("GroupingTemplate", "add field");
                helper.ClickElement("AddField");
                helper.WaitForWorkAround(1500);
            }
        }
        //create a new template
        void createTemplate(GroupingTemplateHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplate", "go to grouping templates");
            VisitOffice("field_grouping_templates");
            log.Log("GroupingTemplate", "create a template");
            helper.ClickElement("Create");
            log.Log("GroupingTemplate", "type template name");
            helper.TypeText("TName", "YangTest");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "choose template module");
            helper.Select("Module", "20");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "choose template processor");
            helper.Select("Processor", "3291");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "choose template condition");
            helper.Select("InputField", "client_details.company_dba_name");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "condition: exists");
            helper.Select("InputOperator", "et");
            helper.WaitForWorkAround(1000);

           
            string[] CompanyDetails = { "1757", "1176", "3537" };
            string[] RateFee = { "1579", "2194" };
            log.Log("GroupingTemplate", "start to add fields");
            setFields(helper, "14864",CompanyDetails, log);
            setFields(helper, "14868", RateFee, log);
            log.Log("GroupingTemplate", "save template");
            helper.ClickElement("SaveTemplate");
            log.Log("GroupingTemplate", "template saved");
        }
        //apply the template in client module
        void applyTemplate(GroupingTemplateHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplate", "go to client");
            VisitOffice("clients");
            log.Log("GroupingTemplate", "search for test client");
            helper.TypeText("ClientSearch", "GroupingTest");
            log.Log("GroupingTemplate", "click test client");
            helper.ClickElement("YangTest");
            helper.WaitForWorkAround(2000);
            log.Log("GroupingTemplate", "go to company details");
            helper.ClickElement("CompanyDetails");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "click assignments");
            helper.ClickElement("Assignments");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplate", "click templates tab");
            helper.ClickElement("TemplatesTab");
            log.Log("GroupingTemplate", "choose processor");
            helper.Select("TemplatesProcessor", "First Data Omaha");
            log.Log("GroupingTemplate", "type condition name");
            helper.TypeText("TemplateBN", "name");

            log.Log("GroupingTemplate", "load templates");
            helper.ClickElement("LoadTemplates");
            log.Log("GroupingTemplate", "select template");
            helper.SelectByText("TemplateDrop","YangTest");
            log.Log("GroupingTemplate", "apply template");
            helper.ClickElement("TemplateApply");
            log.Log("GroupingTemplate", "acccept alert");
            helper.AlertOK();
            log.Log("GroupingTemplate", "template applied");
            helper.WaitForWorkAround(2000);
        }
        //checking the function of grouping template
        void CheckGrouping(GroupingTemplateHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplate", "start to apply template");
            applyTemplate(helper,log);
            log.Log("GroupingTemplate", "count the number of fields in company details ");
            int labelNum = helper.LabelCount("CDSection");
            Assert.IsTrue(labelNum == 3);
            log.Log("GroupingTemplate", "company details passed");
            log.Log("GroupingTemplate", "count the number of fields in rates fees");
            helper.ClickElement("RatesFeeTab");
            helper.WaitForWorkAround(2000);
            int inputNum = helper.LabelCount("DFSection");
            Assert.IsTrue(inputNum == 2);
            log.Log("GroupingTemplate", "rates fees passed");
        }
        //clean up
        void deleteTemplate(GroupingTemplateHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplate", "delete:go to grouping fields");
            VisitOffice("field_grouping_templates");
            log.Log("GroupingTemplate", "delete: delete template");
            helper.ClickElement("Delete");
            log.Log("GroupingTemplate", "delete: accept alert");
            helper.AlertOK();
            helper.WaitForWorkAround(2000);
            log.Log("GroupingTemplate", "template deleted");
        }
        [TestMethod]
        public void GroupTest()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username");
            password1 = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var groupingTemplateHelper = new GroupingTemplateHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("GroupingTemplate", "Login with valid username and password");
            Login(username1[0], password1[0]);
            groupingTemplateHelper.WaitForWorkAround(3000);
            executionLog.Log("GroupingTemplate", "create template");
            createTemplate(groupingTemplateHelper, executionLog);
            executionLog.Log("GroupingTemplate", "check template");
            CheckGrouping(groupingTemplateHelper,executionLog);
            executionLog.Log("GroupingTemplate", "delete template");
            deleteTemplate(groupingTemplateHelper, executionLog);






            // }
            //     catch (Exception e)
            //    {
            //        Console.WriteLine("ERRROROOR");
            //        executionLog.Log("Error", e.StackTrace);
            //        Status = "Fail";

            //        String counter = executionLog.readLastLine("counter");
            //        String Description = executionLog.GetAllTextFile("GroupingTemplate");
            //        String Error = executionLog.GetAllTextFile("Error");
            //        if (counter == "")
            //        {
            //            counter = "0";
            //        }
            //        bool result = loginHelper.CheckExstingIssue("GroupingTemplate");
            //        if (!result)
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                loginHelper.CreateIssue("GroupingTemplate", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //                string id = loginHelper.getIssueID("GroupingTemplate");
            //                TakeScreenshot("GroupingTemplate");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\Iframe.png";
            //                loginHelper.AddAttachment(location, id);
            //            }
            //        }
            //        else
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                TakeScreenshot("OfficeFieldDictionary");
            //                string id = loginHelper.getIssueID("FieldValidationFrame");
            //                string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //                var location = directoryName + "\\GroupingTemplate.png";
            //                loginHelper.AddAttachment(location, id);
            //                loginHelper.AddComment(loginHelper.getIssueID("GroupingTemplate"), "This issue is still occurring");
            //            }
            //        }
            //        JIRA = loginHelper.getIssueID("GroupingTemplate");
            //        executionLog.DeleteFile("Error");
            //        throw;

            //    }
            //    finally
            //    {
            //        executionLog.DeleteFile("GroupingTemplate");
            //        executionLog.WriteInExcel("GroupingTemplate", Status, JIRA, "GroupingTemplate");
            //    }
            //}
            //}
            //}      
        }

    }
}
