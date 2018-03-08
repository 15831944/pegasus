using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpResidualIncomeCalWizardUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void  corpResidualIncomeCalWizardUrlChange()
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
            var corp_ResidualIncome_ImportHelper = new CorpResidualIncome_ImportHelper(GetWebDriver());
     

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Go To Resisual Income ");
                VisitCorp("rir/imports");
               
                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Click On any Calculation wizard  ");
                corp_ResidualIncome_ImportHelper.ClickElement("ClickOnCalculationwizard");
                corp_ResidualIncome_ImportHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("rir/file_wizard/1016");
                
                executionLog.Log("CorpResidualIncomeCalWizardUrlChange", "Verify Validation");
                corp_ResidualIncome_ImportHelper.WaitForText("oops something went wrong." ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpResidualIncomeCalWizardUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("CorpResidual Income Cal Wizard UrlChange");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CorpResidual Income Cal Wizard UrlChange", "Bug", "Medium", "Corp Residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CorpResidual Income Cal Wizard UrlChange");
                        TakeScreenshot("CorpResidualIncomeCalWizardUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeCalWizardUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpResidualIncomeCalWizardUrlChange");
                        string id = loginHelper.getIssueID("CorpResidual Income Cal Wizard UrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeCalWizardUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CorpResidual Income Cal Wizard UrlChange"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CorpResidual Income Cal Wizard UrlChange");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpResidualIncomeCalWizardUrlChange");
                executionLog.WriteInExcel("CorpResidual Income Cal Wizard UrlChange", Status, JIRA, "Corp Residual income");
            }
        }
    }
}
