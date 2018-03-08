using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpResidualIncomeViewTransactionUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpResidualIncomeViewTransactionUrlChange()
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
                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Go To Residual Income");
                VisitCorp("rir/imports");
                
                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Click On View Transaction");
                corp_ResidualIncome_ImportHelper.ClickElement("ClickOnViewTransaction");
                corp_ResidualIncome_ImportHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("rir/transactions/1016");
                
                executionLog.Log("CorpResidualIncomeViewTransactionUrlChange", "Verify Validation");
                corp_ResidualIncome_ImportHelper.WaitForText("oops something went wrong" ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpResidualIncomeViewTransactionUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Corp Residual Income View Transaction Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Residual Income View Transaction Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Residual Income View Transaction Url Change");
                        TakeScreenshot("CorpResidualIncomeViewTransactionUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeViewTransactionUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpResidualIncomeViewTransactionUrlChange");
                        string id = loginHelper.getIssueID("Corp Residual Income View Transaction Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeViewTransactionUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Residual Income View Transaction Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Residual Income View Transaction Url Change");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpResidualIncomeViewTransactionUrlChange");
                executionLog.WriteInExcel("Corp Residual Income View Transaction Url Change", Status, JIRA, "Corp Residual Income");
            }
        }
    }
}
