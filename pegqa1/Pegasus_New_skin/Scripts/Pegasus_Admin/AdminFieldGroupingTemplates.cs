/* Documented by Khalil Shakir
* 
* The AdminFieldGroupingTemplates script tests the Pegasus Admin Portal.
* It tests to see if a field grouping template can be created in Pegasus. 
* It also tests to see if that template can be used when saving  PDF fields to 
* Pegasus' software.
* It fill finally test to see if that template can be cloned then will delete both 
* to reduce clutter in the testing portal. 
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{
    [TestClass]
    public class AdminFieldGroupingTemplates : DriverTestCase
    {

        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createTemplate()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            AdminFieldGroupingTemplatesHelper adminHelper = new AdminFieldGroupingTemplatesHelper(GetWebDriver());

            // Variable
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            var TemplateTest = "Template Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AdminFieldGroupingTemplates", "Entering User Name and Password");
                Login(username[0], password[0]);

                executionLog.Log("AdminFieldGroupingTemplates", "Wait");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Verifying Dashboard");
                VerifyTitle("Dashboard");

                // Visiting Admin Office
                executionLog.Log("AdminFieldGroupingTemplates", "Redirect at Field Grouping Template");
                VisitOffice("field_grouping_templates");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click CreateButton");
                adminHelper.ClickElement("CreateButton");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "TypeText");
                adminHelper.TypeText("TemplateNameField", TemplateTest);
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Select the client option in module");
                adminHelper.SelectText("Module", "Clients");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminFieldGroupingTemplates", "Select company details option in Tab");
                adminHelper.SelectText("Tab", "Company Details");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Section");
                adminHelper.SelectText("Section", "Company Address");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Field");
                adminHelper.SelectText("Field", "Address Line 1");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click AddFieldButton");
                adminHelper.ClickElement("AddFieldButton");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Save");
                adminHelper.ClickElement("Save");
                adminHelper.WaitForWorkAround(4000);

                // Testing the template by mapping a field in an MPA 
                executionLog.Log("AdminFieldGroupingTemplates", "Redirect at PDF Templates page");
                VisitOffice("pdf_templates");

                executionLog.Log("AdminFieldGroupingTemplates", "Click EditATemplate");
                adminHelper.ClickElement("EditPDFMap");
                adminHelper.WaitForWorkAround(4000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click FirstField");
                adminHelper.ClickElement("PDFFileFiled");
                adminHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click SelectATemplate");
                //  adminHelper.ClickElement("SelectATemplate");

                executionLog.Log("AdminFieldGroupingTemplates", "Click Tab");
                adminHelper.SelectText("Tab", "Company Details");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Select the section");
                adminHelper.SelectText("Section", "Company Address");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click PegasusFieldOption");
                adminHelper.SelectText("PegasusField", "Address Line 1");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Map");
                adminHelper.ClickElement("Map");

                // Saving the Mapping 
                executionLog.Log("AdminFieldGroupingTemplates", "Click SaveMapping");
                adminHelper.ClickElement("SaveMapping");
                adminHelper.WaitForWorkAround(5000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Edit");
                adminHelper.ClickElement("EditPDFMap");

                executionLog.Log("AdminFieldGroupingTemplates", "Wait");
                adminHelper.WaitForWorkAround(5000);

                // Confimng Mapping has been saved 
                executionLog.Log("AdminFieldGroupingTemplates", "check if Mapping is present ");
                adminHelper.IsElementPresent("ConfirmMapping");

                // Delete Mapping 
                executionLog.Log("AdminFieldGroupingTemplates", "Click Mapping");
                adminHelper.ClickElement("ConfirmMapping");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click UndoMapping");
                adminHelper.ClickJs("UndoMapping");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Redirect at PDF Templates page");
                VisitOffice("pdf_templates");
                adminHelper.WaitForWorkAround(3000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click EditATemplate");
                adminHelper.ClickElement("FirstPDFTemple");
                adminHelper.WaitForWorkAround(3000);

                // Clone a Template
                executionLog.Log("AdminFieldGroupingTemplates", "Click CloneTemplate");
                adminHelper.ClickJs("CloneTemplate");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click OK");
                adminHelper.AlertOK();


                // Delete Cloned Template and Original Template
                executionLog.Log("AdminFieldGroupingTemplates", "Click Delete");
                adminHelper.ClickJs("DeleteClonedTemplate");

                executionLog.Log("AdminFieldGroupingTemplates", "Click OK");
                adminHelper.AlertOK();

                executionLog.Log("AdminFieldGroupingTemplates", "Wait");
                adminHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminFieldGroupingTemplates", "Click Delete");
                adminHelper.ClickJs("DeleteTemplate");

                executionLog.Log("AdminFieldGroupingTemplates", "Click OK");
                adminHelper.AlertOK();

                executionLog.Log("AdminFieldGroupingTemplates", "Wait");
                adminHelper.WaitForWorkAround(2000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminFieldGroupingTemplates");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Partner Association");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Partner Association", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Partner Association");
                        TakeScreenshot("AdminFieldGroupingTemplates");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminFieldGroupingTemplates.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminFieldGroupingTemplates");
                        string id = loginHelper.getIssueID("Add Partner Association");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminFieldGroupingTemplates.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Partner Association"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Add Partner Association");
                //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AdminFieldGroupingTemplates");
                executionLog.WriteInExcel("Add Partner Association", Status, JIRA, "Partner Portal");
            }
        }
    }

}


