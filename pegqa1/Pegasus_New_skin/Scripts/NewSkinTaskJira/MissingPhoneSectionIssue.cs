using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MissingPhoneSectionIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void missingPhoneSectionIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());

            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("MissingPhoneSectionIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MissingPhoneSectionIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MissingPhoneSectionIssue", "Redirect To Admin");
                VisitOffice("admin");
               
                executionLog.Log("MissingPhoneSectionIssue", "Redirect To Import");
                VisitOffice("pdf_templates/import");

                executionLog.Log("MissingPhoneSectionIssue", "ChooseModule");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                executionLog.Log("MissingPhoneSectionIssue", "Upload file");
                var path = GetPathToFile() + "2.pdf";
                pDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("MissingPhoneSectionIssue", "Import file and Click import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("MissingPhoneSectionIssue", "Select tab");
                pDFTemplate_ImportWizardHelper.SelectByText("Tab", "Contacts");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("MissingPhoneSectionIssue", "Select Section");
                pDFTemplate_ImportWizardHelper.SelectByText("Section", "Contacts");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("MissingPhoneSectionIssue", "Selects SubSection");
                pDFTemplate_ImportWizardHelper.SelectByText("SubSection", "Phones");

                executionLog.Log("MissingPhoneSectionIssue", "Verify Phones is available");
                Assert.IsTrue(pDFTemplate_ImportWizardHelper.IsElementPresent("//*[@id='sub_section']/option[text()='Phones']"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MissingPhoneSectionIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Missing Phone Section Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Missing Phone Section Issue", "Bug", "Medium", "PDF Templates page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Missing Phone Section Issue");
                        TakeScreenshot("MissingPhoneSectionIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MissingPhoneSectionIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MissingPhoneSectionIssue");
                        string id = loginHelper.getIssueID("Missing Phone Section Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MissingPhoneSectionIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Missing Phone Section Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Missing Phone Section Issue");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MissingPhoneSectionIssue");
                executionLog.WriteInExcel("Missing Phone Section Issue", Status, JIRA, "PDF Templates");
            }
        }
    }
}   