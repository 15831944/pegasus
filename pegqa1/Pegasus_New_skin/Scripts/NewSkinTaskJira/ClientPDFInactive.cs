using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientPDFInactive : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientPDFInactive()
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
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // Variables
            string VerifyInactive = "//table[@id='list1']/tbody/tr[2]/td/a/i[contains(@class,'thumbs-o-up')]";
            string text = "//table[@id='list1']/tbody/tr[2]/td/a[contains(@href,'view')]";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ClientPDFInactive", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientPDFInactive", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientPDFInactive", "Visit to PDF template page");
                VisitOffice("pdf_templates");

                executionLog.Log("ClientPDFInactive", "Verify title");
                VerifyTitle("PDF Templates");

                string PdfName = pDFTemplate_PDFTemplateHelper.GetText(text);
                if (!pDFTemplate_PDFTemplateHelper.IsElementPresent(VerifyInactive))
                {
                    executionLog.Log("ClientPDFInactive", "Make inactive pDF");
                    pDFTemplate_PDFTemplateHelper.ClickElement("InActivateSign");

                    executionLog.Log("ClientPDFInactive", "Accept alert message.");
                    pDFTemplate_PDFTemplateHelper.AcceptAlert();
                    pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                }

                executionLog.Log("ClientPDFInactive", "Go to client page");
                VisitOffice("clients");

                executionLog.Log("ClientPDFInactive", "verify title");
                VerifyTitle();

                executionLog.Log("ClientPDFInactive", "Open client");
                office_ClientsHelper.ClickElement("Client1");

                executionLog.Log("ClientPDFInactive", "verify title");
                VerifyTitle(" - Details");

                executionLog.Log("ClientPDFInactive", "click on pdf tab");
                office_ClientsHelper.ClickElement("PDFTab");

                executionLog.Log("ClientPDFInactive", "verify title");
                VerifyTitle(" - Pdfs");

                executionLog.Log("ClientPDFInactive", "verify pdf not available");
                office_ClientsHelper.VerifyTextNotPresent(PdfName + ".pdf");

                executionLog.Log("ClientPDFInactive", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientPDFInactive");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client PDF Inactive");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client PDF Inactive", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client PDF Inactive");
                        TakeScreenshot("ClientPDFInactive");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientPDFInactive.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientPDFInactive");
                        string id = loginHelper.getIssueID("Client PDF Inactive");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientPDFInactive.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client PDF Inactive"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client PDF Inactive");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientPDFInactive");
                executionLog.WriteInExcel("Client PDF Inactive", Status, JIRA, "PDF Import");
            }
        }
    }
}