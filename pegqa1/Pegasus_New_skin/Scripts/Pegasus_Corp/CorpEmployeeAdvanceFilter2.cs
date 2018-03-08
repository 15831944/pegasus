using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpEmployeeAdvanceFilter2 : DriverTestCase
    {

        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void corpEmployeeAdvanceFilter2()
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
                executionLog.Log("CorpEmployeeAdvanceFilter2", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Redirect at employees page.");
                VisitCorp("employees");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify Page title");
                VerifyTitle("Employees");
                //corp_EmployeeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select number of records to 10.");
                corp_EmployeeHelper.SelectByText("ResultsPerPage", "10");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify number of records displayed.");
                corp_EmployeeHelper.ShowResult(10);
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select number of records to 20.");
                corp_EmployeeHelper.SelectByText("ResultsPerPage", "20");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify number of records displayed.");
                corp_EmployeeHelper.ShowResult(20);
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select number of records to 50.");
                corp_EmployeeHelper.SelectByText("ResultsPerPage", "50");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify number of records displayed.");
                corp_EmployeeHelper.ShowResult(50);
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select number of records to 100.");
                corp_EmployeeHelper.SelectByText("ResultsPerPage", "100");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify number of records displayed.");
                corp_EmployeeHelper.ShowResult(100);
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select city in avail columns.");
                corp_EmployeeHelper.SelectByText("AvailableCols", "City");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click arrow to move column.");
                corp_EmployeeHelper.ClickElement("AddCols");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Select zipcode in avail columns.");
                corp_EmployeeHelper.SelectByText("AvailableCols", "Zipcode");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click arrow to move column");
                corp_EmployeeHelper.ClickElement("AddCols");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Enter first name in user details.");
                corp_EmployeeHelper.TypeText("UserDetailsFN", "Sel");

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Enter last name in user details.");
                corp_EmployeeHelper.TypeText("UserDetailsLN", "Enium");

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Enter email in user details.");
                corp_EmployeeHelper.TypeText("UserDetailEmail", "selenium@gmail.com");

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Enter city name in user details.");
                corp_EmployeeHelper.TypeText("UserDetailsCity", "Chicago");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Enter zipcode in user details.");
                corp_EmployeeHelper.TypeText("UserDetailZipCode", "60601");
                //corp_EmployeeHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Click on apply button.");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify employee name on the page.");
                corp_EmployeeHelper.VerifyText("VerifyName", "Sel Enium");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Verify employee email on the page.");
                corp_EmployeeHelper.VerifyText("VerifyEmail2", "selenium@gmail.com");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter2", "Logout from the application.");
                VisitCorp("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpEmployeeAdvanceFilter2");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Employee Advance Filter2");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Employee Advance Filter2", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Employee Advance Filter2");
                        TakeScreenshot("CorpEmployeeAdvanceFilter2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeAdvanceFilter2.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpEmployeeAdvanceFilter2");
                        string id = loginHelper.getIssueID("Corp Employee Advance Filter2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeAdvanceFilter2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Employee Advance Filter2"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Employee Advance Filter2");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CorpEmployeeAdvanceFilter2");
                executionLog.WriteInExcel("Corp Employee Advance Filter2", Status, JIRA, "Corp Emplaoyee");
            }
        }
    }
}