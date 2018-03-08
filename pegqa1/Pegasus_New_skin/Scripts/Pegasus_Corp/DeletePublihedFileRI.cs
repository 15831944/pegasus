using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeletePublihedFileRI : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void deletePublihedFileRI()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpResidualIncome_ImportHelper = new CorpResidualIncome_ImportHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");


            // Variable.
            var FileName = GetPathToFile() + "FDN_Samples.csv";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeletePublihedFileRI", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("DeletePublihedFileRI", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DeletePublihedFileRI", "Click on Import");
                VisitCorp("rir/imports");
                corpResidualIncome_ImportHelper.WaitForWorkAround(4000);

                executionLog.Log("DeletePublihedFileRI", "Verify Page title");
                VerifyTitle("Residual Income - Imports");

                executionLog.Log("DeletePublihedFileRI", "Click On Import New button");
                corpResidualIncome_ImportHelper.ClickElement("ImportNew");
                corpResidualIncome_ImportHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePublihedFileRI", "Verify Page title");
                VerifyTitle("Residual Income - Import New");

                executionLog.Log("DeletePublihedFileRI", "Select Processor");
                corpResidualIncome_ImportHelper.SelectByText("ProcessorType", "First Data Omaha");
                corpResidualIncome_ImportHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePublihedFileRI", "Select Reporting Period");
                corpResidualIncome_ImportHelper.SelectByText("ReportingPeriod", "October");
                //corpResidualIncome_ImportHelper.WaitForWorkAround(5000);

                executionLog.Log("DeletePublihedFileRI", "Click on File Date");
                corpResidualIncome_ImportHelper.ClickElement("FileDate");

                executionLog.Log("DeletePublihedFileRI", "Click on date");
                corpResidualIncome_ImportHelper.ClickElement("ClickDate");

                corpResidualIncome_ImportHelper.Upload("SelectBrowseCSVFile", FileName);
                corpResidualIncome_ImportHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePublihedFileRI", "Click on Import");
                corpResidualIncome_ImportHelper.ClickElement("Import");

                executionLog.Log("DeletePublihedFileRI", " Select Processor Filter");
                corpResidualIncome_ImportHelper.Select("ProcessorFiler", "First Data Omaha");
                corpResidualIncome_ImportHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePublihedFileRI", "Select Filter FileFormat");
                corpResidualIncome_ImportHelper.Select("FilterFileFormat", "First Data Omaha");
                corpResidualIncome_ImportHelper.WaitForWorkAround(3000);

                executionLog.Log("DeletePublihedFileRI", "Enter Filter File name");
                corpResidualIncome_ImportHelper.TypeText("FileName", "FDN_Samples");

                executionLog.Log("DeletePublihedFileRI", "Select Status");
                corpResidualIncome_ImportHelper.Select("SelectStatus", "Imported");
                corpResidualIncome_ImportHelper.WaitForWorkAround(4000);

                executionLog.Log("DeletePublihedFileRI", "Click on calculation.");
                corpResidualIncome_ImportHelper.ClickElement("ClickOnCalculation");

                executionLog.Log("DeletePublihedFileRI", "Wait for success message");
                corpResidualIncome_ImportHelper.WaitForText("Calculation Wizard for", 10);
                corpResidualIncome_ImportHelper.WaitForWorkAround(10000);

                executionLog.Log("DeletePublihedFileRI", "Click on Skip this step");
                corpResidualIncome_ImportHelper.ClickOnDisplayed("SkipStep");
                corpResidualIncome_ImportHelper.WaitForWorkAround(10000);

                executionLog.Log("DeletePublihedFileRI", "Click on Calculate pauout");
                corpResidualIncome_ImportHelper.ClickOnDisplayed("CalculatePayouts");
                corpResidualIncome_ImportHelper.WaitForWorkAround(10000);

                executionLog.Log("DeletePublihedFileRI", "Click on Calculate pauout");
                corpResidualIncome_ImportHelper.ClickOnDisplayed("PublishPayout");
                corpResidualIncome_ImportHelper.WaitForWorkAround(10000);

                executionLog.Log("DeletePublihedFileRI", " Select Processor Filer");
                corpResidualIncome_ImportHelper.Select("ProcessorFiler", "First Data Omaha");

                executionLog.Log("DeletePublihedFileRI", "Select Filter FileFormat");
                corpResidualIncome_ImportHelper.Select("FilterFileFormat", "First Data Omaha");

                executionLog.Log("DeletePublihedFileRI", "Enter file name");
                corpResidualIncome_ImportHelper.TypeText("FileName", "FDN_Samples");

                executionLog.Log("DeletePublihedFileRI", "Select Status");
                corpResidualIncome_ImportHelper.Select("SelectStatus", "Published");

                executionLog.Log("DeletePublihedFileRI", "Click Delete Payout");
                corpResidualIncome_ImportHelper.ClickOnDisplayed("DeletePublished");
                corpResidualIncome_ImportHelper.AcceptAlert();

                executionLog.Log("DeletePublihedFileRI", "Verify text File deleted successfully");
                corpResidualIncome_ImportHelper.WaitForText("File deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeletePublihedFileRI");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Publihed File RI");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Publihed File RI", "Bug", "Medium", "Published File page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Publihed File RI");
                        TakeScreenshot("DeletePublihedFileRI");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePublihedFileRI.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeletePublihedFileRI");
                        string id = loginHelper.getIssueID("Delete Publihed File RI");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeletePublihedFileRI.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Publihed File RI"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Publihed File RI");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeletePublihedFileRI");
                executionLog.WriteInExcel("Delete Publihed File RI", Status, JIRA, "Corp Residual Adjustment");
            }
        }
    }
}