using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PracticalPDFProblem : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void practicalPDFProblem()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PracticalPDFProblem", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PracticalPDFProblem", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PracticalPDFProblem", "Redirect To tempalte");
                VisitCorp("pdf_templates");
               
                executionLog.Log("PracticalPDFProblem", "Redirect To Import");
                VisitCorp("pdf_templates/import");

                executionLog.Log("PracticalPDFProblem", "ChooseModule");
                corpPDFTemplate_ImportWizardHelper.Select("SelectModule", "20");

                var path = GetPathToFile() + "Test_Final.pdf";
                executionLog.Log("PracticalPDFProblem", "upload file");
                corpPDFTemplate_ImportWizardHelper.UploadFile("//*[@id='PdfTemplatePdfFile']", path);

                executionLog.Log("PracticalPDFProblem", "Click import");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Import");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(4000);

                executionLog.Log("PracticalPDFProblem", "Select tab");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Tab", "Business Details");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PracticalPDFProblem", "Verify fields availble under section");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Section", "Merchant Account Data");
                corpPDFTemplate_ImportWizardHelper.WaitForWorkAround(2000);

                executionLog.Log("PracticalPDFProblem", "Verify fields under fields");
                corpPDFTemplate_ImportWizardHelper.SelectByText("Fields", "Business Type");

                executionLog.Log("PracticalPDFProblem", "Click on Next button");
                corpPDFTemplate_ImportWizardHelper.ClickElement("Next");

                executionLog.Log("PracticalPDFProblem", "Verify mapped successfully");
                corpPDFTemplate_ImportWizardHelper.WaitForText("PDF fields mapped successfully.", 10);

            }        
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PracticalPDFProblem");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Practical PDF Problem");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Practical PDF Problem", "Bug", "Medium", "PDF page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Practical PDF Problem");
                        TakeScreenshot("PracticalPDFProblem");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PracticalPDFProblem.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PracticalPDFProblem");
                        string id = loginHelper.getIssueID("Practical PDF Problem");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PracticalPDFProblem.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Practical PDF Problem"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Practical PDF Problem");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PracticalPDFProblem");
                executionLog.WriteInExcel("Practical PDF Problem", Status, JIRA, "PDF Import");
            }
        }
    }
}