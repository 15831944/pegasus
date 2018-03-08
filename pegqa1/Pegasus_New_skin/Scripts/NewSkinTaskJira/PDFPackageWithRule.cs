using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PDFPackageWithRule : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void pDFPackageWithRule()
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
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());


            // Variable random
            var name = "Pakage" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PDFPackageWithRule", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PDFPackageWithRule", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PDFPackageWithRule", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("PDFPackageWithRule", "Redirect To template page");
                VisitOffice("pdf_templates");

                executionLog.Log("PDFPackageWithRule", "Click ON pdf");
                pDFTemplate_PDFTemplateHelper.ClickElement("PDFPackage");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFPackageWithRule", "Enter pakage name");
                pDFTemplate_PDFTemplateHelper.TypeText("PackagePDFName", name);
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFPackageWithRule", "Select Module");
                pDFTemplate_PDFTemplateHelper.Select("ChooseModulePakage", "20");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFPackageWithRule", "Click SelectPDFTempwithRule");
                pDFTemplate_PDFTemplateHelper.ClickElement("SelectPDFTempwithRule");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFPackageWithRule", "Enter Template Rule Name");
                pDFTemplate_PDFTemplateHelper.TypeText("TemplateRuleName", "Test Rule");

                executionLog.Log("PDFPackageWithRule", "SelectRuleType");
                pDFTemplate_PDFTemplateHelper.Select("SelectRuleTypePKG", "if");

                executionLog.Log("PDFPackageWithRule", "SelectStatus");
                pDFTemplate_PDFTemplateHelper.Select("SelectOperatorType", "li");
                pDFTemplate_PDFTemplateHelper.SelectDropDownByText("//*[@id='ImportIndexPopIfFieldValue']", "2");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(4000);

                executionLog.Log("PDFPackageWithRule", "Click Add Pdf");
                pDFTemplate_PDFTemplateHelper.ClickElement("ADDPDF");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFPackageWithRule", "Select Pakage Category");
                pDFTemplate_PDFTemplateHelper.SelectByText("PakageCategory", "Other");

                executionLog.Log("PDFPackageWithRule", "Save PDF Package");
                pDFTemplate_PDFTemplateHelper.ClickJs("SavePDFPakage");

                executionLog.Log("PDFPackageWithRule", "Verify message");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Package Template Created Successfully", 10);

                executionLog.Log("PDFPackageWithRule", "Redirect to templates page");
                VisitOffice("pdf_templates");
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(5000);

                executionLog.Log("PDFPackageWithRule", "Verify title");
                VerifyTitle("PDF Templates");

                executionLog.Log("PDFPackageWithRule", "Search pdf template ");
                pDFTemplate_PDFTemplateHelper.TypeText("SearchPDF", name);
                pDFTemplate_PDFTemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFPackageWithRule", "Select template type pakage ");
                pDFTemplate_PDFTemplateHelper.Select("SelectType", "1");

                executionLog.Log("PDFPackageWithRule", "Select first pdf");
                pDFTemplate_PDFTemplateHelper.ClickElement("Checkbox1");

                executionLog.Log("PDFPackageWithRule", "Click on delete button ");
                pDFTemplate_PDFTemplateHelper.ClickElement("DeletePdf");

                executionLog.Log("PDFPackageWithRule", "Accept alert message. ");
                pDFTemplate_PDFTemplateHelper.AcceptAlert();

                executionLog.Log("PDFPackageWithRule", "Wait for message ");
                pDFTemplate_PDFTemplateHelper.WaitForText("PDF Template Deleted Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFPackageWithRule");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Package With Rule");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Package With Rule", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Package With Rule");
                        TakeScreenshot("PDFPackageWithRule");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFPackageWithRule.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFPackageWithRule");
                        string id = loginHelper.getIssueID("PDF Package With Rule");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFPackageWithRule.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Package With Rule"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Package With Rule");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFPackageWithRule");
                executionLog.WriteInExcel("PDF Package With Rule", Status, JIRA, "PDF Import");
            }
        }
    }
}