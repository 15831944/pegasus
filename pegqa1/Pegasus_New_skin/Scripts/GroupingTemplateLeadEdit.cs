using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
namespace PegasusTests.Scripts
{
    [TestClass]
    public class GroupingTemplateLeadEdit : DriverTestCase
    {
        //add fields shown in this template
        void setFields(GroupingTemplateLeadEditHelper helper, string tab, string[] list, ExecutionLog log)
        {
            log.Log("GroupingTemplateLeadEdit", "add fields shown in this template");
            helper.Select("AdditionalTab", tab);
            helper.WaitForWorkAround(1500);
            for (int i = 0; i < list.Length; i++)
            {
                log.Log("GroupingTemplateLeadEdit", "select field name");
                helper.Select("AdditionalValue", list[i]);
                helper.WaitForWorkAround(1500);
                log.Log("GroupingTemplateLeadEdit", "add field");
                helper.ClickElement("AddField");
                helper.WaitForWorkAround(1500);
            }
        }
        //create a new template
        void createTemplate(GroupingTemplateLeadEditHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplateLeadEdit", "go to grouping templates");
            VisitOffice("field_grouping_templates");
            log.Log("GroupingTemplateLeadEdit", "create a template");
            helper.ClickElement("Create");
            log.Log("GroupingTemplateLeadEdit", "type template name");
            helper.TypeText("TName", "YangTestLead");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "choose template module");
            helper.Select("Module", "14");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "choose template processor");
            helper.Select("Processor", "3291");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "choose template condition");
            helper.Select("InputField", "lead_details.company_name");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "condition: exists");
            helper.Select("InputOperator", "et");
            helper.WaitForWorkAround(1000);


            string[] CompanyDetails = { "640", "4649", "4291" };
            string[] RateFee = { "464", "1871" };
            log.Log("GroupingTemplateLeadEdit", "start to add fields");
            setFields(helper, "14879", CompanyDetails, log);
            setFields(helper, "14881", RateFee, log);
            log.Log("GroupingTemplateLeadEdit", "save template");
            helper.ClickElement("SaveTemplate");
            log.Log("GroupingTemplateLeadEdit", "template saved");
        }
        //apply the template in client module
        void applyTemplate(GroupingTemplateLeadEditHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplateLeadEdit", "go to client");
            VisitOffice("leads");
            log.Log("GroupingTemplateLeadEdit", "search for test client");
            helper.TypeText("LeadSearch", "Grouping Test");
            log.Log("GroupingTemplateLeadEdit", "click test client");
            helper.ClickElement("YangTest");
            helper.WaitForWorkAround(2000);
            log.Log("GroupingTemplateLeadEdit", "go to company details");
            helper.ClickElement("CompanyDetails");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "click assignments");
            helper.ClickElement("Assignments");
            helper.WaitForWorkAround(1000);
            log.Log("GroupingTemplateLeadEdit", "click templates tab");
            helper.ClickElement("TemplatesTab");
            log.Log("GroupingTemplateLeadEdit", "choose processor");
            helper.Select("TemplatesProcessor", "First Data Omaha");
            log.Log("GroupingTemplateLeadEdit", "type condition name");
            helper.TypeText("TemplateBN", "name");
            log.Log("GroupingTemplateLeadEdit", "load templates");
            helper.ClickElement("LoadTemplates");
            log.Log("GroupingTemplateLeadEdit", "select template");
            helper.SelectByText("TemplateDrop", "YangTestLead");
            log.Log("GroupingTemplateLeadEdit", "apply template");
            helper.ClickElement("TemplateApply");
            log.Log("GroupingTemplateLeadEdit", "acccept alert");
            helper.AlertOK();
            log.Log("GroupingTemplateLeadEdit", "template applied");
            helper.WaitForWorkAround(2000);
        }
        //checking the function of grouping template
        void CheckGrouping(GroupingTemplateLeadEditHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplateLeadEdit", "start to apply template");
            applyTemplate(helper,log);
            log.Log("GroupingTemplateLeadEdit", "count the number of fields in company details ");
            int labelNum = helper.LabelCount("CDSection");
            Assert.IsTrue(labelNum == 3);
            log.Log("GroupingTemplateLeadEdit", "company details passed");
            log.Log("GroupingTemplateLeadEdit", "count the number of fields in rates fees");
            helper.ClickElement("RatesFeeTab");
            helper.WaitForWorkAround(2000);
            int inputNum = helper.LabelCount("DFSection");
            Assert.IsTrue(inputNum == 2);
            log.Log("GroupingTemplateLeadEdit", "rates fees passed");
        }
        //clean up
        void deleteTemplate(GroupingTemplateLeadEditHelper helper, ExecutionLog log)
        {
            log.Log("GroupingTemplateLeadEdit", "delete:go to grouping fields");
            VisitOffice("field_grouping_templates");
            log.Log("GroupingTemplateLeadEdit", "delete: delete template");
            helper.ClickElement("Delete");
            log.Log("GroupingTemplateLeadEdit", "delete: accept alert");
            helper.AlertOK();
            helper.WaitForWorkAround(2000);
            log.Log("GroupingTemplateLeadEdit", "template deleted");
        }
        [TestMethod]
        public void GroupTestLead()
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
            var groupingTemplateLeadEditHelper = new GroupingTemplateLeadEditHelper(GetWebDriver());

            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            //try
            //{
            executionLog.Log("GroupingTemplateLeadEdit", "Login with valid username and password");
            Login(username1[0], password1[0]);
            groupingTemplateLeadEditHelper.WaitForWorkAround(3000);
            executionLog.Log("GroupingTemplateLeadEdit", "create template");
            createTemplate(groupingTemplateLeadEditHelper, executionLog);
            executionLog.Log("GroupingTemplateLeadEdit", "check template");
            CheckGrouping(groupingTemplateLeadEditHelper, executionLog);
            executionLog.Log("GroupingTemplateLeadEdit", "delete template");
            deleteTemplate(groupingTemplateLeadEditHelper, executionLog);






            // }
            //     catch (Exception e)
            //    {
            //        Console.WriteLine("ERRROROOR");
            //        executionLog.Log("Error", e.StackTrace);
            //        Status = "Fail";

            //        String counter = executionLog.readLastLine("counter");
            //        String Description = executionLog.GetAllTextFile("GroupingTemplateLeadEdit");
            //        String Error = executionLog.GetAllTextFile("Error");
            //        if (counter == "")
            //        {
            //            counter = "0";
            //        }
            //        bool result = loginHelper.CheckExstingIssue("GroupingTemplateLeadEdit");
            //        if (!result)
            //        {
            //            if (Int16.Parse(counter) < 5)
            //            {
            //                executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //                loginHelper.CreateIssue("GroupingTemplateLeadEdit", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //                string id = loginHelper.getIssueID("GroupingTemplateLeadEdit");
            //                TakeScreenshot("GroupingTemplateLeadEdit");
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
            //                var location = directoryName + "\\GroupingTemplateLeadEdit.png";
            //                loginHelper.AddAttachment(location, id);
            //                loginHelper.AddComment(loginHelper.getIssueID("GroupingTemplateLeadEdit"), "This issue is still occurring");
            //            }
            //        }
            //        JIRA = loginHelper.getIssueID("GroupingTemplateLeadEdit");
            //        executionLog.DeleteFile("Error");
            //        throw;

            //    }
            //    finally
            //    {
            //        executionLog.DeleteFile("GroupingTemplateLeadEdit");
            //        executionLog.WriteInExcel("GroupingTemplateLeadEdit", Status, JIRA, "GroupingTemplateLeadEdit");
            //    }
            //}
            //}
            //}      
        }

    }
}
