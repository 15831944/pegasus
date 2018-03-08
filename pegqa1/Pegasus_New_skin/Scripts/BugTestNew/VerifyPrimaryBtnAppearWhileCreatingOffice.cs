using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyPrimaryBtnAppearWhileCreatingOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyPrimaryBtnAppearWhileCreatingOffice()
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
            var corp_Office_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());
            


            //  Variable random
            
            String JIRA = "";
            String Status = "Pass";
            var officename = "Office" + RandomNumber(1, 999999999);
            var username1 = "user" + RandomNumber(1, 9999);

            try
            {
                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected to Dashboard");

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Create Office");
                VisitCorp("offices");
                Console.WriteLine("Redirected to All Office page");

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Click on Create button");
                corp_Office_OfficeHelper.ClickElement("Create");
                corp_Office_OfficeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Verify page title");
                VerifyTitle("Create an Office");

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.Select("EaddressType", "IM");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.verifyElementPresent("PrimaryRadioEmail1");

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.Select("EaddressType", "Social Media");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.verifyElementPresent("PrimaryRadioEmail1");

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.Select("EaddressType", "Web Links");
                corp_Office_OfficeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyPrimaryBtnAppearWhileCreatingOffice", "Select eAddress Type to IM");
                corp_Office_OfficeHelper.verifyElementPresent("PrimaryRadioEmail1");
                Console.WriteLine("Primary radio button does not disappear on selecting eAddress Type other than Email");
 

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPrimaryBtnAppearWhileCreatingOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Primary Btn Appear While Creating Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Primary Btn Appear While Creating Office", "Bug", "Medium", "Create Office", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Primary Btn Appear While Creating Office");
                        TakeScreenshot("VerifyPrimaryBtnAppearWhileCreatingOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryBtnAppearWhileCreatingOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPrimaryBtnAppearWhileCreatingOffice");
                        string id = loginHelper.getIssueID("Verify Primary Btn Appear While Creating Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPrimaryBtnAppearWhileCreatingOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Primary Btn Appear While Creating Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Primary Btn Appear While Creating Office");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyPrimaryBtnAppearWhileCreatingOffice");
                executionLog.WriteInExcel("Verify Primary Btn Appear While Creating Office", Status, JIRA, "Create Office");
            }
        }
    }
}