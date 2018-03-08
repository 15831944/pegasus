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
    public class OfficeVerifyFND : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void officeVerifyFND()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("OfficeVerifyFND", "Login with valid credential  Username");
                Login(username[0], password[0]);
                //corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyFND", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeVerifyFND", "Redirect to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyFND", "Verify Page title");
                VerifyTitle("Offices");
               
                executionLog.Log("OfficeVerifyFND", "Enter office Name");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", "newthemeoffice");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeVerifyFND", "Click on Office");
                corpOffice_OfficeHelper.ClickElement("ClickOnOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyFND", "Verify Page title");
                VerifyTitle("Details");

                executionLog.Log("OfficeVerifyFND", "Verify codes Present on page");
                corpOffice_OfficeHelper.VerifyPageText("5021");
                corpOffice_OfficeHelper.VerifyPageText("3423");

                executionLog.Log("OfficeVerifyFND", "Redirect to office page");
                VisitCorp("offices");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyFND", "Verify Page title");
                VerifyTitle("Offices");
               
                executionLog.Log("OfficeVerifyCodes", "Enter Sel Name");
                corpOffice_OfficeHelper.TypeText("EnterSelenium", "Office90335");
                corpOffice_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeVerifyFND", "Click on Office");
                corpOffice_OfficeHelper.ClickElement("ClickOnOffice");
                corpOffice_OfficeHelper.WaitForWorkAround(3000);

                executionLog.Log("OfficeVerifyFND", "Verify Page title");
                VerifyTitle("Details");

                executionLog.Log("OfficeVerifyFND", "Verify codes Present on page");
                corpOffice_OfficeHelper.VerifyPageText("123456");
                corpOffice_OfficeHelper.VerifyPageText("4534354");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeVerifyFND");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Office Verify FND");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Office Verify FND", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Office Verify FND");
                        TakeScreenshot("OfficeVerifyFND");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeVerifyFND.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeVerifyFND");
                        string id = loginHelper.getIssueID("Office Verify FND");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeVerifyFND.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Office Verify FND"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Office Verify FND");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("OfficeVerifyFND");
                executionLog.WriteInExcel("Office Verify FND", Status, JIRA, "Corp Office");
            }
        }
    }
}


