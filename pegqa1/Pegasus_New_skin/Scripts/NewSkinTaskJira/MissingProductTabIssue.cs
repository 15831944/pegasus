using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MissingProductTabIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void missingProductTabIssue()
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


            // Variable random
            String Status = "Pass";
            String JIRA = "";
            try
            {
                executionLog.Log("MissingProductTabIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MissingProductTabIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MissingProductTabIssue", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("MissingProductTabIssue", "Redirect To Import");
                VisitOffice("pdf_templates/import");

                executionLog.Log("MissingProductTabIssue", "ChooseModule");
                pDFTemplate_ImportWizardHelper.Select("SelectModule", "20");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                var path = GetPathToFile() + "2.pdf";
                pDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("MissingProductTabIssue", "Import file and Click import");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("MissingProductTabIssue", "Verify Product tab is not missing");
                pDFTemplate_ImportWizardHelper.SelectByText("Tab", "Products");
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                Assert.IsTrue(pDFTemplate_ImportWizardHelper.IsElementPresent("//select[@id='tab']/option[text()='Products']"));
                pDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MissingProductTabIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Missing Product Tab Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Missing Product Tab Issue", "Bug", "Medium", "Import PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Missing Product Tab Issue");
                        TakeScreenshot("MissingProductTabIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MissingProductTabIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MissingProductTabIssue");
                        string id = loginHelper.getIssueID("Missing Product Tab Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MissingProductTabIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Missing Product Tab Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Missing Product Tab Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MissingProductTabIssue");
                executionLog.WriteInExcel("Missing Product Tab Issue", Status, JIRA, "Import PDF");
            }

        }
    }
}