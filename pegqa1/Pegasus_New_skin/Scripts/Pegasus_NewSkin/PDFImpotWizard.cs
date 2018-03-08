using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PDFImpotWizard : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void pDFImpotWizard()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var pDFTemplate_ImportWizardHelper = new PDFTemplate_ImportWizardHelper(GetWebDriver());
            var pDFTemplate_PDFTemplateHelper = new PDFTemplate_PDFTemplateHelper(GetWebDriver());
            var corpPDFTemplate_ImportWizardHelper = new CorpPDFTemplate_ImportWizardHelper(GetWebDriver());
            var corpPDFTemplate_CategoriesHelper = new CorpPDFTemplate_CategoriesHelper(GetWebDriver());

            // Variable random
            var Category = "Category" + RandomNumber(1, 99999);
            var name = "TestMerchant" + GetRandomNumber();
            var FilePth = GetPathToFile() + "real.pdf";
            var InvalidFilePath = GetPathToFile() + "clientsamples(2).csv";

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("PDFImpotWizard", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("PDFImpotWizard", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("PDFImpotWizard", "Redirect at pdf categories pge.");
                VisitCorp("pdf_templates/categories");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Click Create PDF Template");
                corpPDFTemplate_CategoriesHelper.ClickElement("Create");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Enter PDF NAME");
                corpPDFTemplate_CategoriesHelper.TypeText("EnterName", Category);

                executionLog.Log("PDFImpotWizard", "Click on Save");
                corpPDFTemplate_CategoriesHelper.ClickElement("Save");

                executionLog.Log("PDFImpotWizard", "Wait for Confirmation");
                corpPDFTemplate_CategoriesHelper.WaitForText("Category Created Successfully", 10);

                executionLog.Log("PDFImpotWizard", "Visit pdf Template import page");
                VisitCorp("pdf_templates/import");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Select Module.");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");

                executionLog.Log("PDFImpotWizard", "Uplaod File");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", FilePth);
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Click on Import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");

                executionLog.Log("PDFImpotWizard", "Wait for next button to appear.");
                corpPDFTemplate_ImportWizardHelper.WaitForElementPresent("ClickNextbtn", 10);

                executionLog.Log("PDFImpotWizard", "Click on Next");
                corpPDFTemplate_ImportWizardHelper.ClickDisplayed("//button[@title='Next']");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Wait for Confirmation");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF fields mapped successfully.", 05);

                executionLog.Log("PDFImpotWizard", "Wait for next button to appear.");
                corpPDFTemplate_ImportWizardHelper.WaitForElementPresent("ClickNextBtn2", 10);

                executionLog.Log("PDFImpotWizard", "Click on Next button again");
                corpPDFTemplate_ImportWizardHelper.ClickElement("ClickNextBtn2");

                executionLog.Log("PDFImpotWizard", "Verify confirmation");
                corpPDFTemplate_ImportWizardHelper.WaitForText("Signature Options saved successfully.", 10);

                executionLog.Log("PDFImpotWizard", "Select Category");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectCatory", "Card Service Agreements");

                executionLog.Log("PDFImpotWizard", "Click on Save");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("PDFImpotWizard", "Verif Confirmation");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

                executionLog.Log("PDFImpotWizard", "Visit pdf Template import page");
                VisitCorp("pdf_templates/import");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Click Import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                //corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Wait for validation text.");
                corpPDFTemplate_ImportWizardHelper.WaitForText("This field is required.", 10);

                executionLog.Log("PDFImpotWizard", "Click on cancel.");
                corpPDFTemplate_ImportWizardHelper.ClickDisplayed("//a[@title='Cancel']");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Verify text on page.");
                corpPDFTemplate_ImportWizardHelper.VerifyText("VerifyTextPDFTemplatesHeader", "PDF Templates");

                executionLog.Log("PDFImpotWizard", "Visit pdf Template import page");
                VisitCorp("pdf_templates/import");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Select Module");
                corpPDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");

                executionLog.Log("PDFImpotWizard", "Uplaod File");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", InvalidFilePath);
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Click Import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Accept alert message.");
                corpPDFTemplate_ImportWizardHelper.AcceptAlert();

                executionLog.Log("PDFImpotWizard", "Logout from the application.");
                VisitCorp("logout");

                executionLog.Log("PDFImpotWizard", "Login to the office module.");
                Login(username1[0], password[0]);
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("PDFImpotWizard", "Goto PDF Categories");
                VisitOffice("pdf_templates/categories");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(4000);

                executionLog.Log("PDFImpotWizard", "Click Create PDF Template");
                pDFTemplate_PDFTemplateHelper.ClickElement("ClickCreatePDFImp");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Enter PDF Category Name");
                pDFTemplate_PDFTemplateHelper.TypeText("EnterPDFCategoryName", Category);

                executionLog.Log("PDFImpotWizard", "Click on Save");
                pDFTemplate_PDFTemplateHelper.ClickElement("PDFImportSave");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Goto pdf template import");
                VisitOffice("pdf_templates/import");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Select Module");
                pDFTemplate_ImportWizardHelper.SelectByText("SelectModule", "Clients");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Upload file.");
                pDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", FilePth);
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Click Import PDF");
                pDFTemplate_ImportWizardHelper.ClickElement("Import");

                executionLog.Log("PDFImpotWizard", "Wait for next button to appear.");
                pDFTemplate_ImportWizardHelper.WaitForElementPresent("ClickNextbtn", 10);

                executionLog.Log("PDFImpotWizard", "Click Next button");
                pDFTemplate_ImportWizardHelper.ClickElement("Next");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Verify Confirmation");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF fields mapped successfully.", 10);

                executionLog.Log("PDFImpotWizard", "Wait for next button to appear.");
                pDFTemplate_ImportWizardHelper.WaitForElementPresent("ClickNextBtn2", 10);

                executionLog.Log("PDFImpotWizard", "Click Next button");
                pDFTemplate_ImportWizardHelper.ClickElement("ClickNextBtn2");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("PDFImpotWizard", "Select Category.");
                pDFTemplate_ImportWizardHelper.SelectByText("Category", "Card Service Agreements");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("PDFImpotWizard", "Click on Save");
                pDFTemplate_ImportWizardHelper.ClickElement("Save");

                executionLog.Log("PDFImpotWizard", "Wait for Confirmation");
                pDFTemplate_ImportWizardHelper.WaitForText("PDF Template options saved successfully.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PDFImpotWizard");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("PDF Impot Wizard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("PDF Impot Wizard", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("PDF Impot Wizard");
                        TakeScreenshot("PDFImpotWizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImpotWizard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PDFImpotWizard");
                        string id = loginHelper.getIssueID("PDF Impot Wizard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PDFImpotWizard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("PDF Impot Wizard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("PDF Impot Wizard");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PDFImpotWizard");
                executionLog.WriteInExcel("PDF Impot Wizard", Status, JIRA, "PDF Import");
            }
        }
    }
}
