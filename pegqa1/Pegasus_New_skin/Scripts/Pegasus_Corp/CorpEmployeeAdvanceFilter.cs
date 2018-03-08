using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpEmployeeAdvanceFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Temp")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void corpEmployeeAdvanceFilter()
        {

            string[] username1 = null;
            string[] password1 = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpEmployeeAdvanceFilter", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpEmployeeAdvanceFilter", "Redirect at employees page.");
                VisitCorp("employees");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify Page title");
                VerifyTitle("Employees");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Select city in avail columns.");
                corp_EmployeeHelper.SelectByText("AvailableCols", "City");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click arrow to move column.");
                corp_EmployeeHelper.ClickElement("AddCols");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Select state in avail columns.");
                corp_EmployeeHelper.SelectByText("AvailableCols", "State");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click arrow to move column");
                corp_EmployeeHelper.ClickElement("AddCols");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Select zipcode in avail columns.");
                corp_EmployeeHelper.SelectByText("AvailableCols", "Zipcode");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click arrow to move column");
                corp_EmployeeHelper.ClickElement("AddCols");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify columns in displayed list present on page.");
                corp_EmployeeHelper.IsElementPresent("HeadCity");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify columns in displayed list present on page.");
                corp_EmployeeHelper.IsElementPresent("HeadState");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify columns in displayed list present on page.");
                corp_EmployeeHelper.IsElementPresent("HeadZipCode");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify page title as merchants");
                VerifyTitle("Clients");
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Redirect at employees page.");
                VisitCorp("employees");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify Page title");
                VerifyTitle("Employees");
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify default position of phone column.");
                corp_EmployeeHelper.IsElementPresent("Phone3");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify default position of email column.");
                corp_EmployeeHelper.IsElementPresent("Email2");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Select email in displayed column.");
                corp_EmployeeHelper.SelectByText("DisplayCols", "E-Mail");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Move email 1 step up.");
                corp_EmployeeHelper.ClickElement("MoveUp");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Select phone in displayed column.");
                corp_EmployeeHelper.SelectByText("DisplayCols", "Phone");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Move email 1 step down.");
                corp_EmployeeHelper.ClickElement("MoveDown");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify changed position of email column.");
                corp_EmployeeHelper.IsElementPresent("Email1");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Verify changed position of phone column.");
                corp_EmployeeHelper.IsElementPresent("Phone4");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                Console.WriteLine("Counter value is    " + counter);
                String Description = executionLog.GetAllTextFile("CorpEmployeeAdvanceFilter");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Employee Advance Filter");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Employee Advance Filter", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Employee Advance Filter");
                        TakeScreenshot("CorpEmployeeAdvanceFilter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeAdvanceFilter.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpEmployeeAdvanceFilter");
                        string id = loginHelper.getIssueID("Corp Employee Advance Filter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeAdvanceFilter.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Employee Advance Filter"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Employee Advance Filter");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CorpEmployeeAdvanceFilter");
                executionLog.WriteInExcel("Corp Employee Advance Filter", Status, JIRA, "Corp Emplaoyee");
            }
        }
    }
}