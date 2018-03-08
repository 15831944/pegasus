using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PDFImportError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void pDFImportError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpPDFTemplate_ImportWizardHelper = new CorpPDFTemplate_ImportWizardHelper(GetWebDriver());


            // Variable random
            var path = GetPathToFile() + "2.pdf";
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFImportError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFImportError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PDFImportError", "Redirect To template");
                VisitCorp("pdf_templates");

                executionLog.Log("PDFImportError", "Redirect To Import");
                VisitCorp("pdf_templates/import");

                executionLog.Log("PDFImportError", "SelectModule");
                corpPDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                executionLog.Log("PDFImportError", "Upload file");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("PDFImportError", "Click import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportError", "Select tab");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Tab", "Business Details");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImportError", "Verify fields availble under section");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Section", "Merchant Account Data");

                executionLog.Log("PDFImportError", "Verify fields under fields");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Fields", "Business Type");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFImportError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Import Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Import Error", "Bug", "Medium", "Corp PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Import Error");
                        TakeScreenshot("PDFImportError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFImportError");
                        string id = loginHelper.getIssueID("PDF Import Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImportError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Import Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Import Error");
        //        executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFImportError");
                executionLog.WriteInExcel("PDF Import Error", Status, JIRA, "Corp PDF Import");
            }
        }
    }
}